using SqlSugar;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;

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
        /// 请假单-请假天数是否超过3天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanDay3(long formId)
        {
            var leaveInfo = await _db.Queryable<LeaveFormEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(leave => leave.FormId == formId)
                                     .FirstAsync();
            int days = (leaveInfo.LeaveEndTime - leaveInfo.LeaveStartTime).Days;
            return days >= 3;
        }

        /// <summary>
        /// 请假单-请假天数是否超过3天
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<bool> LeaveThanThreeDay(long formId)
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
