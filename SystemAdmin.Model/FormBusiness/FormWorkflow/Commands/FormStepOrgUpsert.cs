namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    public class FormStepOrgUpsert
    {
        /// <summary>
        /// 签核步骤组织架构Id
        /// </summary>
        public string StepOrgId { get; set; } = string.Empty;

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;

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
