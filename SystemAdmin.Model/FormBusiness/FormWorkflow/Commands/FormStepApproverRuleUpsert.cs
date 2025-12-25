namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 审批步骤自定义来源新增/修改类
    /// </summary>
    public class FormStepApproverRuleUpsert
    {
        /// <summary>
        /// 审批步骤自定义Id
        /// </summary>
        public long StepApproverRule { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

        /// <summary>
        /// 代码标记
        /// </summary>
        public long Mark { get; set; }

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
