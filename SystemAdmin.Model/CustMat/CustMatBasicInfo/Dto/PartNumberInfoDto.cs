using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto
{
    /// <summary>
    /// 料号信息Dto
    /// </summary>
    public class PartNumberInfoDto
    {
        /// <summary>
        /// 料号Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PartNumberId { get; set; }

        /// <summary>
        /// 所属厂商Id
        /// </summary>
        public long ManufacturerId { get; set; }

        /// <summary>
        /// 所属厂商名称
        /// </summary>
        public long ManufacturerName { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string PartNumberNo { get; set; } = string.Empty;

        /// <summary>
        /// 品名
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 规格
        /// </summary>
        public string Specifications { get; set; } = string.Empty;
    }
}
