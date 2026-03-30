using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 步骤流程分支Dto
    /// </summary>
    public class WorkflowBranchStepDto
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BranChId { get; set; }

        /// <summary>
        /// 分支Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BranchId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// 下一步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long NextStepId { get; set; }

        /// <summary>
        /// 下一步骤名称
        /// </summary>
        public string NextStepName { get; set; } = string.Empty;
    }
}
