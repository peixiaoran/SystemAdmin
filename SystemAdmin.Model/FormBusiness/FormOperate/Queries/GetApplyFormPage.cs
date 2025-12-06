using SqlSugar;
namespace SystemAdmin.Model.FormBusiness.FormOperate.Queries
{
    /// <summary>
    /// 查询申请表单信息请求参数
    /// </summary>
    public class getApplyFormPage : PageModel
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
