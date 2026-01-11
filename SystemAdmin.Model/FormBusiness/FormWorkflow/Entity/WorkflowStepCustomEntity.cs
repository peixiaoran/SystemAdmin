using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 流程审批步骤自定义来源表
    /// </summary>
    [SugarTable("[Form].[WorkflowStepCustom]")]
    public class WorkflowStepCustomEntity
    {
        /// <summary>
        /// 审批步骤自定义Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long StepCustomId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

        /// <summary>
        /// 代码标记
        /// </summary>
        public string HandlerKey { get; set; } = string.Empty;

        /// <summary>
        /// 逻辑说明
        /// </summary>
        public string LogicalExplanation { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
