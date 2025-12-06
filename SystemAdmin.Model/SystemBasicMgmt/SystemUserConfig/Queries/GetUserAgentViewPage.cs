using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Queries
{
    /// <summary>
    /// 查询员工代理人选择视图请求参数
    /// </summary>
    public class GetUserAgentViewPage : PageModel
    {
        /// <summary>
        /// 被代理员工Id
        /// </summary>
        public string SubstituteUserId { get; set; } = string.Empty;

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
