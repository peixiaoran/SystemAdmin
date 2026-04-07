namespace SystemAdmin.Model.FormBusiness.Workflow.ApprovalFlowManager
{
    /// <summary>
    /// 表单签核人员
    /// </summary>
    public class FormApprovalFlow
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 是否跳过
        /// </summary>
        public int IsSkip { get; set; }

        /// <summary>
        /// 步骤签核人员
        /// </summary>
        public List<StepApprovalUser> stepApprovalUser { get; set; } = new List<StepApprovalUser>();

        /// <summary>
        /// 是否签核完成
        /// </summary>
        public int Result { get; set; }
    }
}
