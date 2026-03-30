using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormAudit.Dto
{
    /// <summary>
    /// 表单年月单号计数实体类
    /// </summary>
    public class FormSequenceDto
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单类型名称
        /// </summary>
        public string FormTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 年月（yyyyMM）
        /// </summary>
        public string Ym { get; set; } = string.Empty;

        /// <summary>
        /// 当前表单数量
        /// </summary>
        public int Total { get; set; }
    }
}
