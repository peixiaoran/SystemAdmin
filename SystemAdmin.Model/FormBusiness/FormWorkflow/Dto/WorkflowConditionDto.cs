using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 流程条件Dto
    /// </summary>
    public class WorkflowConditionDto
    {
        /// <summary>
        /// 流程条件Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ConditionId { get; set; }

        /// <summary>
        /// 所属表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 流程条件名称（中文）
        /// </summary>
        public string ConditionNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 流程条件名称（英文）
        /// </summary>
        public string ConditionNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 代码标记
        /// </summary>
        public string HandlerKey { get; set; } = string.Empty;

        /// <summary>
        /// 逻辑说明
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
