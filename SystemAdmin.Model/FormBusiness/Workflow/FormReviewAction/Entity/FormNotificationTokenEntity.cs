using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewAction.Entity
{
    /// <summary>
    /// 表单邮件通知链接Token实体
    /// </summary>
    [SugarTable("[Form].[FormNotificationToken]")]
    public class FormNotificationTokenEntity
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 待审批人
        /// </summary>
        public long ReviewUserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
