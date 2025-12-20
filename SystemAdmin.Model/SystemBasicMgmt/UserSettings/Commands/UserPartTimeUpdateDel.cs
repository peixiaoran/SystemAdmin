namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Commands
{
    /// <summary>
    /// 员工兼任修改/删除类
    /// </summary>
    public class UserPartTimeUpdateDel
    {
        /// <summary>
        /// 员工Id（老）
        /// </summary>
        public string Old_UserId { get; set; } = string.Empty;

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
