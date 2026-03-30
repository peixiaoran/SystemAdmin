using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询流程分支信息请求参数
    /// </summary>
    public class GetWorkflowBranchPage : PageModel
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;
    }
}
