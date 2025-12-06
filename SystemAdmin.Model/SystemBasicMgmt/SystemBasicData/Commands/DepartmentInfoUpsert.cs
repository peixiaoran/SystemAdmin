namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands
{
    /// <summary>
    /// 部门新增/修改类
    /// </summary>
    public class DepartmentInfoUpsert
    {
        /// <summary>
        /// 部门主键Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称（中文）
        /// </summary>
        public string DepartmentNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称（英文）
        /// </summary>
        public string DepartmentNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 上级部门Id（可为空）
        /// </summary>
        public string ParentId { get; set; } = string.Empty;

        /// <summary>
        /// 部门级别Id
        /// </summary>
        public string DepartmentLevelId { get; set; } = string.Empty;

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
        /// 部门描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
