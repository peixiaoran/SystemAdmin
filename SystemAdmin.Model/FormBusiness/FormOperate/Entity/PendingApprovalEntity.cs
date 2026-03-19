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
        /// 签核身份
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// 待签核员工Id
        /// </summary>
        public long ApproveUserId { get; set; }
    }
}
