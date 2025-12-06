namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Commands
{
    /// <summary>
    /// 员工代理配置删除类
    /// </summary>
    public class UserAgentDel
    {
        /// <summary>
        /// 被代理员工Id
        /// </summary>
        public string SubstituteUserId { get; set; } = string.Empty;

        /// <summary>
        /// 代理员工Id
        /// </summary>
        public string AgentUserId { get; set; } = string.Empty;
    }
}
