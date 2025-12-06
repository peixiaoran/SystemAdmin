using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 职业下拉框Dto
    /// </summary>
    public class UserLaborDropDto
    {
        /// <summary>
        /// 职业Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LaborId { get; set; }

        /// <summary>
        /// 职业编码
        /// </summary>
        public string LaborName { get; set; } = string.Empty;
    }
}
