using SqlSugar;
using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// <summary>
    /// 员工视图类
    /// </summary>
    [SugarTable("[Basic].[V_UserInfo]")]
    public class UserInfoView
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }


        /// <summary>
        /// 所属部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 所属部门文字描述（中文）
        /// </summary>
        public string DepartmentNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 所属部门文字描述（英文）
        /// </summary>
        public string DepartmentNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 所属部门级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 所属部门级别文字描述（中文）
        /// </summary>
        public string DepartmentLevelNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 所属部门级别文字描述（英文）
        /// </summary>
        public string DepartmentLevelNameEn { get; set; } = string.Empty;

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
        /// 性别
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int Gender { get; set; }

        /// <summary>
        /// 性别文字描述（中文）
        /// </summary>
        public string GenderNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 性别文字描述（英文）
        /// </summary>
        public string GenderNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginNo { get; set; } = string.Empty;

        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 国籍
        /// </summary>
        public int Nationality { get; set; }

        /// <summary>
        /// 国籍文字描述（中文）
        /// </summary>
        public string NationalityNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 国籍文字描述（英文）
        /// </summary>
        public string NationalityNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 员工头像地址
        /// </summary>
        public string AvatarAddress { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PositionId { get; set; }

        /// <summary>
        /// 职级文字描述（中文）
        /// </summary>
        public string PositionNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职级文字描述（英文）
        /// </summary>
        public string PositionNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职级排序
        /// </summary>
        public int PositionOrderBy { get; set; }

        /// <summary>
        /// 职业Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LaborId { get; set; }

        /// <summary>
        /// 职业文字描述（中文）
        /// </summary>
        public string LaborNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职业文字描述（英文）
        /// </summary>
        public string LaborNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoleId { get; set; }

        /// <summary>
        /// 角色文字描述（中文）
        /// </summary>
        public string RoleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 角色文字描述（英文）
        /// </summary>
        public string RoleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否在职
        /// </summary>
        public int IsEmployed { get; set; }

        /// <summary>
        /// 是否在职文字描述（中文）
        /// </summary>
        public string IsEmployedNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否在职文字描述（英文）
        /// </summary>
        public string IsEmployedNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否签核
        /// </summary>
        public int IsApproval { get; set; }

        /// <summary>
        /// 是否签核文字描述（中文）
        /// </summary>
        public string IsApprovalNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否签核文字描述（英文）
        /// </summary>
        public string IsApprovalNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否实时通知邮件
        /// </summary>
        public int IsRealtimeNotification { get; set; }

        /// <summary>
        /// 是否实时通知邮件（中文）
        /// </summary>
        public string IsRealtimeNotificationCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否实时通知邮件（英文）
        /// </summary>
        public string IsRealtimeNotificationEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否实时通知邮件
        /// </summary>
        public int IsScheduledNotification { get; set; }

        /// <summary>
        /// 是否定时通知邮件（中文）
        /// </summary>
        public string IsScheduledNotificationCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否定时通知邮件（英文）
        /// </summary>
        public string IsScheduledNotificationEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否代理其他员工
        /// </summary>
        public int IsAgent { get; set; }

        /// <summary>
        /// 是否代理其他员工（中文）
        /// </summary>
        public string IsAgentNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否代理其他员工（英文）
        /// </summary>
        public string IsAgentNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否兼任
        /// </summary>
        public int IsPartTime { get; set; }

        /// <summary>
        /// 是否兼任文字描述（中文）
        /// </summary>
        public string IsParttimeNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否兼任文字描述（英文）
        /// </summary>
        public string IsParttimeNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 是否冻结
        /// </summary>
        public int IsFreeze { get; set; }

        /// <summary>
        /// 是否冻结文字描述（中文）
        /// </summary>
        public string IsFreezeNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 是否冻结文字描述（英文）
        /// </summary>
        public string IsFreezeNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
