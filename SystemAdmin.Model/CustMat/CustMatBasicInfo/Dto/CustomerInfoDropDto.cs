using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Dto
{
    /// <summary>
    /// 客户信息下拉框Dto
    /// </summary>
    public class CustomerInfoDropDto
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long CustomerId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public long CustomerName { get; set; }
    }
}
