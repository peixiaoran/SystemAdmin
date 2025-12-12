using SqlSugar;
using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto
{
    /// <summary>
    /// 系统菜单Dto
    /// </summary>
    public class SysMenuInfoDto
    {
        /// <summary>
        /// 菜单主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long MenuId { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ParentMenuId { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; } = string.Empty;

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; } = string.Empty;

        /// <summary>
        /// 子菜单集合
        /// </summary>
        [SugarColumn(IsIgnore = true, IsTreeKey = true)]
        public List<SysMenuInfoDto> MenuChildList { get; set; } = new List<SysMenuInfoDto>();
    }
}
