using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto
{
    /// <summary>
    /// 请假表文件表Dto
    /// </summary>
    public class FormFileDto
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

        /// <summary>
        /// 文件大小（kb）
        /// </summary>
        public int FileSize { get; set; }
    }
}
