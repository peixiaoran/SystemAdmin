namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Commands
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
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 代理结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;
    }
}
