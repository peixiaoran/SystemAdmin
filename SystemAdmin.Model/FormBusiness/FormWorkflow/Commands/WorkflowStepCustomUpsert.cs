namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程步骤自定义来源新增/修改类
    /// </summary>
    public class WorkflowStepCustomUpsert
    {
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
