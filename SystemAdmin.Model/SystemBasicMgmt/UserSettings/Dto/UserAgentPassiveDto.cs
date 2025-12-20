using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto
{
    /// <summary>
    /// 员工被哪些人代理Dto
    /// </summary>
    public class UserAgentPassiveDto
    {
        /// <summary>
        /// 被代理人Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long SubstituteUserId { get; set; }

        /// <summary>
        /// 代理人Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long AgentUserId { get; set; }

        /// <summary>
        /// 代理人工号
        /// </summary>
        public string AgentUserNo { get; set; } = string.Empty;

        /// <summary>
        /// 代理人姓名
        /// </summary>
        public string AgentUserName { get; set; } = string.Empty;

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
