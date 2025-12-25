using System.Text.Json.Serialization;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    public class FormStepEntityDto
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
        /// 审批人选取方式（依组织架构、指定部门员工级别、指定员工、自定义）
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

        /// <summary>
        /// 签核步骤组织架构来源实体
        /// </summary>
        public FormStepOrgEntity formStepOrgEntity { get; set; } = new FormStepOrgEntity();

        /// <summary>
        /// 签核步骤组织架构来源实体
        /// </summary>
        public FormStepDeptCriteriaEntity formStepDeptCriteriaEntity { get; set; } = new FormStepDeptCriteriaEntity();

        /// <summary>
        /// 签核步骤指定员工来源实体
        /// </summary>
        public FormStepUserEntity formStepUserEntity { get; set; } = new FormStepUserEntity();

        /// <summary>
        /// 签核步骤自定义来源实体
        /// </summary>
        public FormStepApproverRuleEntity formStepApproverRuleEntity { get; set; } = new FormStepApproverRuleEntity();
    }
}
