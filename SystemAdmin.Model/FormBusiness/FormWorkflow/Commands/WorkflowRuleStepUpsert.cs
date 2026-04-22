namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 流程规则步骤新增/修改类
    /// </summary>
    public class WorkflowRuleStepUpsert
    {
        /// <summary>
        /// 规则Id
        /// </summary>
        public long RuleId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public long CurrentStepId { get; set; }

        /// <summary>
        /// 下一步骤Id
        /// </summary>
        public long NextStepId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
