using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto
{
    /// <summary>
    /// 表单组别Dto
    /// </summary>
    public class FormGroupDto
    {
        /// <summary>
        /// 表单组别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormGroupId { get; set; }

        /// <summary>
        /// 表单组别名称（中文）
        /// </summary>
        public string FormGroupNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别名称（英文）
        /// </summary>
        public string FormGroupNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 表单描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
