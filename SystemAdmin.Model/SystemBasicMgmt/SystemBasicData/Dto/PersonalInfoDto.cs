using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 个人信息Dto
    /// </summary>
    public class PersonalInfoDto
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名（中文）
        /// </summary>
        public string UserNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名（英文）
        /// </summary>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 所属部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 所属部门级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }

        /// <summary>
        /// 职业编码
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LaborId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoleId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 员工头像地址
        /// </summary>
        public string AvatarAddress { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginNo { get; set; } = string.Empty;

        /// <summary>
        /// 是否在职
        /// </summary>
        public int IsEmployed { get; set; }

        /// <summary>
        /// 是否签核
        /// </summary>
        public int IsApproval { get; set; }

        /// <summary>
        /// 是否实时邮件通知
        /// </summary>
        public int IsRealtimeNotification { get; set; }

        /// <summary>
        /// 是否定时邮件通知
        /// </summary>
        public int IsScheduledNotification { get; set; }

        /// <summary>
        /// 是否代理其他员工
        /// </summary>
        public int IsAgent { get; set; }

        /// <summary>
        /// 是否兼任
        /// </summary>
        public int IsPartTime { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public int IsFreeze { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
