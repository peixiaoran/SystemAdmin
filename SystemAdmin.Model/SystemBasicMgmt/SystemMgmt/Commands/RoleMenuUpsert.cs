namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Commands
{
    /// <summary>
    /// 角色菜单更新类
    /// </summary>
    public class RoleMenuUpsert
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; } = string.Empty;

        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;

        /// <summary>
        /// 选中菜单Ids
        /// </summary>
        public List<string> SelectedMenuIds { get; set; } = new List<string>();
    }
}
