using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 步骤组织架构Dto
    /// </summary>
    public class WorkflowStepOrgDto
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 部门级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DeptLeaveId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }
    }
}
