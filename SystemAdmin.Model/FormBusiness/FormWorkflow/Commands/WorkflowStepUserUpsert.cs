namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 步骤指定员工新增/修改类
    /// </summary>
    public class WorkflowStepUserUpsert
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
