using SqlSugar;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.FormBusiness.Forms.FormLifecycle
{
    public class WorkflowConditionFun
    {
        private readonly SqlSugarScope _db;

        public WorkflowConditionFun(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 请假单-请假人职级范围为师一至师五
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS1_5(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 11 && postiton.SortOrder <= 15)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级范围为师六至师十
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS6_10(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 6 && postiton.SortOrder <= 10)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级范围为师十一至师十三
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS11_13(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 3 && postiton.SortOrder <= 5)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假天数是否超过5天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanLeaveDay5(long formId)
        {
            var leaveInfo = await _db.Queryable<LeaveFormEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(leave => leave.FormId == formId)
                                     .FirstAsync();
            int days = (leaveInfo.LeaveEndTime - leaveInfo.LeaveStartTime).Days;
            return days >= 5;
        }
    }
}
