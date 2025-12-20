using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries
{
    /// <summary>
    /// 查询币别分页请求参数
    /// </summary>
    public class GetCurrencyInfoPage : PageModel
    {
        /// <summary>
        /// 币别编码
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;
    }
}
