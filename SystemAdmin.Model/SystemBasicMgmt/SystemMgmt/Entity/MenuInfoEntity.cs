using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity
{
    /// <summary>
    /// 系统菜单实体
    /// </summary>
    [SugarTable("[Basic].[MenuInfo]")]
    public class MenuInfoEntity
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long MenuId { get; set; }

        /// <summary>
        /// 所属模块Id
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// 父菜单Id
        /// </summary>
        public long ParentMenuId { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称（中文）
        /// </summary>
        public string MenuNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称（英文）
        /// </summary>
        public string MenuNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 菜单类型（2、一级菜单 3、二级菜单）
        /// </summary>
        public string MenuType { get; set; } = string.Empty;

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsVisible { get; set; }

        /// <summary>
        /// 对应API路由
        /// </summary>
        public string RoutePath { get; set; } = string.Empty;

        /// <summary>
        /// 前端重定向
        /// </summary>
        public string Redirect { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<MenuInfoEntity> MenuChildList { get; set; } = new List<MenuInfoEntity>();
    }
}
