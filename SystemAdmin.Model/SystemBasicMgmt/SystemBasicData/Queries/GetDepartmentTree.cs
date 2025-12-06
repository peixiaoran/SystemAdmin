namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries
{
    /// <summary>
    /// 查询部门树列表请求参数
    /// </summary>
    public class GetDepartmentTree
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;
    }
}
