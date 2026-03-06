namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Commands
{
    /// <summary>
    /// 员工代理配置新增/修改类
    /// </summary>
    public class UserAgentUpsert
    {
        /// <summary>
        /// 被代理人Id
        /// </summary>
        public string SubstituteUserId { get; set; } = string.Empty;

        /// <summary>
        /// 代理人Id
        /// </summary>
        public string AgentUserId { get; set; } = string.Empty;

        /// <summary>
        /// 代理开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 代理结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
