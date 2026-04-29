using SqlSugar;
namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询规则步骤请求参数
    /// </summary>
    public class GetWorkflowRulePage : PageModel
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        public string PositionId { get; set; } = string.Empty;
    }
}
