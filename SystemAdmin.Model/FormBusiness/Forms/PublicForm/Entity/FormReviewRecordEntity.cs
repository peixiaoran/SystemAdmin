using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity
{
    /// <summary>
    /// 表单审批记录实体类
    /// </summary>
    [SugarTable("[Form].[FormReviewRecord]")]
    public class FormReviewRecordEntity
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
        /// 操作状态
        /// </summary>
        public string ReviewResult { get; set; } = string.Empty;

        /// <summary>
        /// 驳回至步骤
        /// </summary>
        public long RejectedStepId { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// 审批类型
        /// </summary>
        public string ReviewType { get; set; } = string.Empty;

        /// <summary>
        /// 审批身份
        /// </summary>
        public string ReviewAppointment { get; set; } = string.Empty;

        /// <summary>
        /// 审批员工Id
        /// </summary>
        public long ReviewUserId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ReviewDateTime { get; set; }
    }
}
