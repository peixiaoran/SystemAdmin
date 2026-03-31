using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 步骤组织架构表
    /// </summary>
    [SugarTable("[Form].[WorkflowStepOrg]")]
    public class WorkflowStepOrgEntity
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long StepId { get; set; }

        /// <summary>
        /// 部门级别Id
        /// </summary>
        public long DeptLeaveId { get; set; }

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
        public DateTime CreatedDate { get; set; }
    }
}
