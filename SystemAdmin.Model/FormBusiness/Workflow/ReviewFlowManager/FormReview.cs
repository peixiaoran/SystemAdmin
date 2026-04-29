using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Workflow.ReviewFlowManager
{
    /// <summary>
    /// 表单流程
    /// </summary>
    public class FormReview
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 驳回次数
        /// </summary>
        public int RejectCount { get; set; }

        /// <summary>
        /// 步骤审批人员列表
        /// </summary>
        public List<StepReview> stepReviewFlowList { get; set; } = new List<StepReview>();
    }
}
