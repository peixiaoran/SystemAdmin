namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询审批步骤来源请求参数
    /// </summary>
    public class GetFormStepDetails
    {
        /// <summary>
        /// 签核步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;
    }
}
