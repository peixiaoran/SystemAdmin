namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 审批步骤新增/修改类
    /// </summary>
    public class WorkflowStepUpsert
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别Id
        /// </summary>
        public string FormGroupId { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤名称（中文）
        /// </summary>
        public string StepNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 步骤名称（英文）
        /// </summary>
        public string StepNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否为开始步骤
        /// </summary>
        public int IsStartStep { get; set; }

        /// <summary>
        /// 步骤指派规则（依组织架构、指定部门员工级别、指定员工、自定义）
        /// </summary>
        public string Assignment { get; set; } = string.Empty;

        /// <summary>
        /// 签核方式（单签、会签、或签）
        /// </summary>
        public string ApproveMode { get; set; } = string.Empty;

        /// <summary>
        /// 是否催签
        /// </summary>
        public int IsReminderEnabled { get; set; }

        /// <summary>
        /// 催签间隔分钟
        /// </summary>
        public int ReminderIntervalMinutes { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 步骤组织架构新增/修改类
        /// </summary>
        public WorkflowStepOrgUpsert stepOrgUpsert { get; set; } = new WorkflowStepOrgUpsert();

        /// <summary>
        /// 步骤指定部门员工级别新增/修改类
        /// </summary>
        public WorkflowStepDeptUserUpsert stepDeptUserUpsert { get; set; } = new WorkflowStepDeptUserUpsert();

        /// <summary>
        /// 步骤指定员工新增/修改类
        /// </summary>
        public WorkflowStepUserUpsert stepUserUpsert { get; set; } = new WorkflowStepUserUpsert();

        /// <summary>
        /// 步骤自定义新增/修改类
        /// </summary>
        public WorkflowStepCustomUpsert stepCustomUpsert { get; set; } = new WorkflowStepCustomUpsert();
    }
}
