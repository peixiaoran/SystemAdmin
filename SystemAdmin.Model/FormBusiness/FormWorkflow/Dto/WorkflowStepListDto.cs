using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 流程审批步骤Dto
    /// </summary>
    public class WorkflowStepListDto
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
        /// 步骤指派规则
        /// </summary>
        public string Assignment { get; set; } = string.Empty;

        /// <summary>
        /// 步骤指派规则名称
        /// </summary>
        public string AssignmentName { get; set; } = string.Empty;
    }
}
