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
            // 检查当前用户是否是申请人
            bool isApplicant = await _db.Queryable<FormInstanceEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(instance => instance.FormId == formId && instance.ApplicantUserId == _loginuser.UserId)
                                        .AnyAsync();

            if (isApplicant)
                return true;

            // 检查当前用户是否在待审核列表中
            bool isReviewer = await _db.Queryable<PendingReviewEntity>()
                                       .With(SqlWith.NoLock)
                                       .LeftJoin<UserAgentEntity>((pending, useragent) => pending.ReviewUserId == useragent.SubstituteUserId && useragent.StartTime <= DateTime.Now && useragent.EndTime >= DateTime.Now)
                                       .Where((pending, useragent) => pending.FormId == formId && (pending.ReviewUserId == _loginuser.UserId || useragent.AgentUserId == _loginuser.UserId))
                                       .AnyAsync();

            if (isReviewer)
                return true;

            // 检查当前用户是否曾经参与过审批
            bool hasReviewRecord = await _db.Queryable<FormReviewRecordEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(record => record.FormId == formId && (record.ReviewUserId == _loginuser.UserId || record.OriginalUserId == _loginuser.UserId))
                                            .AnyAsync();

            return hasReviewRecord;
        }

        /// <summary>
        /// 验证是否有权审批
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> CanReview(long formId)
        {
            bool isReviewer = await _db.Queryable<PendingReviewEntity>()
                                       .With(SqlWith.NoLock)
                                       .LeftJoin<UserAgentEntity>((pending, useragent) => pending.ReviewUserId == useragent.SubstituteUserId && useragent.StartTime <= DateTime.Now && useragent.EndTime >= DateTime.Now)
                                       .Where((pending, useragent) => pending.FormId == formId && (pending.ReviewUserId == _loginuser.UserId || useragent.AgentUserId == _loginuser.UserId))
                                       .AnyAsync();
            return isReviewer;
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
