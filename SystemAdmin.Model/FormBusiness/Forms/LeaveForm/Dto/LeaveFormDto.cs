using System.Text.Json.Serialization;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto
{
    /// <summary>
    /// 请假单基础信息Dto
    /// </summary>
    public class LeaveFormDto
    {
        /// <summary>
        /// 请假单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 表单类别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 请假单号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;
          
        /// <summary>
        /// 表单状态
        /// </summary>
        public string FormStatus { get; set; } = string.Empty;

        /// <summary>
        /// 表单状态名称
        /// </summary>
        public string FormStatusName { get; set; } = string.Empty;

        /// <summary>
        /// 申请人工号
        /// </summary>
        public string ApplicantUserNo { get; set; } = string.Empty;

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantUserName { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplicantDeptName { get; set; } = string.Empty;

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
        public DateTime? LeaveStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? LeaveEndTime { get; set; }

        /// <summary>
        /// 请假小时（系统计算）
        /// </summary>
        public decimal LeaveHours { get; set; }

        /// <summary>
        /// 代理人工号
        /// </summary>
        public string AgentUserNo { get; set; } = string.Empty;

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<FormAttachmentDto> AttachmentList { get; set; } = new List<FormAttachmentDto>();

        /// <summary>
        /// 审批记录表
        /// </summary>
        public List<FormReviewRecordDto> ReviewRecordList { get; set; } = new List<FormReviewRecordDto>();
    }
}
