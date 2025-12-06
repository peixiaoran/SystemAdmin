using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 职级Dto
    /// </summary>
    public class UserPositionDto
    {
        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }

        /// <summary>
        /// 职级编码
        /// </summary>
        public string PositionNo { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称（中文）
        /// </summary>
        public string PositionNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称（英文）
        /// </summary>
        public string PositionNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职级描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
