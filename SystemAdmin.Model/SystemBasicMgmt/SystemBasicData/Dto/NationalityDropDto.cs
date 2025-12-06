using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 国籍下拉框Dto
    /// </summary>
    public class NationalityDropDto
    {
        /// <summary>
        /// 国籍Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long NationId { get; set; }

        /// <summary>
        /// 国籍名称（中文）
        /// </summary>
        public string NationName { get; set; } = string.Empty;
    }
}
