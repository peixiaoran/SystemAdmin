namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 审批步骤指定部门员工级别新增/修改类
    /// </summary>
    public class FormStepDeptCriteriaUpsert
    {
        /// <summary>
        /// 审批步骤指定部门员工级别Id
        /// </summary>
        public string StepDeptUserId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

        /// <summary>
        /// 部门Ids
        /// </summary>
        public string DeptIds { get; set; } = string.Empty;

        /// <summary>
        /// 职级Ids
        /// </summary>
        public string PositionIds { get; set; } = string.Empty;

        /// <summary>
        /// 职业Ids
        /// </summary>
        public string LaborIds { get; set; } = string.Empty;

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
