namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries
{
    /// <summary>
    /// 查询汇率实体请求参数
    /// </summary>
    public class GetExchangeRateEntity
    {
        /// <summary>
        /// 本币别编码
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 目标币别编码
        /// </summary>
        public string ExchangeCurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 年月
        /// </summary>
        public string YearMonth { get; set; } = string.Empty;
    }
}
