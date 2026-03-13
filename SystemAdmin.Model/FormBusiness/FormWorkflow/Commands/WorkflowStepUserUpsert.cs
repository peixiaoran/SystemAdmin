namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程步骤指定员工新增/修改类
    /// </summary>
    public class WorkflowStepUserUpsert
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
