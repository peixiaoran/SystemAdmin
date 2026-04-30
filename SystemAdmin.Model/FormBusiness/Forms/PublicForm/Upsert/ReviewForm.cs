namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Upsert
{
    /// <summary>
    /// 审批表单提交类
    /// </summary>
    public class ReviewForm
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public string FormId { get; set; } = string.Empty;

        /// <summary>
        /// 审批意见
        /// </summary>
        public string RejectStepId { get; set; } = "0";

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Comment { get; set; } = string.Empty;
    }
}
