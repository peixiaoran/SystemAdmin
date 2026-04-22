namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程规则新增/修改类
    /// </summary>
    public class WorkflowRuleUpsert
    {
        /// <summary>
        /// 规则Id
        /// </summary>
        public string RuleId { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 规则名称（中文）
        /// </summary>
        public string RuleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 规则名称（英文）
        /// </summary>
        public string RuleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        public string PositionId { get; set; } = string.Empty;

        /// <summary>
        /// 导向
        /// </summary>
        public string Guidance { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
