using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto
{
    /// <summary>
    /// 表单通知Token返回 Dto
    /// </summary>
    public class FormNotificationReturnDto
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名（中文）
        /// </summary>
        public string UserNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名（英文）
        /// </summary>
        public string UserNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 员工头像地址
        /// </summary>
        public string AvatarAddress { get; set; } = string.Empty;

        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }
    }
}
