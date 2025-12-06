namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Commands
{
    /// <summary>
    /// 员工绑定表单新增/修改类
    /// </summary>
    public class UserFormBindUpsert
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别类型Id
        /// </summary>
        public List<string> FormGroupTypeId { get; set; } = new List<string>();
    }
}
