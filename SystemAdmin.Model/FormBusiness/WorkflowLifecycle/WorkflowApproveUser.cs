using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.WorkflowLifecycle
{
    /// <summary>
    /// 流程审批人员Dto
    /// </summary>
    public class WorkflowApproveUser
    {
        /// <summary>
        /// 审批步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批步骤签核人员列表
        /// </summary>
        public List<StepApproveUser> stepApproveUsers { get; set; } = new List<StepApproveUser>();
    }
}
