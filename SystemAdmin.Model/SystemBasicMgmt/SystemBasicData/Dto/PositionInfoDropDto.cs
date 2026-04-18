using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 职级下拉Dto
    /// </summary>
    public class PositionInfoDropDto
    {
        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }

        /// <summary>
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;
    }
}
