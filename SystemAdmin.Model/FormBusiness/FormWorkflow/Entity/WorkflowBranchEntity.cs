using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 分支实体类
    /// </summary>
    [SugarTable("[Form].[WorkflowBranch]")]
    public class WorkflowBranchEntity
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long BranchId { get; set; }

        /// <summary>
        /// 所属表单Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 分支名称（中文）
        /// </summary>
        public string BranchNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 分支名称（英文）
        /// </summary>
        public string BranchNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 代码标记
        /// </summary>
        public string HandlerKey { get; set; } = string.Empty;

        /// <summary>
        /// 逻辑说明
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
