using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.Auth.Dto
{
    /// <summary>
    /// 系统模块Dto
    /// </summary>
    public class SysModuleInfoDto
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

        /// <summary>
        /// 模块图标
        /// </summary>
        public string ModuleIcon { get; set; } = string.Empty;

        /// <summary>
        /// 模块路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
