namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries
{
    /// <summary>
    /// 查询员工被哪些员工代理请求参数
    /// </summary>
    public class GetUserAgentPassiveList
    {
        /// <summary>
        /// 被代理人Id
        /// </summary>
        public string SubstituteUserId { get; set; } = string.Empty;
    }
}
