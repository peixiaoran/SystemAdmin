using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Entity
{
    /// <summary>
    /// 表单待签核人表
    /// </summary>
    [SugarTable("[Form].[PendingReview]")]
    public class PendingReviewEntity
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

        /// <summary>
        /// 待审批人身份
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// 待审批人Id
        /// </summary>
        public long ReviewUserId { get; set; }
    }
}
