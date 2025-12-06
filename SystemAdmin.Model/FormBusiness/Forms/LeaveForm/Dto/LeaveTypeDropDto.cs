using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto
{
    /// <summary>
    /// 假别下拉框Dto
    /// </summary>
    public class LeaveTypeDropDto
    {
        /// <summary>
        /// 假别编码
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int LeaveTypeCode { get; set; }

        /// <summary>
        /// 假别名称
        /// </summary>
        public string LeaveTypeName { get; set; } = string.Empty;
    }
}
