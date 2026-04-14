namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands
{
    /// <summary>
    /// 请假单保存类
    /// </summary>
    public class LeaveFormSave
    {
        /// <summary>
        /// 请假单Id
        /// </summary>
        public string FormId { get; set; } = string.Empty;

        /// <summary>
        /// 假别
        /// </summary>
        public string LeaveTypeCode { get; set; } = string.Empty;

        /// <summary>
        /// 请假事由
        /// </summary>
        public string LeaveReason { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime LeaveStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime LeaveEndTime { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal LeaveDays { get; set; }

        /// <summary>
        /// 代理人工号
        /// </summary>
        public string AgentUserNo { get; set; } = string.Empty;
    }
}
