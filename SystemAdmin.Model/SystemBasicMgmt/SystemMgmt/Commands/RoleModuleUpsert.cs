namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Commands
{
    /// <summary>
    /// 角色模块更新类
    /// </summary>
    public class RoleModuleUpsert
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; } = string.Empty;

        /// <summary>
        /// 选中模块Ids
        /// </summary>
        public List<string> SelectedModuleIds { get; set; } = new List<string>();

        /// <summary>
        /// 未选中模块Ids
        /// </summary>
        public List<string> UnSelectedModuleIds { get; set; } = new List<string>();
    }
}
