using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 步骤指定部门员工级别来源表
    /// </summary>
    [SugarTable("[Form].[WorkflowStepDeptUser]")]
    public class WorkflowStepDeptUserEntity
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long StepId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
