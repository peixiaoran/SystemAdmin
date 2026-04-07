using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Entity
{
    /// <summary>
    /// 表单待签核人表
    /// </summary>
    [SugarTable("[Form].[PendingApproval]")]
    public class PendingApprovalEntity
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public long CurrentStepId { get; set; }

        /// <summary>
        /// 待签核员工Id
        /// </summary>
        public long ApproveUserId { get; set; }
    }
}
