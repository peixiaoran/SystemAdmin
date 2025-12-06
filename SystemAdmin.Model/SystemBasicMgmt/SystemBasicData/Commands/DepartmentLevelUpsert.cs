namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands
{
    /// <summary>
    /// 部门级别新增/修改类
    /// </summary>
    public class DepartmentLevelUpsert
    {
        /// <summary>
        /// 部门级别Id
        /// </summary>
        public string DepartmentLevelId { get; set; } = string.Empty;

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
        /// 部门等级排序值
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
