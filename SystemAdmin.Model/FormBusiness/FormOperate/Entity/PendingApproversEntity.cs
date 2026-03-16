using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Entity
{
    /// <summary>
    /// 表单待签核人员实体类
    /// </summary>
    [SugarTable("[Form].[PendingApprovers]")]
    public class PendingApproversEntity
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 待签核员工Id
        /// </summary>
        public long ApproveUserId { get; set; }
    }
}
