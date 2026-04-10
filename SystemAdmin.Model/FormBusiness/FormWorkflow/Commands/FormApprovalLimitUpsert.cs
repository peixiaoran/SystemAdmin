namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 职级签至最大范围新增/修改类
    /// </summary>
    public class FormApprovalLimitUpsert
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 签核最高职级Id
        /// </summary>
        public long MaxPositionId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
