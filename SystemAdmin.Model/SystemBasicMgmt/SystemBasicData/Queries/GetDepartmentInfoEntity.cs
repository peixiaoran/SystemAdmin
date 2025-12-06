namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries
{
    /// <summary>
    /// 查询部门实体请求参数
    /// </summary>
    public class GetDepartmentInfoEntity
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;
    }
}
