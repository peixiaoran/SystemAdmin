using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 职业Dto
    /// </summary>
    public class UserLaborDto
    {
        /// <summary>
        /// 职业Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LaborId { get; set; }

        /// <summary>
        /// 职业编号
        /// </summary>
        public string LaborNo { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称（中文）
        /// </summary>
        public string LaborNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称（英文）
        /// </summary>
        public string LaborNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职业描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
