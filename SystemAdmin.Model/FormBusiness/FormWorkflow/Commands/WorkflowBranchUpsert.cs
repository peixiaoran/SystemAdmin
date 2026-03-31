namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 分支新增/修改类
    /// </summary>
    public class WorkflowBranchUpsert
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        public string BranchId { get; set; } = string.Empty;

        /// <summary>
        /// 所属表单Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 分支名称（中文）
        /// </summary>
        public string BranchNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 分支名称（英文）
        /// </summary>
        public string BranchNameEn { get; set; } = string.Empty;

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
