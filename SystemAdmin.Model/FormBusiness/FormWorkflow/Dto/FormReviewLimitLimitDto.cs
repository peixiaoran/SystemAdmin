using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 签核层级上限Dto
    /// </summary>
    public class FormReviewLimitDto
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }

        /// <summary>
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 签核最高职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long MaxPositionId { get; set; }

        /// <summary>
        /// 签核最高职级名称
        /// </summary>
        public string MaxPositionName { get; set; } = string.Empty;
    }
}
