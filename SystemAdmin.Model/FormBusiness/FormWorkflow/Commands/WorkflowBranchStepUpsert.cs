namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程分支新增/修改类
    /// </summary>
    public class WorkflowBranchStepUpsert
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        public string BranChId { get; set; } = string.Empty;

        /// <summary>
        /// 分支Id
        /// </summary>
        public string BranchId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 下一步骤Id
        /// </summary>
        public string NextStepId { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
