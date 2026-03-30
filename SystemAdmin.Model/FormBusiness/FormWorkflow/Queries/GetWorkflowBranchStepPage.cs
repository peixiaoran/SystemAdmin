using SqlSugar;
namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询流程分支步骤信息请求参数
    /// </summary>
    public class GetWorkflowBranchStepPage : PageModel
    {
        /// <summary>
        /// 流程分支Id
        /// </summary>
        public string BranchId { get; set; } = string.Empty;
    }
}
