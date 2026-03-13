namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程步骤指定部门员工级别新增/修改类
    /// </summary>
    public class WorkflowStepDeptUserUpsert
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        public string PositionId { get; set; } = string.Empty;
    }
}
