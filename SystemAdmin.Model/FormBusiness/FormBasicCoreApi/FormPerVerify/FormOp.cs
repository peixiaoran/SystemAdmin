namespace SystemAdmin.Model.FormBusiness.FormBasicCoreApi.FormPerVerify
{
    [Flags]
    public enum FormOp
    {
        /// <summary>
        /// 申请
        /// </summary>
        Apply = 1,

        /// <summary>
        /// 查看
        /// </summary>
        View = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,

        /// <summary>
        /// 审批
        /// </summary>
        Approve = 4,

        /// <summary>
        /// 撤回
        /// </summary>
        Cancel = 5,
    }
}
