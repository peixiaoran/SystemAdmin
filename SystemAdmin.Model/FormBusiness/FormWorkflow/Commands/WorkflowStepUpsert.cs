namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程审批步骤新增/修改类
    /// </summary>
    public class WorkflowStepUpsert
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 表单步骤名称（中文）
        /// </summary>
        public string StepNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单步骤名称（英文）
        /// </summary>
        public string StepNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 架构级别（组织架构、执行级）
        /// </summary>
        public string ArchitectureLevel { get; set; } = string.Empty;

        /// <summary>
        /// 是否为开始步骤
        /// </summary>
        public int IsStartStep { get; set; }

        /// <summary>
        /// 审批人选取方式（依组织架构、指定部门员工级别、指定员工、自定义）
        /// </summary>
        public string Assignment { get; set; } = string.Empty;

        /// <summary>
        /// 签核方式（单签、会签）
        /// </summary>
        public string ApproveMode { get; set; } = string.Empty;

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 审批步骤组织架构新增/修改类
        /// </summary>
        public WorkflowStepOrgUpsert workflowStepOrgUpsert { get; set; } = new WorkflowStepOrgUpsert();

        /// <summary>
        /// 审批步骤指定部门员工级别新增/修改类
        /// </summary>
        public WorkflowStepDeptUserUpsert workflowStepDeptUserUpsert { get; set; } = new WorkflowStepDeptUserUpsert();

        /// <summary>
        /// 审批步骤指定员工新增/修改类
        /// </summary>
        public WorkflowStepUserUpsert workflowStepUserUpsert { get; set; } = new WorkflowStepUserUpsert();

        /// <summary>
        /// 审批步骤自定义来源新增/修改类
        /// </summary>
        public WorkflowStepRuleUpsert workflowStepRuleUpsert { get; set; } = new WorkflowStepRuleUpsert();
    }
}
