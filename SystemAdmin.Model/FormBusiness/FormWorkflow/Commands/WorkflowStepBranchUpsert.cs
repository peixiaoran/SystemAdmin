namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程步骤分支新增/修改类
    /// </summary>
    public class WorkflowStepBranchUpsert
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        public string BranChId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 条件Id
        /// </summary>
        public string ConditionId { get; set; } = string.Empty;

        /// <summary>
        /// 同时符合多条件时，是否执行此条件
        /// </summary>
        public int ExecuteMatched { get; set; }

        /// <summary>
        /// 下一步骤Id
        /// </summary>
        public string NextStepId { get; set; } = string.Empty;
    }
}
