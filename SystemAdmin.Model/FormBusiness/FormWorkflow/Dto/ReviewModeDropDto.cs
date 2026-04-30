namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 审批方式下拉框Dto
    /// </summary>
    public class ReviewModeDropDto
    {
        /// <summary>
        /// 审批方式编码
        /// </summary>
        public string ApproveModeCode { get; set; } = string.Empty;

        /// <summary>
        /// 审批方式名称
        /// </summary>
        public string ApproveModeName { get; set; } = string.Empty;
    }
}
