using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 流程审批步骤Dto
    /// </summary>
    public class WorkflowStepPageDto
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 步骤选人方式名称
        /// </summary>
        public string AssignmentName { get; set; } = string.Empty;

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 步骤条件分支集合
        /// </summary>
        public List<WorkflowStepConditionDto> StepConditionList { get; set; } = new List<WorkflowStepConditionDto>();
    }
}
