using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// 员工实体类
    /// </summary>
    [SugarTable("[Basic].[UserInfo]")]
    public class UserInfoEntity
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long UserId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 员工编号
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
        /// 职业编码
        /// </summary>
        public long LaborId { get; set; }

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
        /// 密码盐值
        /// </summary>
        public string PwdSalt { get; set; } = string.Empty;

        /// <summary>
        /// 员工头像
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

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; } = null;
    }
}
