using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class FormPermissionChecker
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;

        public FormPermissionChecker(CurrentUser loginuser, SqlSugarScope db)
        {
            _loginuser = loginuser;
            _db = db;
        }

        /// <summary>
        /// 验证是否有权申请
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<bool> CanApply(long formTypeId)
        {
            return await _db.Queryable<UserFormEntity>()
                            .With(SqlWith.NoLock)
                            .Where(userform => userform.FormGroupTypeId == formTypeId && userform.UserId == _loginuser.UserId)
                            .AnyAsync();
        }

        /// <summary>
        /// 验证是否有权查看
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> CanView(long formId)
        {
            return await _db.Queryable<FormReviewRecordEntity>()
                            .With(SqlWith.NoLock)
                            .Where(record => record.ReviewUserId == _loginuser.UserId)
                            .AnyAsync();
        }

        /// <summary>
        /// 验证是否有权审批
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> CanReview(long formId)
        {
            return await _db.Queryable<FormInstanceEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<PendingReviewEntity>((instance, pending) => instance.FormId == pending.FormId && instance.CurrentStepId == pending.CurrentStepId)
                            .Where((instance, pending) => instance.FormId == formId && pending.ReviewUserId == _loginuser.UserId)
                            .AnyAsync();
        }

        /// <summary>
        /// 验证是否有权作废
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> CanVoided(long formId)
        {
            return await _db.Queryable<FormInstanceEntity>()
                            .With(SqlWith.NoLock)
                            .Where(instance => instance.ApplicantUserId == _loginuser.UserId && (instance.FormStatus == FormStatus.PendingSubmit.ToEnumString() || instance.FormStatus == FormStatus.Rejected.ToEnumString()))
                            .AnyAsync();
        }
    }
}
