using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 员工Dto
    /// </summary>
    public class UserInfoEntityDto
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
        /// 是否代理其他员工
        /// </summary>
        public int AgentOthers { get; set; }

        /// <summary>
        /// 是否被代理
        /// </summary>
        public int Represented { get; set; }

        /// <summary>
        /// 所属部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 所属部门级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 所属部门级别名称
        /// </summary>
        public string DepartmentLevelName { get; set; } = string.Empty;

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
        /// 角色Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int Gender { get; set; }

        /// <summary>
        /// 性别描述
        /// </summary>
        public string GenderName { get; set; } = string.Empty;

        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 国籍
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Nationality { get; set; }

        /// <summary>
        /// 国籍文字描述
        /// </summary>
        public string NationalityName { get; set; } = string.Empty;

        /// <summary>
        /// 职业Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LaborId { get; set; }

        /// <summary>
        /// 职业文字描述
        /// </summary>
        public string LaborName { get; set; } = string.Empty;

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
        /// 员工头像地址
        /// </summary>
        public string AvatarAddress { get; set; } = string.Empty;

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
        /// 过期天数
        /// </summary>
        public int ExpirationDays { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpirationTime { get; set; } = string.Empty;
    }
}
