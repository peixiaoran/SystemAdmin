using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries
{
    /// <summary>
    /// 查询表单组别分页请求参数
    /// </summary>
    public class GetFormGroupPage : PageModel
    {
        /// <summary>
        /// 表单组别名称
        /// </summary>
        public string FormGroupName { get; set; } = string.Empty;
    }
}
