using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto
{
    /// <summary>
    /// 表单通知Token Dto
    /// </summary>
    public class FormNotificationUserDto
    {
        /// <summary>
        /// 员工信息
        /// </summary>
        public UserInfoEntity user { get; set; } = new UserInfoEntity();

        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }
    }
}
