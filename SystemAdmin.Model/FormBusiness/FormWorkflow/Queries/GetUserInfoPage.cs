using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询用户信息请求参数
    /// </summary>
    public class GetUserInfoPage : PageModel
    {
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
