using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询流程条件信息请求参数
    /// </summary>
    public class GetWorkflowConditionPage : PageModel
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;
    }
}
