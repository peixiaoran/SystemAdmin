using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto
{
    /// <summary>
    /// 性别下拉框Dto
    /// </summary>
    public class GenderDropDto
    {
        /// <summary>
        /// 性别编码
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int GenderCode { get; set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        public string GenderName { get; set; } = string.Empty;
    }
}
