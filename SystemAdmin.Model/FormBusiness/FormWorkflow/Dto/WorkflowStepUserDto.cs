using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 步骤指定员工Dto
    /// </summary>
    public class WorkflowStepUserDto
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
        /// 员工Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }
    }
}
