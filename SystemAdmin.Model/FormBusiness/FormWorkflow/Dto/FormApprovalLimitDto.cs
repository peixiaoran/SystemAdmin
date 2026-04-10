namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 职级签至最大范围Dto
    /// </summary>
    public class FormApprovalLimitDto
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
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 签核最高职级Id
        /// </summary>
        public long MaxPositionId { get; set; }

        /// <summary>
        /// 签核最高职级名称
        /// </summary>
        public string MaxPositionName { get; set; } = string.Empty;
    }
}
