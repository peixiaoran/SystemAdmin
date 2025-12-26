using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询流程审批步骤分页请求参数
    /// </summary>
    public class GetWorkflowStepPage : PageModel
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;
    }
}
