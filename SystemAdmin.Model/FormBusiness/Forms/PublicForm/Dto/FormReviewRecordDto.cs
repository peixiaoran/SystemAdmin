using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto
{
    /// <summary>
    /// 表单审批记录Dto
    /// </summary>
    public class FormReviewRecordDto
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 操作状态
        /// </summary>
        public string ReviewResult { get; set; } = string.Empty;

        /// <summary>
        /// 操作状态名称
        /// </summary>
        public string ReviewResultName { get; set; } = string.Empty;

        /// <summary>
        /// 驳回至步骤名称
        /// </summary>
        public string RejectStepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// 审批类型
        /// </summary>
        public string ReviewType { get; set; } = string.Empty;

        /// <summary>
        /// 审批类型名称
        /// </summary>
        public string ReviewTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 审批身份
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// 审批身份名称
        /// </summary>
        public string AppointmentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 原本审批员工姓名
        /// </summary>
        public string OriginalUserName { get; set; } = string.Empty;

        /// <summary>
        /// 实际审批员工姓名
        /// </summary>
        public string OperationUserName { get; set; } = string.Empty;

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ReviewDateTime { get; set; }
    }
}
