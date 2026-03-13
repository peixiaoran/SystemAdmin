namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程步骤组织架构新增/修改类
    /// </summary>
    public class WorkflowStepOrgUpsert
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DeptLeaveId { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        public string PositionId { get; set; } = string.Empty;
    }
}
