using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto
{
    /// <summary>
    /// 厂商信息Dto
    /// </summary>
    public class ManufacturerInfoDto
    {
        /// <summary>
        /// 厂商Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ManufacturerId { get; set; }

        /// <summary>
        /// 厂商编码
        /// </summary>
        public string ManufacturerCode { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称（中文）
        /// </summary>
        public string ManufacturerNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称（英文）
        /// </summary>
        public string ManufacturerNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 厂商邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 厂商传真
        /// </summary>
        public string Fax { get; set; } = string.Empty;

        /// <summary>
        /// 厂商描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
