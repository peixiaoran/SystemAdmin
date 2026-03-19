using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Dto
{
    /// <summary>
    /// 表单类别下拉Dto
    /// </summary>
    public class FormTypeDropDto
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单类别名称
        /// </summary>
        public string FormTypeName { get; set; } = string.Empty;
    }
}
