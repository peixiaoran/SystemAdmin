using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto
{
    public class ImportanceDropDto
    {
        /// <summary>
        /// 重要程度编码
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int ImportanceCode { get; set; }

        /// <summary>
        /// 重要程度名称
        /// </summary>
        public string ImportanceName { get; set; } = string.Empty;
    }
}
