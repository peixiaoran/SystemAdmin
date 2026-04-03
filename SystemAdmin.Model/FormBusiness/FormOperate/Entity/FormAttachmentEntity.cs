using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Entity
{
    /// <summary>
    /// 请假表文件表
    /// </summary>
    [SugarTable("[Form].[FormFile]")]
    public class FormAttachmentEntity
    {
        /// <summary>
        /// 附件Id
        /// </summary>
        public long AttachmentId { get; set; }

        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 附件文件名
        /// </summary>
        public string AttachmentName { get; set; } = string.Empty;

        /// <summary>
        /// 附件文件相对路径
        /// </summary>
        public string AttachmentPath { get; set; } = string.Empty;

        /// <summary>
        /// 附件文件大小（kb）
        /// </summary>
        public int AttachmentSize { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
