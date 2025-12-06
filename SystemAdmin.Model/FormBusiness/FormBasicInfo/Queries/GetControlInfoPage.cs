using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries
{
    /// <summary>
    /// 查询表单表单控件分页请求参数
    /// </summary>
    public class GetControlInfoPage : PageModel
    {
        /// <summary>
        /// 控件编码
        /// </summary>
        public string ControlCode { get; set; } = string.Empty;
    }
}
