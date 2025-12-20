namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries
{
    /// <summary>
    /// 查询员工绑定表单请求参数
    /// </summary>
    public class GetUserFormBindViewTree
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
