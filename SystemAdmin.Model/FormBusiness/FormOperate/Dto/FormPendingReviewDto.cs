namespace SystemAdmin.Model.FormBusiness.FormOperate.Dto
{
    /// <summary>
    /// 待审批列表Dto
    /// </summary>
    public class FormPendingReviewDto
    {
        /// <summary>
        /// 当前步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 待审批人身份
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// 待审批人身份名称
        /// </summary>
        public string AppointmentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 待审批人名称
        /// </summary>
        public string ReviewUserName { get; set; } = string.Empty;

        /// <summary>
        /// 代理人名称
        /// </summary>
        public string AgentUserName { get; set; } = string.Empty;
    }
}
