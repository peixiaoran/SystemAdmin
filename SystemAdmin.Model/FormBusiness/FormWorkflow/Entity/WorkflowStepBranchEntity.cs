using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 步骤流程分支表
    /// </summary>
    [SugarTable("[Form].[WorkflowStepBranch]")]
    public class WorkflowStepBranchEntity
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        public long BranChId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

        /// <summary>
        /// 条件Id
        /// </summary>
        public long ConditionId { get; set; }

        /// <summary>
        /// 同时符合多条件时，是否执行此条件
        /// </summary>
        public int ExecuteMatched { get; set; }

        /// <summary>
        /// 下一步骤Id
        /// </summary>
        public long NextStepId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long ModifiedBy { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
