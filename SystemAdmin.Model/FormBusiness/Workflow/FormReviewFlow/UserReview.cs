using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow
{
    /// <summary>
    /// 步骤审批人员
    /// </summary>
    public class UserReview
    {
        /// <summary>
        /// 审批人员Id - 实或兼
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 审批人员姓名 - 实或兼
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人员Id - 代
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long AgentUserId { get; set; }

        /// <summary>
        /// 审批人员姓名 - 代
        /// </summary>
        public string AgentUserName { get; set; } = string.Empty;

        /// <summary>
        /// 审批身份名称
        /// </summary>
        public string AppointmentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 目前状态（未审批、审批中、审批完成）
        /// </summary>
        public string Result { get; set; } = string.Empty;
    }
}
