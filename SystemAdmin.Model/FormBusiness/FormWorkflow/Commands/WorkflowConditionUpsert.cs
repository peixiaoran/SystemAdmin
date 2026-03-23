namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程条件新增/修改类
    /// </summary>
    public class WorkflowConditionUpsert
    {
        /// <summary>
        /// 流程条件Id
        /// </summary>
        public string ConditionId { get; set; } = string.Empty;

        /// <summary>
        /// 所属表单Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

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
