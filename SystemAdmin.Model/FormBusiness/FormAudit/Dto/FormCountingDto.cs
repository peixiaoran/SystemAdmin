using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormAudit.Dto
{
    /// <summary>
    /// 表单年月单号计数实体类
    /// </summary>
    public class FormCountingDto
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
        public string YM { get; set; } = string.Empty;

        /// <summary>
        /// 当前表单数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 草稿数量
        /// </summary>
        public int Draft { get; set; }

        /// <summary>
        /// 提交数量
        /// </summary>
        public int Submitted { get; set; }

        /// <summary>
        /// 送审数量
        /// </summary>
        public int Approved { get; set; }

        /// <summary>
        /// 驳回数量
        /// </summary>
        public int Rejected { get; set; }

        /// <summary>
        /// 作废数量
        /// </summary>
        public int Canceled { get; set; }
    }
}
