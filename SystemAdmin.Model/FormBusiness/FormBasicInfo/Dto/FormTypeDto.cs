using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto
{
    public class FormTypeDto
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单组别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormGroupId { get; set; }

        /// <summary>
        /// 表单组别名称
        /// </summary>
        public string FormGroupName { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别名称（中文）
        /// </summary>
        public string FormTypeNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别名称（英文）
        /// </summary>
        public string FormTypeNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; } = string.Empty;

        /// <summary>
        /// 表单审批路径
        /// </summary>
        public string ApprovalPath { get; set; } = string.Empty;

        /// <summary>
        /// 表单视图路径
        /// </summary>
        public string ViewPath { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 表单类别描述（中文）
        /// </summary>
        public string DescriptionCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单类别描述（英文）
        /// </summary>
        public string DescriptionEn { get; set; } = string.Empty;
    }
}
