namespace SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow.Dto
{
    /// <summary>
    /// 申请人表单详情
    /// </summary>
    public class ApplicantFormDetail
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 表单类型Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 规则Id
        /// </summary>
        public long RuleId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public long CurrentStepId { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public long ApplicantUserId { get; set; }

        /// <summary>
        /// 申请人部门Id
        /// </summary>
        public long ApplicantDeptId { get; set; }

        /// <summary>
        /// 申请人部门等级
        /// </summary>
        public int DeptLevelSort { get; set; }

        /// <summary>
        /// 申请人职级等级
        /// </summary>
        public int PositionSort { get; set; }
    }
}
