using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// <summary>
    /// 部门信息实体类
    /// </summary>
    [SugarTable("[Basic].[DepartmentInfo]")]
    public class DepartmentInfoEntity
    {
        /// <summary>
        /// 部门主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 部门编码（唯一标识）
        /// </summary>
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 上级部门Id（可为空）
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 部门级别Id
        /// </summary>
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 排序值（默认0）
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 部门固定电话
        /// </summary>
        public string Landline { get; set; } = string.Empty;

        /// <summary>
        /// 部门邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 部门地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用（1：启用，0：停用）
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人Id
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [SugarColumn(IsIgnore = true, IsTreeKey = true)]
        public List<DepartmentInfoEntity> DepartmentChildList { get; set; } = new List<DepartmentInfoEntity>();
    }
}
