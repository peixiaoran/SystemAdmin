namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Queries
{
    /// <summary>
    /// 查询员工代理了哪些员工请求参数
    /// </summary>
    public class GetUserAgentProactiveList
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
