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
        /// 重要程度
        /// </summary>
        public int ImportanceCode { get; set; }

        /// <summary>
        /// 请假单Id
        /// </summary>
        public string FormId { get; set; } = string.Empty;

        /// <summary>
        /// 请假单号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplicantTime { get; set; } = string.Empty;

        /// <summary>
        /// 申请人工号
        /// </summary>
        public string ApplicantUserNo { get; set; } = string.Empty;

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantUserName { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门Id
        /// </summary>
        public long ApplicantDeptId { get; set; }

        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplicantDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 假别编码
        /// </summary>
        public int LeaveTypeCode { get; set; }

        /// <summary>
        /// 请假事由
        /// </summary>
        public string LeaveReason { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string LeaveStartTime { get; set; } = string.Empty;

        /// <summary>
        /// 结束时间
        /// </summary>
        public string LeaveEndTime { get; set; } = string.Empty;

        /// <summary>
        /// 请假小时（系统计算）
        /// </summary>
        public decimal LeaveHours { get; set; }

        /// <summary>
        /// 交接人Id
        /// </summary>
        public string LeaveHandoverUserName { get; set; } = string.Empty;
    }
}
