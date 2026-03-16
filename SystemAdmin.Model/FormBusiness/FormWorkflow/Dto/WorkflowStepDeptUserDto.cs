using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 步骤指定部门员工级别Dto
    /// </summary>
    public class WorkflowStepDeptUserDto
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }
    }
}
