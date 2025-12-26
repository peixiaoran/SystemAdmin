namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程审批步骤自定义来源新增/修改类
    /// </summary>
    public class WorkflowStepRuleUpsert
    {
        /// <summary>
        /// 审批步骤自定义Id
        /// </summary>
        public string StepRuleId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 代码标记
        /// </summary>
        public string HandlerKey { get; set; } = string.Empty;

        /// <summary>
        /// 逻辑说明
        /// </summary>
        public string LogicalExplanation { get; set; } = string.Empty;
    }
}
