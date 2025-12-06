using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Dto
{
    /// <summary>
    /// 申请表单信息Dto
    /// </summary>
    public class ApplyFormInfoDto
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单类别名称
        /// </summary>
        public string FormTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 表单审批路径
        /// </summary>
        public string ApprovalPath { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
