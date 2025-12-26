using System.Text.Json.Serialization;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 流程审批步骤Dto
    /// </summary>
    public class WorkflowStepPageDto
    {
        /// <summary>
        /// 流程步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 流程步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人选取方式（依组织架构、指定部门员工级别、指定员工、自定义）
        /// </summary>
        public int Assignment { get; set; }

        /// <summary>
        /// 审批人选取方式名称
        /// </summary>
        public string AssignmentName { get; set; } = string.Empty;

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
