namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto
{
    /// <summary>
    /// 汇率对照Dto
    /// </summary>
    public class ExchangeRateDto
    {
        /// <summary>
        /// 本币别Id
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 兑换币别Id
        /// </summary>
        public string ExchangeCurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 汇率
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        public string YearMonth { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
