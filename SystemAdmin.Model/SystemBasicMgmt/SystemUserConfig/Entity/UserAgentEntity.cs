using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity
{
    /// <summary>
    /// 员工代理实体类
    /// </summary>
    [SugarTable("[Basic].[UserAgent]")]
    public class UserAgentEntity
    {
        /// <summary>
        /// 被代理员工Id
        /// </summary>
        public long SubstituteUserId { get; set; }

        /// <summary>
        /// 代理员工Id
        /// </summary>
        public long AgentUserId { get; set; }

        /// <summary>
        /// 代理开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 代理结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 创建人Id
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人Id
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
