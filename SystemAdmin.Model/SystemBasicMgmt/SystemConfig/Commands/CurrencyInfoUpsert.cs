namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Commands
{
    /// <summary>
    /// 币别新增/修改类
    /// </summary>
    public class CurrencyInfoUpsert
    {
        /// <summary>
        /// 币别主键Id
        /// </summary>
        public string CurrencyId { get; set; } = string.Empty;

        /// <summary>
        /// 币别编码
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// 币别名称（中文）
        /// </summary>
        public string CurrencyNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 币别名称（英文）
        /// </summary>
        public string CurrencyNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 币别排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
