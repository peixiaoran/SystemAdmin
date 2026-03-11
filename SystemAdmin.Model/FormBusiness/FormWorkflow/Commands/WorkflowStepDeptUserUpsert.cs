namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程审批步骤指定部门员工级别新增/修改类
    /// </summary>
    public class WorkflowStepDeptUserUpsert
    {
        /// <summary>
        /// 审批步骤指定部门员工级别Id
        /// </summary>
        public string StepDeptUserId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 部门Ids
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 职级Ids
        /// </summary>
        public long PositionId { get; set; }
    }
}
