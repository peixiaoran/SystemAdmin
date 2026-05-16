namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Upsert
{
    /// <summary>
    /// 审批表单提交类
    /// </summary>
    public class ApproveForm
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public string FormId { get; set; } = string.Empty;

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Comment { get; set; } = string.Empty;
    }
}
