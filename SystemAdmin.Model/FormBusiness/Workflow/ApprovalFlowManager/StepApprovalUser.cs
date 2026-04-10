namespace SystemAdmin.Model.FormBusiness.Workflow.ApprovalFlowManager
{
    /// <summary>
    /// 步骤签核人员
    /// </summary>
    public class StepApprovalUser
    {
        /// <summary>
        /// 签核人员姓名 - 实或兼
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 签核人员姓名 - 代
        /// </summary>
        public string AgentUserName { get; set; } = string.Empty;

        /// <summary>
        /// 签核身份
        /// </summary>
        public string AppointmentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 目前状态（未审批、审批中、审批完成）
        /// </summary>
        public string Result { get; set; } = string.Empty;
    }
}
