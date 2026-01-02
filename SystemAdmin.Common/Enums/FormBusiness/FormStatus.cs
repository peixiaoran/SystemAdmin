namespace SystemAdmin.Common.Enums.FormBusiness
{
    /// <summary>
    /// 表单状态枚举
    /// </summary>
    public enum FormStatus
    {
        /// <summary>
        /// 待送审
        /// </summary>
        PendingSubmission = 1,

        /// <summary>
        /// 审批中
        /// </summary>
        UnderReview = 2,

        /// <summary>
        /// 已驳回
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// 已批准
        /// </summary>
        Approved = 4,

        /// <summary>
        /// 作废
        /// </summary>
        Cancelled = 5,
    }
}
