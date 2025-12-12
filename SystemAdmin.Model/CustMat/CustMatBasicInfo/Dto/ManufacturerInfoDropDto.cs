using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto
{
    /// <summary>
    /// 厂商信息下拉框Dto
    /// </summary>
    public class ManufacturerInfoDropDto
    {
        /// <summary>
        /// 厂商Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ManufacturerId { get; set; }

        /// <summary>
        /// 厂商名称
        /// </summary>
        public long ManufacturerName { get; set; }
    }
}
