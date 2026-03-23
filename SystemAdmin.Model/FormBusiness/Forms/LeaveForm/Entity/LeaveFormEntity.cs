using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity
{
    /// <summary>
    /// 请假单基础信息实体
    /// </summary>
    [SugarTable("[Form].[LeaveForm]")]
    public class LeaveFormEntity
    {
        /// <summary>
        /// 请假单Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long FormId { get; set; }

        /// <summary>
        /// 请假单号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long ApplicantUserId { get; set; }

        /// <summary>
        /// 假别类型Id
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

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
