namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询流程审批步骤信息实体请求参数
    /// </summary>
    public class GetWorkflowStepEntity
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;
    }
}
