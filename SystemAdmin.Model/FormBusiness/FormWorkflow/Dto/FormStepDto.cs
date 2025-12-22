using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    /// <summary>
    /// 表单审批步骤Dto
    /// </summary>
    public class FormStepDto
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long StepId { get; set; }

        /// <summary>
        /// 表单步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人选取方式（依组织架构、指定部门人员级别、指定人员、自定义）
        /// </summary>
        public int Assignment { get; set; }

        /// <summary>
        /// 审批人选取方式名称
        /// </summary>
        public string AssignmentName { get; set; } = string.Empty;

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
