using System.Text.Json.Serialization;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    public class WorkflowStepEntityDto
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 表单步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 是否为开始步骤
        /// </summary>
        public int IsStartStep { get; set; }

        /// <summary>
        /// 架构级别（组织架构、执行级）
        /// </summary>
        public string ArchitectureLevel { get; set; } = string.Empty;

        /// <summary>
        /// 审批人选取方式（依组织架构、指定部门员工级别、指定员工、自定义）
        /// </summary>
        public int Assignment { get; set; }

        /// <summary>
        /// 签核方式（单签、会签）
        /// </summary>
        public int ApproveMode { get; set; }

        /// <summary>
        /// 是否催签
        /// </summary>
        public int IsReminderEnabled { get; set; }

        /// <summary>
        /// 催签间隔分钟
        /// </summary>
        public int ReminderIntervalMinutes { get; set; }

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
