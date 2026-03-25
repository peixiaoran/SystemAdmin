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
        /// 请假单-请假人职级范围为师一至师四
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS1_4(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 12 && postiton.SortOrder <= 15)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级范围为师五至师八
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS5_8(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 8 && postiton.SortOrder <= 11)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级范围为师九至师十
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS9_10(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 6 && postiton.SortOrder <= 7)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级为师十一
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS11(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder == 5)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级为师十二
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS12(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder == 4)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假人职级范围为师十三至师十四
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanPositionDivisionS2_3(long formId)
        {
            return await _db.Queryable<LeaveFormEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<UserInfoEntity>((leave, user) => leave.ApplicantUserId == user.UserId)
                            .InnerJoin<UserPositionEntity>((leave, user, postiton) => user.PositionId == postiton.PositionId)
                            .Where((leave, user, postiton) => leave.FormId == formId && postiton.SortOrder >= 2 && postiton.SortOrder <= 3)
                            .AnyAsync();
        }

        /// <summary>
        /// 请假单-请假天数是否超过3天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanLeaveDay3(long formId)
        {
            var leaveInfo = await _db.Queryable<LeaveFormEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(leave => leave.FormId == formId)
                                     .FirstAsync();
            int days = (leaveInfo.LeaveEndTime - leaveInfo.LeaveStartTime).Days;
            return days >= 3;
        }
    }
}
