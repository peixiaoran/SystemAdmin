using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 分支步骤表
    /// </summary>
    [SugarTable("[Form].[WorkflowBranchStep]")]
    public class WorkflowBranchStepEntity
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        public long BranchId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

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
