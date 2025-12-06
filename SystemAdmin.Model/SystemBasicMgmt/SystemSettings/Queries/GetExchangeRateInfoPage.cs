using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries
{
    /// <summary>
    /// 查询汇率对照分页请求参数
    /// </summary>
    public class GetExchangeRatePage : PageModel
    {
        /// <summary>
        /// 本币别编码
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 年月
        /// </summary>
        public string YearMonth { get; set; } = string.Empty;
    }
}
