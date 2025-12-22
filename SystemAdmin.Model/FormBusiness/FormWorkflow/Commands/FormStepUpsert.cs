namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 签核步骤新增/修改类
    /// </summary>
    public class FormStepUpsert
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
        /// 审批人选取方式（依组织架构、指定部门人员级别、指定人员、自定义）
        /// </summary>
        public int Assignment { get; set; }

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
