using Mapster;
using SqlSugar;
using System.Reflection;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewAction.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    /// <summary>
    /// 表单基础
    /// </summary>
    public class FormManager
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormManager(CurrentUser loginuser, SqlSugarScope db, Language lang)
        {
            _loginuser = loginuser;
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询表单通知Token信息
        /// </summary>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetFormNotificationTokenWithUser(string tokenValue)
        {
            return await _db.Queryable<FormNotificationTokenEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((token, user) => token.ReviewUserId == user.UserId)
                            .Where((token, user) => token.Token == tokenValue)
                            .Select((token, user) => user)
                            .FirstAsync();
        }

        /// <summary>
        /// 初始化表单
        /// </summary>
        /// <param name="formTypeId"></param>
        public async Task<string> InitializeFormInstance(long formTypeId)
        {
            var now = DateTime.Now;
            var ym = now.ToString("yyyyMM");
            var formId = SnowFlakeSingle.Instance.NextId();

            // 并行查询：表单前缀 + 起始步骤（互不依赖）
            var prefixTask = _db.Queryable<FormTypeEntity>()
                                .With(SqlWith.NoLock)
                                .Where(ft => ft.FormTypeId == formTypeId)
                                .Select(ft => ft.Prefix)
                                .FirstAsync();

            var startStepIdTask = _db.Queryable<WorkflowStepEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(step => step.FormTypeId == formTypeId && step.IsStartStep == 1)
                                     .Select(step => step.StepId)
                                     .FirstAsync();

            await Task.WhenAll(prefixTask, startStepIdTask);
            var prefix = await prefixTask;
            var startStepId = await startStepIdTask;

            string formNo = string.Empty;

            // 事务内完成：取号 + 插入表单实例 + 插入待审记录
            await _db.Ado.UseTranAsync(async () =>
            {
                // 应用锁：必须在事务内，LockOwner='Transaction' 才有效
                var lockKey = $"FormNo_{formTypeId}_{ym}";
                await _db.Ado.ExecuteCommandAsync(
                    "EXEC sp_getapplock @Resource = @lockKey, @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 10000",
                    new { lockKey });

                // 取号（不加 NoLock，避免脏读导致重复号）
                var sequence = await _db.Queryable<FormSequenceEntity>()
                                        .Where(s => s.FormTypeId == formTypeId && s.Ym == ym)
                                        .FirstAsync();

                int nextNo;
                if (sequence == null)
                {
                    nextNo = 1;
                    await _db.Insertable(new FormSequenceEntity
                    {
                        FormTypeId = formTypeId,
                        Ym = ym,
                        Total = nextNo,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = now
                    }).ExecuteCommandAsync();
                }
                else
                {
                    nextNo = sequence.Total + 1;
                    await _db.Updateable<FormSequenceEntity>()
                             .SetColumns(s => new FormSequenceEntity
                             {
                                 Total = nextNo,
                                 ModifiedBy = _loginuser.UserId,
                                 ModifiedDate = now
                             })
                             .Where(s => s.FormTypeId == formTypeId && s.Ym == ym)
                             .ExecuteCommandAsync();
                }

                formNo = $"{prefix}-{ym}{nextNo:D4}";

                // 插入表单实例
                await _db.Insertable(new FormInstanceEntity
                {
                    FormId = formId,
                    FormTypeId = formTypeId,
                    FormNo = formNo,
                    FormStatus = FormStatus.PendingSubmit.ToEnumString(),
                    ApplicantUserId = _loginuser.UserId,
                    RuleId = 0,
                    CurrentStepId = startStepId,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = now
                }).ExecuteCommandAsync();

                // 插入待审记录
                await _db.Insertable(new PendingReviewEntity
                {
                    FormId = formId,
                    StepId = startStepId,
                    AppointmentType = AppointmentType.Actual.ToEnumString(),
                    ReviewUserId = _loginuser.UserId
                }).ExecuteCommandAsync();
            });

            return formId.ToString();
        }

        /// <summary>
        /// 匹配工作流规则
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<int> MatchWorkflowRuleAsync(long formTypeId, long formId)
        {
            var appPositionId = await _db.Queryable<UserInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .Where(user => user.UserId == _loginuser.UserId)
                                         .Select(user => user.PositionId)
                                         .FirstAsync();

            var ruleList = await _db.Queryable<WorkflowRuleEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(rule => rule.FormTypeId == formTypeId)
                                    .ToListAsync();

            long ruleId = 0;
            foreach (var rule in ruleList)
            {
                bool positionMatch = rule.PositionId == appPositionId;

                // 内联 Guidance 检查逻辑
                bool guidanceMatch = false;
                if (!string.IsNullOrEmpty(rule.Guidance))
                {
                    try
                    {
                        var method = GetType().GetMethod(rule.Guidance, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (method?.Invoke(this, new object[] { formId }) is Task<bool> task)
                            guidanceMatch = await task;
                    }
                    catch { /* 反射调用失败视为不匹配 */ }
                }

                // 优先级1：Position匹配且(Guidance为空或匹配)
                if (positionMatch && (string.IsNullOrEmpty(rule.Guidance) || guidanceMatch))
                {
                    ruleId = rule.RuleId;
                    continue;
                }

                // 优先级2：仅Guidance匹配
                if (ruleId == 0 && !positionMatch && guidanceMatch)
                {
                    ruleId = rule.RuleId;
                }

                // 优先级3：Position和Guidance都为空
                if (ruleId == 0 && rule.PositionId == 0 && string.IsNullOrEmpty(rule.Guidance))
                {
                    ruleId = rule.RuleId;
                }
            }
            return await _db.Updateable<FormInstanceEntity>()
                            .SetColumns(instance => new FormInstanceEntity
                            {
                                RuleId = ruleId
                            }).Where(instance => instance.FormId == formId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 保存表单实例
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveFormInstance(long formId)
        {
            var formTypeId = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(instance => instance.FormId == formId)
                                      .Select(instance => instance.FormTypeId)
                                      .FirstAsync();

            // 匹配工作流规则
            var ruleId = await MatchWorkflowRuleAsync(formTypeId, formId);

            // 更新表单实例
            return await _db.Updateable<FormInstanceEntity>()
                            .SetColumns(f => new FormInstanceEntity
                            {
                                ModifiedBy = _loginuser.UserId,
                                ModifiedDate = DateTime.Now
                            }).Where(instance => instance.FormId == formId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询附件列表
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormAttachmentDto>> GetAttachmentList(long formId)
        {
            var list = await _db.Queryable<FormAttachmentEntity>()
                                .With(SqlWith.NoLock)
                                .Where(formfile => formfile.FormId == formId)
                                .ToListAsync();
            return list.Adapt<List<FormAttachmentDto>>();
        }

        /// <summary>
        /// 新增附件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAttachment(FormAttachmentEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public async Task<int> DeleteAttachment(long attachmentId)
        {
            return await _db.Deleteable<FormAttachmentEntity>()
                            .Where(attach => attach.AttachmentId == attachmentId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询审批记录列表
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormReviewRecordDto>> GetReviewRecordList(long formId)
        {
            var list = await _db.Queryable<FormReviewRecordEntity>()
                                .With(SqlWith.NoLock)
                                .InnerJoin<WorkflowStepEntity>((record, step) => record.StepId == step.StepId)
                                .InnerJoin<DictionaryInfoEntity>((record, step, reviewresult) => reviewresult.DicType == "ReviewResult" && record.ReviewResult == reviewresult.DicCode)
                                .LeftJoin<WorkflowStepEntity>((record, step, reviewresult, rejectstep) => record.RejectStepId == rejectstep.StepId)
                                .InnerJoin<DictionaryInfoEntity>((record, step, reviewresult, rejectstep, reivewtype) => reivewtype.DicType == "ReviewType" && record.ReviewType == reivewtype.DicCode)
                                .InnerJoin<DictionaryInfoEntity>((record, step, reviewresult, rejectstep, reivewtype, appointmenttype) => appointmenttype.DicType == "AppointmentType" && record.AppointmentType == appointmenttype.DicCode)
                                .InnerJoin<UserInfoEntity>((record, step, reviewresult, rejectstep, reivewtype, appointmenttype, originaluser) => record.OriginalUserId == originaluser.UserId)
                                .InnerJoin<UserInfoEntity>((record, step, reviewresult, rejectstep, reivewtype, appointmenttype, originaluser, operationUser) => record.OperationUserId == operationUser.UserId)
                                .Where((record, step, reviewresult, rejectstep, reivewtype, appointmenttype, originaluser, operationuser) => record.FormId == formId)
                                .OrderBy((record, step, reviewresult, rejectstep, reivewtype, appointmenttype, originaluser, operationuser) => record.ReviewDateTime)
                                .Select((record, step, reviewresult, rejectstep, reivewtype, appointmenttype, originaluser, operationuser) => new FormReviewRecordDto
                                {
                                    FormId = record.FormId,
                                    StepName = _lang.Locale == "zh-CN"
                                               ? step.StepNameCn
                                               : step.StepNameEn,
                                    ReviewResultName = _lang.Locale == "zh-CN"
                                               ? reviewresult.DicNameCn
                                               : reviewresult.DicNameEn,
                                    RejectStepName = _lang.Locale == "zh-CN"
                                               ? rejectstep.StepNameCn
                                               : rejectstep.StepNameEn,
                                    Comment = record.Comment,
                                    ReviewTypeName = _lang.Locale == "zh-CN"
                                               ? reivewtype.DicNameCn
                                               : reivewtype.DicNameEn,
                                    AppointmentTypeName = _lang.Locale == "zh-CN"
                                               ? appointmenttype.DicNameCn
                                               : appointmenttype.DicNameEn,
                                    OriginalUserName = _lang.Locale == "zh-CN"
                                               ? originaluser.UserNameCn
                                               : originaluser.UserNameEn,
                                    OperationUserName = _lang.Locale == "zh-CN"
                                               ? operationuser.UserNameCn
                                               : operationuser.UserNameEn,
                                    ReviewDateTime = record.ReviewDateTime,
                                }).ToListAsync();
            return list.Adapt<List<FormReviewRecordDto>>();
        }


        #region 请假单
        /// <summary>
        /// 请假天数小于2天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LessOver2(long formId)
        {
            var leave = await _db.Queryable<LeaveFormEntity>()
                                 .With(SqlWith.NoLock)
                                 .FirstAsync(leave => leave.FormId == formId);

            return leave != null && leave.LeaveDays <= 2;
        }

        /// <summary>
        /// 请假天数大于2天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> MoreOver2(long formId)
        {
            var leave = await _db.Queryable<LeaveFormEntity>()
                                 .With(SqlWith.NoLock)
                                 .FirstAsync(leave => leave.FormId == formId);

            return leave != null && leave.LeaveDays > 2;
        }
        #endregion
    }
}
