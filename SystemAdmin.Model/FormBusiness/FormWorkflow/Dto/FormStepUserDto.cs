namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    public class FormStepUserDto
    {
        /// <summary>
        /// 签核步骤指定员工Id
        /// </summary>
        public long StepUserId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public long StepId { get; set; }

        /// <summary>
        /// 部门级别Ids
        /// </summary>
        public string UserIds { get; set; } = string.Empty;
    }
}
