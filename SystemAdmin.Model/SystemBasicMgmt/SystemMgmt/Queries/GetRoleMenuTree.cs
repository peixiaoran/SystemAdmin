namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
    /// <summary>
    /// 查询角色菜单树请求参数
    /// </summary>
    public class GetRoleMenuTree
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; } = string.Empty;

        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;
    }
}
