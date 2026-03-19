using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto
{
    /// <summary>
    /// 请假表文件表Dto
    /// </summary>
    public class LeaveFileDto
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;
    }
}
