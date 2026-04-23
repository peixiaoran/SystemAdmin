namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程规则步骤新增/修改类
    /// </summary>
    public class WorkflowRuleStepUpsert
    {
        /// <summary>
        /// 规则Id
        /// </summary>
        public string RuleId { get; set; } = string.Empty;

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public string CurrentStepId { get; set; } = string.Empty;

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
