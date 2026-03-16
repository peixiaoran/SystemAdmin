namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Commands
{
    /// <summary>
    /// 请假单保存类
    /// </summary>
    public class LeaveFormSave
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 表单简短描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 请假单Id
        /// </summary>
        public string FormId { get; set; } = string.Empty;

        /// <summary>
        /// 请假单号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;

        /// <summary>
        /// 申请人工号
        /// </summary>
        public long ApplicantUserId { get; set; }

        /// <summary>
        /// 假别编码
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
        /// 请假小时
        /// </summary>
        public decimal LeaveHours { get; set; }

        /// <summary>
        /// 代理人工号
        /// </summary>
        public string AgentUserNo { get; set; } = string.Empty;
    }
}
