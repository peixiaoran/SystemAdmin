using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    public class FormStepDeptCriteriaDto
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
    }
}
