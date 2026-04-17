using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dtp
{
    /// <summary>
    /// 表单附件表Dto
    /// </summary>
    public class FormAttachmentDto
    {
        /// <summary>
        /// 附件Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long AttachmentId { get; set; }

        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 附件文件名
        /// </summary>
        public string AttachmentName { get; set; } = string.Empty;

        /// <summary>
        /// 附件相对路径
        /// </summary>
        public string AttachmentPath { get; set; } = string.Empty;

        /// <summary>
        /// 附件大小（kb）
        /// </summary>
        public int AttachmentSize { get; set; }
    }
}
