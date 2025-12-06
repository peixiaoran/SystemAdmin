namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Commands
{
    /// <summary>
    /// 员工兼任新增/修改类
    /// </summary>
    public class UserPartTimeInsert
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 兼任部门Id
        /// </summary>
        public string PartTimeDeptId { get; set; } = string.Empty;

        /// <summary>
        /// 兼任职级Id
        /// </summary>
        public string PartTimePositionId { get; set; } = string.Empty;

        /// <summary>
        /// 兼任职业Id
        /// </summary>
        public string PartTimeLaborId { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;
    }
}
