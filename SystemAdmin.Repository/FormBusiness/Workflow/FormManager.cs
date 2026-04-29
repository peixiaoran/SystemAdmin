using SqlSugar;
using System.Reflection;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    /// <summary>
    /// 表单基础
    /// </summary>
    public class FormManager
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;

        public FormManager(CurrentUser loginuser, SqlSugarScope db)
        {
            _loginuser = loginuser;
            _db = db;
        }

        /// <summary>
        /// 初始化表单
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<string> InitializeFormInstance(long formTypeId)
        {
            var now = DateTime.Now;
            var ym = now.ToString("yyyyMM");
            // 1. 生成表单编号
            var (formNo, prefix) = await GenerateFormNoAsync(formTypeId, ym, now);
            var formId = SnowFlakeSingle.Instance.NextId();

            // 2. 匹配工作流规则
            var ruleId = await MatchWorkflowRuleAsync(formTypeId, formId);

            // 3. 查询起始步骤
            var startStepId = await _db.Queryable<WorkflowStepEntity>()
                                       .Where(step => step.FormTypeId == formTypeId && step.IsStartStep == 1)
                                       .Select(step => step.StepId)
                                       .FirstAsync();
            // 4. 创建表单实例
            await _db.Insertable(new FormInstanceEntity
            {
                FormId = formId,
                FormTypeId = formTypeId,
                FormNo = formNo,
                FormStatus = FormStatus.PendingSubmit.ToEnumString(),
                ApplicantUserId = _loginuser.UserId,
                RuleId = ruleId,
                CurrentStepId = startStepId,
                CreatedBy = _loginuser.UserId,
                CreatedDate = now
            }).ExecuteCommandAsync();

            await _db.Insertable(new PendingReviewEntity
            {
                FormId = formId,
                CurrentStepId = startStepId,
                ReviewUserId = _loginuser.UserId
            }).ExecuteCommandAsync();

            return formId.ToString();
        }

        /// <summary>
        /// 生成单号
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="ym"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        private async Task<(string formNo, string prefix)> GenerateFormNoAsync(long formTypeId, string ym, DateTime now)
        {
            var prefix = await _db.Queryable<FormTypeEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(formtype => formtype.FormTypeId == formTypeId)
                                  .Select(formtype => formtype.Prefix)
                                  .FirstAsync();

            // 应用锁防止并发
            var lockKey = $"FormNo_{formTypeId}_{ym}";
            await _db.Ado.ExecuteCommandAsync(
                "EXEC sp_getapplock @Resource = @lockKey, @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 10000",
                new { lockKey });

            // 获取或创建流水号
            var sequence = await _db.Queryable<FormSequenceEntity>()
                                    .With(SqlWith.NoLock)
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
                sequence.Total = nextNo;
                sequence.ModifiedBy = _loginuser.UserId;
                sequence.ModifiedDate = now;
                await _db.Updateable(sequence)
                         .UpdateColumns(s => new { s.Total, s.ModifiedBy, s.ModifiedDate })
                         .ExecuteCommandAsync();
            }

            return ($"{prefix}-{ym}{nextNo:D4}", prefix);
        }

        /// <summary>
        /// 匹配工作流规则
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        private async Task<long> MatchWorkflowRuleAsync(long formTypeId, long formId)
        {
            var appPositionId = await _db.Queryable<UserInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .Where(u => u.UserId == _loginuser.UserId)
                                         .Select(u => u.PositionId)
                                         .FirstAsync();

            var ruleList = await _db.Queryable<WorkflowRuleEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(r => r.FormTypeId == formTypeId)
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
                    return rule.RuleId;

                // 优先级2：仅Guidance匹配
                if (ruleId == 0 && !positionMatch && guidanceMatch)
                    ruleId = rule.RuleId;

                // 优先级3：Position和Guidance都为空
                if (ruleId == 0 && rule.PositionId == 0 && string.IsNullOrEmpty(rule.Guidance))
                    ruleId = rule.RuleId;
            }

            return ruleId;
        }

        /// <summary>
        /// 保存表单实例
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveFormInstance(long formId)
        {
            var formTypeId = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(f => f.FormId == formId)
                                      .Select(f => f.FormTypeId)
                                      .FirstAsync();

            // 匹配工作流规则
            var ruleId = await MatchWorkflowRuleAsync(formTypeId, formId);

            // 更新表单实例
            return await _db.Updateable<FormInstanceEntity>()
                            .SetColumns(f => new FormInstanceEntity
                            {
                                RuleId = ruleId,
                                ModifiedBy = _loginuser.UserId,
                                ModifiedDate = DateTime.Now
                            }).Where(f => f.FormId == formId)
                            .ExecuteCommandAsync();
        }



        #region 请假单
        /// <summary>
        /// 请假天数超过3天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> IsLeaveDaysOver2(long formId)
        {
            var leave = await _db.Queryable<LeaveFormEntity>()
                                 .With(SqlWith.NoLock)
                                 .FirstAsync(leave => leave.FormId == formId);

            return leave != null && leave.LeaveDays <= 2;
        }
        #endregion
    }
}
