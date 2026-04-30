namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewAction
{
    /// <summary>
    /// 实职和审批人身份
    /// </summary>
    public class ActualAppointment
    {
        /// <summary>
        /// 原本审批人Id
        /// </summary>
        public long OriginalUserId { get; set; }

        /// <summary>
        /// 实际审批人Id
        /// </summary>
        public long ReviewUserId { get; set; }

        /// <summary>
        /// 实际审批人身份
        /// </summary>
        public string AppointmentCode { get; set; } = string.Empty;
    }
}
