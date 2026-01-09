namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 流程审批条件实体类
    /// </summary>
    public class WorkflowConditionEntity
    {
        /// <summary>
        /// 审批条件Id
        /// </summary>
        public long ConditionId { get; set; }

        /// <summary>
        /// 流程条件名称（中文）
        /// </summary>
        public string ConditionNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 流程条件名称（英文）
        /// </summary>
        public string ConditionNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 代码标记
        /// </summary>
        public string HandlerKey { get; set; } = string.Empty;

        /// <summary>
        /// 逻辑说明
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
