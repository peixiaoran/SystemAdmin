using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto
{
    /// <summary>
    /// 字典Dto
    /// </summary>
    public class DictionaryInfoDto
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DicId { get; set; }

        /// <summary>
        /// 所属模块Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ModuleId { get; set; }

        /// <summary>
        /// 所属模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DicType { get; set; } = string.Empty;

        /// <summary>
        /// 字典编码
        /// </summary>
        public string DicCode { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称（中文）
        /// </summary>
        public string DicNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称（英文）
        /// </summary>
        public string DicNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 字典排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
