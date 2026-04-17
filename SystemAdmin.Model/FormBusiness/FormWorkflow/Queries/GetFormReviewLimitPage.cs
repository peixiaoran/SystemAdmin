using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询签核层级上限请求参数
    /// </summary>
    public class GetFormReviewLimitPage : PageModel
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;
    }
}
