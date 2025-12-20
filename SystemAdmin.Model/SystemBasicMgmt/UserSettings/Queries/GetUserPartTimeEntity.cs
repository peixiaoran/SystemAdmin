namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries
{
    /// <summary>
    /// 查询员工兼任实体请求参数
    /// </summary>
    public class GetUserPartTimeEntity
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 兼任部门Id（老）
        /// </summary>
        public string Old_PartTimeDeptId { get; set; } = string.Empty;

        /// <summary>
        /// 兼任职级Id（老）
        /// </summary>
        public string Old_PartTimePositionId { get; set; } = string.Empty;

        /// <summary>
        /// 兼任职业Id（老）
        /// </summary>
        public string Old_PartTimeLaborId { get; set; } = string.Empty;
    }
}
