using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity
{
    /// <summary>
    /// 币别实体类
    /// </summary>
    [SugarTable("[Basic].[CurrencyInfo]")]
    public class CurrencyInfoEntity
    {
        /// <summary>
        /// 币别Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long CurrencyId { get; set; }

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
