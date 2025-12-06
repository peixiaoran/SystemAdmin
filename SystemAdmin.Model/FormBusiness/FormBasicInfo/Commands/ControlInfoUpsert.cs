namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Commands
{
    /// <summary>
    /// 表单控件新增/修改类
    /// </summary>
    public class ControlInfoUpsert
    {
        /// <summary>
        /// 控件编码
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
