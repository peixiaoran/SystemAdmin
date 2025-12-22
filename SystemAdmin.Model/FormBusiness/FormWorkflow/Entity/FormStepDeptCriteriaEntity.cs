using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 签核步骤指定部门人员级别来源表
    /// </summary>
    [SugarTable("[Form].[FormStepDeptCriteria]")]
    public class FormStepDeptCriteriaEntity
    {
        /// <summary>
        /// 签核步骤指定部门人员级别Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long StepDeptUserId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

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
