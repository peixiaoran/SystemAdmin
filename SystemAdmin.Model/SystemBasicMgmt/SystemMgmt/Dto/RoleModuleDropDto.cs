using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto
{
    /// <summary>
    /// 角色绑定模块下拉框Dto
    /// </summary>
    public class RoleModuleDropDto
    {
        /// <summary>
        /// 模块主键Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ModuleId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;
    }
}
