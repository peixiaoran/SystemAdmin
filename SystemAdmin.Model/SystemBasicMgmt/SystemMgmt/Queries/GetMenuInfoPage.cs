using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
    /// <summary>
    /// 查询菜单分页请求参数
    /// </summary>
    public class GetMenuInfoPage : PageModel
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; } = string.Empty;

        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;
        
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public string ParentMenuId { get; set; } = string.Empty;

        /// <summary>
        /// 对应API路由
        /// </summary>
        public string RoutePath { get; set; } = string.Empty;
    }
}
