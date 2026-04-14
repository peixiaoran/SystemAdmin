using SqlSugar;
using System.Reflection;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;

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
        public async Task<string> InitializeFormInstance(string formTypeId)
        {
            var now = DateTime.Now; var ym = now.ToString("yyyyMM");
            var formTypeIdLong = long.Parse(formTypeId);
            var prefix = await _db.Queryable<FormTypeEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(formtype => formtype.FormTypeId == long.Parse(formTypeId))
                                  .Select(formtype => formtype.Prefix)
                                  .FirstAsync();

            // 1. 对当前表单类别 + 年月加应用锁，防止并发重复取号
            var lockKey = $"FormNo_{formTypeIdLong}_{ym}";
            await _db.Ado.ExecuteCommandAsync(@" 
                    EXEC sp_getapplock 
                    @Resource = @lockKey, 
                    @LockMode = 'Exclusive', 
                    @LockOwner = 'Transaction', 
                    @LockTimeout = 10000", new { lockKey });

            // 2. 查询当前流水（此时已被锁保护）
            var autoEntity = await _db.Queryable<FormSequenceEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(sequence => sequence.FormTypeId == formTypeIdLong && sequence.Ym == ym)
                                      .FirstAsync();
            int nextNo;
            if (autoEntity == null)
            {
                nextNo = 1;
                var entity = new FormSequenceEntity
                {
                    FormTypeId = formTypeIdLong,
                    Ym = ym,
                    Total = nextNo,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = now
                };
                await _db.Insertable(entity).ExecuteCommandAsync();
            }
            else
            {
                nextNo = autoEntity.Total + 1;
                autoEntity.Total = nextNo;
                autoEntity.ModifiedBy = _loginuser.UserId;
                autoEntity.ModifiedDate = now;
                await _db.Updateable(autoEntity)
                         .UpdateColumns(sequence => new 
                         { 
                             sequence.Total, 
                             sequence.ModifiedBy, 
                             sequence.ModifiedDate 
                         }).ExecuteCommandAsync();
            }
            // 3. 生成单号
            var formNo = $"{prefix}-{ym}{nextNo:D4}"; 
            var formId = SnowFlakeSingle.Instance.NextId();

            // 4. 定位表单分支
            long branchId = -1;
            var branch = _db.Queryable<WorkflowBranchEntity>()
                            .With(SqlWith.NoLock)
                            .Where(branch => branch.FormTypeId == long.Parse(formTypeId));
            foreach (var item in await branch.ToListAsync())
            {
                if (!string.IsNullOrEmpty(item.HandlerKey))
                {
                    var method = GetType().GetMethod(item.HandlerKey, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (method != null)
                    {
                        var result = method.Invoke(this, new object[] { formId });

                        if (result is Task<bool> taskResult)
                        {
                            var isMatch = await taskResult;
                            if (isMatch)
                            {
                                branchId = item.BranchId;
                            }
                        }
                    }
                }
            }
            if (branchId == -1)
            {
                branchId = await branch.Where(branch => branch.IsDefault == 1).Select(branch => branch.BranchId).FirstAsync();
            }

            // 5. 表单开始步骤Id
            var startStepId = await _db.Queryable<WorkflowStepEntity>().Where(step => step.IsStartStep == 1).Select(step => step.StepId).FirstAsync();

            // 6. 插入表单实例
            var formInstance = new FormInstanceEntity
            {
                FormId = formId,
                FormTypeId = formTypeIdLong,
                FormNo = formNo,
                FormStatus = FormStatus.PendingSubmission.ToEnumString(),
                ApplicantUserId = _loginuser.UserId,
                BranchId = branchId,
                CurrentStepId = startStepId,
                CreatedBy = _loginuser.UserId,
                CreatedDate = now
            };
            await _db.Insertable(formInstance).ExecuteCommandAsync();

            var pendingApproval = new PendingApprovalEntity
            {
                FormId = formId,
                CurrentStepId = startStepId,
                ApproveUserId = _loginuser.UserId,
            };
            await _db.Insertable(pendingApproval).ExecuteCommandAsync();
            return formId.ToString();
        }

        /// <summary>
        /// 保存表单实例
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveFormInstance(long formId)
        {
            // 1. 查询表单类型
            var formTypeId = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .Where(form => form.FormId == formId)
                                      .Select(form => form.FormTypeId)
                                      .FirstAsync();

            // 2. 定位表单分支
            var branch = _db.Queryable<WorkflowBranchEntity>()
                            .With(SqlWith.NoLock)
                            .Where(branch => branch.FormTypeId == formTypeId);
            long branchId = await branch.Where(branch => branch.IsDefault == 1).Select(branch => branch.BranchId).FirstAsync();
            foreach (var item in await branch.ToListAsync())
            {
                if (!string.IsNullOrEmpty(item.HandlerKey))
                {
                    var method = GetType().GetMethod(item.HandlerKey, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (method != null)
                    {
                        var result = method.Invoke(this, new object[] { formId });

                        if (result is Task<bool> taskResult)
                        {
                            var isMatch = await taskResult;
                            if (isMatch)
                            {
                                branchId = item.BranchId;
                            }
                        }
                    }
                }
            }

            // 3. 修改表单实例
            return await _db.Updateable<FormInstanceEntity>()
                            .SetColumns(forminfo => new FormInstanceEntity
                            {
                                BranchId = branchId,
                                ModifiedBy = _loginuser.UserId,
                                ModifiedDate = DateTime.Now
                            }).Where(forminfo => forminfo.FormId == formId)
                            .ExecuteCommandAsync();
        }



        #region 请假单
        /// <summary>
        /// 请假天数超过3天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> IsLeaveDaysOver3(long formId)
        {
            var leave = await _db.Queryable<LeaveFormEntity>()
                                 .With(SqlWith.NoLock)
                                 .FirstAsync(x => x.FormId == formId);

            return leave != null && leave.LeaveDays > 3;
        }
        #endregion
    }
}
