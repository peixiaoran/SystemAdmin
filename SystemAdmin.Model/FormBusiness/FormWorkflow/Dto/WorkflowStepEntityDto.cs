using System.Text.Json.Serialization;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 步骤实体Dto
    /// </summary>
    public class WorkflowStepEntityDto
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 表单步骤名称（中文）
        /// </summary>
        public string StepNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单步骤名称（英文）
        /// </summary>
        public string StepNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否为开始步骤
        /// </summary>
        public int IsStartStep { get; set; }

        /// <summary>
        /// 架构级别（组织架构、执行级）
        /// </summary>
        public string ArchitectureLevel { get; set; } = string.Empty;

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
        /// 签核步骤组织架构来源实体
        /// </summary>
        public WorkflowStepOrgEntity workflowStepOrgEntity { get; set; } = new WorkflowStepOrgEntity();

        /// <summary>
        /// 签核步骤组织架构来源实体
        /// </summary>
        public WorkflowStepDeptUserEntity workflowStepDeptUserEntity { get; set; } = new WorkflowStepDeptUserEntity();

        /// <summary>
        /// 签核步骤指定员工来源实体
        /// </summary>
        public WorkflowStepUserEntity workflowStepUserEntity { get; set; } = new WorkflowStepUserEntity();

        /// <summary>
        /// 签核步骤自定义来源实体
        /// </summary>
        public WorkflowStepCustomEntity workflowStepApproverCustomEntity { get; set; } = new WorkflowStepCustomEntity();
    }
}
