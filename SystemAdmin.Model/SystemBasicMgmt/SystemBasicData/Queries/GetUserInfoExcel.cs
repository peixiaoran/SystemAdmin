namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries
{
    /// <summary>
    /// 导出用户信息Excel请求参数
    /// </summary>
    public class GetUserInfoExcel
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
