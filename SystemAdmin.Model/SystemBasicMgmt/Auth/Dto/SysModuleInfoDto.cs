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
        /// 模块名称（中文）
        /// </summary>
        public string ModuleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（英文）
        /// </summary>
        public string ModuleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 模块图标
        /// </summary>
        public string ModuleIcon { get; set; } = string.Empty;

        /// <summary>
        /// 模块路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 备注（中文）
        /// </summary>
        public string RemarkCh { get; set; } = string.Empty;

        /// <summary>
        /// 备注（英文）
        /// </summary>
        public string RemarkEn { get; set; } = string.Empty;
    }
}
