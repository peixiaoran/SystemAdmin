using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto
{
    /// <summary>
    /// 角色模块Dto
    /// </summary>
    public class RoleModuleDto
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ModuleId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 是否绑定
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
