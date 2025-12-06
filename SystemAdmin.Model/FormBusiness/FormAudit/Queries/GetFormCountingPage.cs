using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormAudit.Queries
{
    /// <summary>
    /// 查询表单计数信息请求参数
    /// </summary>
    public class GetFormCountingPage : PageModel
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;
    }
}
