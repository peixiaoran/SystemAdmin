namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程审批步骤指定员工新增/修改类
    /// </summary>
    public class WorkflowStepUserUpsert
    {
        /// <summary>
        /// 审批步骤指定员工Id
        /// </summary>
        public string StepUserId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 用户Ids
        /// </summary>
        public string UserIds { get; set; } = string.Empty;
    }
}
