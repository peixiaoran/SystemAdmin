namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries
{
    /// <summary>
    /// 查询币别实体请求参数
    /// </summary>
    public class GetCurrencyInfoEntity
    {
        /// <summary>
        /// 币别Id
        /// </summary>
        public string CurrencyId { get; set; } = string.Empty;
    }
}
