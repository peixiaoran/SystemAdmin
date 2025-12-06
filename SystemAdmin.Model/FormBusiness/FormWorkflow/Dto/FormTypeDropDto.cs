using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 表单类型下拉框Dto
    /// </summary>
    public class FormTypeDropDto
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
    }
}
