using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 国籍信息Dto
    /// </summary>
    public class NationalityInfoDto
    {
        /// <summary>
        /// 国籍Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long NationId { get; set; }

        /// <summary>
        /// 国籍名称（中文）
        /// </summary>
        public string NationNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 国籍名称（英文）
        /// </summary>
        public string NationNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
