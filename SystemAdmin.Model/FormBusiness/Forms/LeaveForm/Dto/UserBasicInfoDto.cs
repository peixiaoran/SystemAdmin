namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto
{
    /// <summary>
    /// 表单员工基本信息
    /// </summary>
    public class UserBasicInfoDto
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DetpId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DetpName { get; set; } = string.Empty;

        /// <summary>
        /// 职级编码
        /// </summary>
        public string PositionNo { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
