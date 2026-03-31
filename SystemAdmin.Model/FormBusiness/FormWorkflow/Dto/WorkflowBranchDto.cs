using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 分支Dto
    /// </summary>
    public class WorkflowBranchDto
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BranchId { get; set; }

        /// <summary>
        /// 所属表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 分支名称（中文）
        /// </summary>
        public string BranchNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 分支名称（英文）
        /// </summary>
        public string BranchNameEn { get; set; } = string.Empty;

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
