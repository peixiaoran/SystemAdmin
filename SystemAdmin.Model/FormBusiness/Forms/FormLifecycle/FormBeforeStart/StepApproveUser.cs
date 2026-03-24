using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart
{
    /// <summary>
    /// 步骤待签核人
    /// </summary>
    public class StepApproveUser
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 员工姓名（本或兼）
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 员工Id（代）
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long AgentUserId { get; set; }

        /// <summary>
        /// 员工姓名（代）
        /// </summary>
        public string AgentUserName { get; set; } = string.Empty;

        /// <summary>
        /// 签核类型（实任、兼任、代理）
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// 签核类型名称（实任、兼任、代理）
        /// </summary>
        public string AppointmentTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 是否完成签核
        /// </summary>
        public int IsPending { get; set; }
    }
}
