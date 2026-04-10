using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询职级签至最大范围请求参数
    /// </summary>
    public class GetFormApprovalLimitPage : PageModel
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;
    }
}
