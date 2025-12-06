using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// <summary>
    /// 部门级别实体类
    /// </summary>
    [SugarTable("[Basic].[DepartmentLevel]")]
    public class DepartmentLevelEntity
    {
        /// <summary>
        /// 部门级别Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 部门级别编号
        /// </summary>
        public string DepartmentLevelCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门级别名称（中文）
        /// </summary>
        public string DepartmentLevelNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 部门级别名称（英文）
        /// </summary>
        public string DepartmentLevelNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 排序值（默认0）
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
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
