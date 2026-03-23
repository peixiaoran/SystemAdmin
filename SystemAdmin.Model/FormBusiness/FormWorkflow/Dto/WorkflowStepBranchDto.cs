using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 步骤流程分支Dto
    /// </summary>
    public class WorkflowStepBranchDto
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BranChId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 条件Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ConditionId { get; set; }

        /// <summary>
        /// 条件名称
        /// </summary>
        public string ConditionName { get; set; } = string.Empty;

        /// <summary>
        /// 同时符合多条件时，是否执行此条件
        /// </summary>
        public int ExecuteMatched { get; set; }

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
