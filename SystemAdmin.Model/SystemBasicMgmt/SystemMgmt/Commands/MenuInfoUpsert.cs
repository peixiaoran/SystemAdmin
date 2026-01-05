namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
    /// <summary>
    /// 菜单新增/修改类
    /// </summary>
    public class MenuInfoUpsert
    {
        /// <summary>
        /// 菜单主键Id
        /// </summary>
        public string MenuId { get; set; } = string.Empty;

        /// <summary>
        /// 父节点Id
        /// </summary>
        public string ParentMenuId { get; set; } = string.Empty;

        /// <summary>
        /// 模块父节点Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;

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
        /// 对应API路由
        /// </summary>
        public string RoutePath { get; set; } = string.Empty;

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
        /// 菜单路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }

        /// <summary>
        /// 前端重定向
        /// </summary>
        public string Redirect { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
