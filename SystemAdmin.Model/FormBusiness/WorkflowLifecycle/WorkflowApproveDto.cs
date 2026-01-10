namespace SystemAdmin.Model.FormBusiness.WorkflowLifecycle
{
    /// <summary>
    /// 流程审批人员Dto
    /// </summary>
    public class WorkflowApproveDto
    {
        /// <summary>
        /// 审批步骤Id
        /// </summary>
        public long Step { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 符合条件Id
        /// </summary>
        public long ConditionId { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
