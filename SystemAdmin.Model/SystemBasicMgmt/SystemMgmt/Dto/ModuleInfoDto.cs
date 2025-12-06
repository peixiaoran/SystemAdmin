using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto
{
    /// <summary>
    /// 模块Dto
    /// </summary>
    public class ModuleInfoDto
    {
        /// <summary>
        /// 模块主键Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ModuleId { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（中文）
        /// </summary>
        public string ModuleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（英文）
        /// </summary>
        public string ModuleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 模块级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ModuleTypeId { get; set; }

        /// <summary>
        /// 模块级别名称
        /// </summary>
        public string ModuleTypeDesc { get; set; } = string.Empty;

        /// <summary>
        /// 模块图标
        /// </summary>
        public string ModuleIcon { get; set; } = string.Empty;

        /// <summary>
        /// 模块排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        public int IsVisible { get; set; }

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
