using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
    /// <summary>
    /// 查询角色分页请求参数
    /// </summary>
    public class GetRoleInfoPage : PageModel
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
    }
}
