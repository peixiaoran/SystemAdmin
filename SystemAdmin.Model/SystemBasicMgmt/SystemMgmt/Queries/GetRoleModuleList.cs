namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
    /// <summary>
    /// 查询角色模块列表请求参数
    /// </summary>
    public class GetRoleModuleList
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; } = string.Empty;
    }
}
