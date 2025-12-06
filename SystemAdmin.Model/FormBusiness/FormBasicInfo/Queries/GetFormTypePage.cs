using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries
{
    /// <summary>
    /// 查询表单类别分页请求参数
    /// </summary>
    public class GetFormTypePage : PageModel
    {
        /// <summary>
        /// 表单组别Id
        /// </summary>
        public string FormGroupId { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别名称
        /// </summary>
        public string FormTypeName { get; set; } = string.Empty;
    }
}
