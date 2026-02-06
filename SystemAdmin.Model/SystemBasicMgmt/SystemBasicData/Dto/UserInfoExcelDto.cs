namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 导出员工信息Excel Dto
    /// </summary>
    public class UserInfoExcelDto
    {
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
        /// 所属部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 入职时间
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderName { get; set; } = string.Empty;

        /// <summary>
        /// 国籍
        /// </summary>
        public string NationalityName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 是否在职
        /// </summary>
        public string IsEmployedName { get; set; } = string.Empty;

        /// <summary>
        /// 是否需要签核
        /// </summary>
        public string IsApprovalName { get; set; } = string.Empty;

        /// <summary>
        /// 是否冻结
        /// </summary>
        public string IsFreezeName { get; set; } = string.Empty;
    }
}
