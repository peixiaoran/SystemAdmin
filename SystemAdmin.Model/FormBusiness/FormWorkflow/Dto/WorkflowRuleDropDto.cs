using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 规则下拉框Dto
    /// </summary>
    public class WorkflowRuleDropDto
    {
        /// <summary>
        /// 规则Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RuleId { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; } = string.Empty;
    }
}
