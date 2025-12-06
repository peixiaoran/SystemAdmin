using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 签核步骤组织结构来源Dto
    /// </summary>
    public class FormStepOrgDto
    {
        /// <summary>
        /// 签核步骤组织结构Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepOrgId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 部门来源Ids
        /// </summary>
        public string OrgDeptLeaveIds { get; set; } = string.Empty;

        /// <summary>
        /// 职级来源Ids
        /// </summary>
        public string OrgPositionIds { get; set; } = string.Empty;

        /// <summary>
        /// 职业来源Ids
        /// </summary>
        public string OrgLaborIds { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
