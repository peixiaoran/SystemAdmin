namespace SystemAdmin.Common.Enums.FormBusiness
{
    /// <summary>
    /// 流程审批步骤选取人员来源方式枚举
    /// </summary>
    public enum StepAssignment
    {
        /// <summary>
        /// 组织架构
        /// </summary>
        Org = 1,

        /// <summary>
        /// 指定部门员工级别
        /// </summary>
        DeptUser = 2,

        /// <summary>
        /// 指定员工
        /// </summary>
        User = 3,

        /// <summary>
        /// 自定义规则
        /// </summary>
        Rule = 4,
    }
}
