using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Dto
{
    /// <summary>
    /// 待签核表单Dto
    /// </summary>
    public class PendingApprovalDto
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
        /// 表单类型
        /// </summary>
        public string FormTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 表单状态
        /// </summary>
        public string FormStatusName { get; set; } = string.Empty;

        /// <summary>
        /// 表单签核路径
        /// </summary>
        public string ApprovalPath { get; set; } = string.Empty;

        /// <summary>
        /// 表单视图路径
        /// </summary>
        public string ViewPath { get; set; } = string.Empty;
    }
}
