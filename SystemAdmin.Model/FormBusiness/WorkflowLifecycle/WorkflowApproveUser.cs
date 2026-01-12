namespace SystemAdmin.Model.FormBusiness.WorkflowLifecycle
{
    /// <summary>
    /// 流程审批人员Dto
    /// </summary>
    public class WorkflowApproveUser
    {
        /// <summary>
        /// 审批步骤Id
        /// </summary>
        public long StepId { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 员工Id
        /// </summary>
        public long AppmoveUserId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string AppmoveUserName { get; set; } = string.Empty;
    }
}
