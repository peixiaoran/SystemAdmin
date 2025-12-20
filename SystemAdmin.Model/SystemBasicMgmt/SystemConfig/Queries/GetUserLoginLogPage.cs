using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries
{
    /// <summary>
    /// 查询员工登录日志分页请求参数
    /// </summary>
    public class GetUserLoginLogPage : PageModel
    {
        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 操作状态
        /// </summary>
        public string StatusId { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;
    }
}
