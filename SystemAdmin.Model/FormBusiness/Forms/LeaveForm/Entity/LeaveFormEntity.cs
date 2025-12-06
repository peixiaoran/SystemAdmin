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
        /// 假别类型Id
        /// </summary>
        public int LeaveTypeCode { get; set; }

        /// <summary>
        /// 请假事由
        /// </summary>
        public string LeaveReason { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string? LeaveStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string? LeaveEndTime { get; set; }

        /// <summary>
        /// 请假小时（系统计算）
        /// </summary>
        public decimal LeaveHours { get; set; }

        /// <summary>
        /// 交接人Id
        /// </summary>
        public string LeaveHandoverUserName { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
