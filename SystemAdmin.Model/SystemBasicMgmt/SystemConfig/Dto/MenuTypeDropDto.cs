using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto
{
    /// <summary>
    /// 菜单类型下拉框Dto
    /// </summary>
    public class MenuTypeDropDto
    {
        /// <summary>
        /// 菜单类型编码
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int MenuTypeCode { get; set; }

        /// <summary>
        /// 菜单类型名称
        /// </summary>
        public string MenuTypeName { get; set; } = string.Empty;
    }
}
