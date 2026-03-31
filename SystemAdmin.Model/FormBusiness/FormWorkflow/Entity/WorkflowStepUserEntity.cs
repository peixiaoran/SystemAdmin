using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 步骤指定员工表
    /// </summary>
    [SugarTable("[Form].[WorkflowStepUser]")]
    public class WorkflowStepUserEntity
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
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
