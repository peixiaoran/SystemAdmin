namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands
{
    /// <summary>
    /// 员工新增/修改类
    /// </summary>
    public class UserInfoUpsert
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        public string PositionId { get; set; } = string.Empty;

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
        /// 员工角色Id
        /// </summary>
        public string RoleId { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 国籍
        /// </summary>
        public long Nationality { get; set; }

        /// <summary>
        /// 职业Id
        /// </summary>
        public string LaborId { get; set; } = string.Empty;

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
        /// 登录密码
        /// </summary>
        public string PassWord { get; set; } = string.Empty;

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PwdSalt { get; set; } = string.Empty;

        /// <summary>
        /// 头像图片地址
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
        /// 是否冻结
        /// </summary>
        public int IsFreeze { get; set; }

        /// <summary>
        /// 密码过期天数
        /// </summary>
        public int ExpirationDays { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
