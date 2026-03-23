namespace SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart
{
    /// <summary>
    /// 符合条件的分支
    /// </summary>
    public class AccordanceBranch
    {
        /// <summary>
        /// 条件Id
        /// </summary>
        public long ConditionId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long NextStepId { get; set; }

        /// <summary>
        /// 同时符合多条件时，是否执行此条件
        /// </summary>
        public int ExecuteMatched { get; set; }
    }
}
