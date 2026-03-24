namespace SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart
{
    /// <summary>
    /// 流程驳回记录
    /// </summary>
    public class RejectLogList
    {
        /// <summary>
        /// 驳回步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 驳回用户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 驳回时间
        /// </summary>
        public DateTime RejectDate { get; set; }
    }
}
