namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Commands
{
    /// <summary>
    /// 签核层级上限新增/修改类
    /// </summary>
    public class FormReviewLimitUpsert
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 职级Id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 签核最高职级Id
        /// </summary>
        public long MaxPositionId { get; set; }
    }
}
