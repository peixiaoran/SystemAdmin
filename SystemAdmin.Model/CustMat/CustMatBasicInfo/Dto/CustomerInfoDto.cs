using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto
{
    /// <summary>
    /// 客户信息Dto
    /// </summary>
    public class CustomerInfoDto
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long CustomerId { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称（中文）
        /// </summary>
        public string CustomerNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称（英文）
        /// </summary>
        public string CustomerNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 客户描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
