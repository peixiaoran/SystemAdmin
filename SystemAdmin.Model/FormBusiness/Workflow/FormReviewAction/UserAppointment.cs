using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewAction
{
    /// <summary>
    /// 步骤审批人员身份
    /// </summary>
    public class UserAppointment
    {
        /// <summary>
        /// 审批人员Id - 实或兼
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ReviewUserId { get; set; }

        /// <summary>
        /// 审批人员Id - 代或兼代
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long AgentUserId { get; set; }

        /// <summary>
        /// 审批身份类型
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;
    }
}
