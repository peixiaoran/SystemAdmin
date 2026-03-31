using SqlSugar;
namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询分支步骤请求参数
    /// </summary>
    public class GetWorkflowBranchStepPage : PageModel
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        public string BranchId { get; set; } = string.Empty;
    }
}
