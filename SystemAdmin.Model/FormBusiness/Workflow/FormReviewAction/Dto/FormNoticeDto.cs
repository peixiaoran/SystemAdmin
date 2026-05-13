namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewAction.Dto
{
    /// <summary>
    /// 表单通知邮件审批Dto
    /// </summary>
    public class FormNoticeDto
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 表单No
        /// </summary>
        public string FormNo { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型名称（中文）
        /// </summary>
        public string FormTypeNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型名称（英文）
        /// </summary>
        public string FormTypeNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型审批路径
        /// </summary>
        public string ReviewPath { get; set; } = string.Empty;

        /// <summary>
        /// 当前步骤名称（中文）
        /// </summary>
        public string CurrentStepNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 当前步骤名称（英文）
        /// </summary>
        public string CurrentStepNameEn { get; set; } = string.Empty;
    }
}
