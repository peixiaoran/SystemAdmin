using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Entity
{
    /// <summary>
    /// 菜单实体类
    /// </summary>
    [SugarTable("[Basic].[MenuInfo]")]
    public class SysMenuInfoEntity
    {
        /// <summary>
        /// 菜单主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long MenuId { get; set; }

        /// <summary>
        /// 模块父节点Id
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public long ParentMenuId { get; set; }

        /// <summary>
        ///  菜单编码
        /// </summary>
        public string MenuCode { get; set; } = string.Empty;

        /// <summary>
        ///  菜单名称（中文）
        /// </summary>
        public string MenuNameCn { get; set; } = string.Empty;

        /// <summary>
        ///  菜单名称（中文）
        /// </summary>
        public string MenuNameEn { get; set; } = string.Empty;

        /// <summary>
        ///  菜单路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        ///  菜单图标
        /// </summary>
        public string MenuIcon { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 对应API路由
        /// </summary>
        public string RoutePath { get; set; } = string.Empty;

        /// <summary>
        /// 子节点集合
        /// </summary>
        [SugarColumn(IsIgnore = true, IsTreeKey = true)]
        public List<SysMenuInfoEntity> MenuChildList { get; set; } = new List<SysMenuInfoEntity>();
    }
}
