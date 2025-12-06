using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity
{
    /// <summary>
    /// 汇率对照实体类
    /// </summary>
    [SugarTable("[Basic].[ExchangeRate]")]
    public class ExchangeRateEntity
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
        /// 年月
        /// </summary>
        public string YearMonth { get; set; } = string.Empty;

        /// <summary>
        /// 汇率
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
