using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto
{
    /// <summary>
    /// 员工分页Dto
    /// </summary>
    public class UserPartTimeDto
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo {  get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName {  get; set; } = string.Empty;

        /// <summary>
        /// 是否签核
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int IsApproval { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }

        /// <summary>
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 兼任部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PartTimeDeptId { get; set; }

        /// <summary>
        /// 兼任部门名称
        /// </summary>
        public string PartTimeDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 兼任职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PartTimePositionId { get; set; }

        /// <summary>
        /// 兼任职级名称
        /// </summary>
        public string PartTimePositionName { get; set; } = string.Empty;

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
