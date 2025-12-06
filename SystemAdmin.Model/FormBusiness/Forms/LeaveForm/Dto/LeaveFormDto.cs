using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto
{
    /// <summary>
    /// 请假单基础信息Dto
    /// </summary>
    public class LeaveFormDto
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单简短描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 重要程度
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int ImportanceCode { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int FormStatus { get; set; }

        /// <summary>
        /// 表单状态名称
        /// </summary>
        public string FormStatusName { get; set; } = string.Empty;

        /// <summary>
        /// 请假单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
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
        [JsonConverter(typeof(LongToStringConverter))]
        public long ApplicantDeptId { get; set; }

        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplicantDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 假别编码
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int LeaveTypeCode { get; set; }

        /// <summary>
        /// 请假事由
        /// </summary>
        public string LeaveReason { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public string? LeaveStartTime { get; set; } = string.Empty;

        /// <summary>
        /// 结束时间
        /// </summary>
        public string? LeaveEndTime { get; set; } = string.Empty;

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
