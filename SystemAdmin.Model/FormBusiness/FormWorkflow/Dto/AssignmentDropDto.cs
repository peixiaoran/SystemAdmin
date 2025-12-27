namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 审批人选取方式下拉框Dto
    /// </summary>
    public class AssignmentDropDto
    {
        /// <summary>
        /// 审批人选取方式编码
        /// </summary>
        public string AssignmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 审批人选取方式名称
        /// </summary>
        public string AssignmentName { get; set; } = string.Empty;
    }
}
