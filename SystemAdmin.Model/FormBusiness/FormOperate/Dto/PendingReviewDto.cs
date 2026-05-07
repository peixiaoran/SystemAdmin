using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Dto
{
    /// <summary>
    /// 待送审 & 审批表单Dto
    /// </summary>
    public class PendingReviewDto
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 表单单号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public string FormTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 表单状态
        /// </summary>
        public string FormStatus { get; set; } = string.Empty;

        /// <summary>
        /// 表单状态名称
        /// </summary>
        public string FormStatusName { get; set; } = string.Empty;

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplyUserName { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门名称
        /// </summary>
        public string ApplyUserDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 表单签核路径
        /// </summary>
        public string ApprovalPath { get; set; } = string.Empty;

        /// <summary>
        /// 表单视图路径
        /// </summary>
        public string ViewPath { get; set; } = string.Empty;

        /// <summary>
        /// 是否可以作废
        /// </summary>
        public int isVoided { get; set; }
    }
}
