using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 分支下拉Dto
    /// </summary>
    public class WorkflowBranchDropDto
    {
        /// <summary>
        /// 分支Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BranchId { get; set; }

        /// <summary>
        /// 分支名称
        /// </summary>
        public string BranchName { get; set; } = string.Empty;
    }
}
