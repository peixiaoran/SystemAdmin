namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto
{
    /// <summary>
    /// 币别下拉框Dto
    /// </summary>
    public class CurrencyInfoDropDto
    {
        /// <summary>
        /// 币别编码
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 币别名称
        /// </summary>
        public string CurrencyName { get; set; } = string.Empty;
    }
}
