namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程审批步骤组织架构新增/修改类
    /// </summary>
    public class WorkflowStepOrgUpsert
    {
        /// <summary>
        /// 审批步骤组织架构Id
        /// </summary>
        public string StepOrgId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 部门来源Ids
        /// </summary>
        public long DeptLeaveId { get; set; }

        /// <summary>
        /// 职级来源Ids
        /// </summary>
        public string PositionIds { get; set; } = string.Empty;
    }
}
