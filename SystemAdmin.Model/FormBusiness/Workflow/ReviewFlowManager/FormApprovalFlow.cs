namespace SystemAdmin.Model.FormBusiness.Workflow.ReviewFlowManager
{
    /// <summary>
    /// 表单签核人员
    /// </summary>
    public class FormReviewFlow
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 是否跳过
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// 步骤签核人员
        /// </summary>
        public List<StepReviewUser> stepReviewUser { get; set; } = new List<StepReviewUser>();

        /// <summary>
        /// 目前状态（未审批、审批中、审批完成）
        /// </summary>
        public string Result { get; set; } = string.Empty;
    }
}
