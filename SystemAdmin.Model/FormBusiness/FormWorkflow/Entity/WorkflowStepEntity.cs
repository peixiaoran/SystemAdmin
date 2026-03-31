using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 步骤实体类
    /// </summary>
    [SugarTable("[Form].[WorkflowStep]")]
    public class WorkflowStepEntity
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long StepId { get; set; }

        /// <summary>
        /// 表单组别Id
        /// </summary>
        public long FormGroupId { get; set; }

        /// <summary>
        /// 表单类型Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 步骤名称（中文）
        /// </summary>
        public string StepNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 步骤名称（英文）
        /// </summary>
        public string StepNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否为开始步骤
        /// </summary>
        public int IsStartStep { get; set; }

        /// <summary>
        /// 步骤指派规则（依组织架构、指定部门员工级别、指定员工、自定义）
        /// </summary>
        public string Assignment { get; set; } = string.Empty;

        /// <summary>
        /// 签核方式（单签、会签、或签）
        /// </summary>
        public string ApproveMode { get; set; } = string.Empty;

        /// <summary>
        /// 是否催签
        /// </summary>
        public int IsReminderEnabled { get; set; }

        /// <summary>
        /// 催签间隔分钟
        /// </summary>
        public int ReminderIntervalMinutes { get; set; }

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
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
