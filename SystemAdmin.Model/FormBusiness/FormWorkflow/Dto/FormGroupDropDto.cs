using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 表单组别下拉框Dto
    /// </summary>
    public class FormGroupDropDto
    {
        /// <summary>
        /// 表单组别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormGroupId { get; set; }

        /// <summary>
        /// 表单组别名称
        /// </summary>
        public string FormGroupName { get; set; } = string.Empty;
    }
}
