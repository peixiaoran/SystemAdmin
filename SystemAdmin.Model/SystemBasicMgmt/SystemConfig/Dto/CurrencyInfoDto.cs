using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto
{
    /// <summary>
    /// 币别Dto
    /// </summary>
    public class CurrencyInfoDto
    {
        /// <summary>
        /// 币别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
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
    }
}
