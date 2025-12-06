using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 审批人选取方式下拉框Dto
    /// </summary>
    public class AssignmentDropDto
    {
        /// <summary>
        /// 审批人选取方式编码
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int ApproverAssignmentCode { get; set; }

        /// <summary>
        /// 审批人选取方式名称
        /// </summary>
        public string ApproverAssignmentName { get; set; } = string.Empty;
    }
}
