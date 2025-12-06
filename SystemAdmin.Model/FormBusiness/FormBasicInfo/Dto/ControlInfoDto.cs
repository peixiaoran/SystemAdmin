namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto
{
    /// <summary>
    /// 表单表单控件Dto
    /// </summary>
    public class ControlInfoDto
    {
        /// <summary>
        /// 表单控件编码
        /// </summary>
        public string ControlCode { get; set; } = string.Empty;

        /// <summary>
        /// 表单控件名称
        /// </summary>
        public string ControlName { get; set; } = string.Empty;

        /// <summary>
        /// 表单控件描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
