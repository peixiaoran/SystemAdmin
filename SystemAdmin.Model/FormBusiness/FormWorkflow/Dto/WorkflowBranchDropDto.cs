using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 流程分支下拉Dto
    /// </summary>
    public class WorkflowBranchDropDto
    {
        /// <summary>
        /// 流程分支Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BranchId { get; set; }

        /// <summary>
        /// 流程分支名称
        /// </summary>
        public string BranchName { get; set; } = string.Empty;
    }
}
