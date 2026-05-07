using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Queries
{
    /// <summary>
    /// 查询待签核信息请求参数
    /// </summary>
    public class GetPendingSubAppPage : PageModel
    {
        /// <summary>
        /// 表单组别Id
        /// </summary>
        public string FormGroupId { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 表单单号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;
    }
}
