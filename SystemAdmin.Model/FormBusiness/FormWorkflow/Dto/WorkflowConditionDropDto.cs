using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 条件下拉Dto
    /// </summary>
    public class WorkflowConditionDropDto
    {
        /// <summary>
        /// 条件Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ConditionId { get; set; }

        /// <summary>
        /// 条件名称
        /// </summary>
        public string ConditionName { get; set; } = string.Empty;
    }
}
