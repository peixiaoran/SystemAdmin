using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow.Dto
{
    /// <summary>
    /// 表单审批人员
    /// </summary>
    public class StepReview
    {
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
        /// 是否跳过
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// 步骤审批人员列表
        /// </summary>
        public List<UserReview> stepReviewUser { get; set; } = new List<UserReview>();
    }
}
