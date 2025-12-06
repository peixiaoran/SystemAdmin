namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Commands
{
    /// <summary>
    /// 表单类别新增/修改类
    /// </summary>
    public class FormTypeUpsert
    {
        /// <summary>
        /// 表单类别Id
        /// </summary>
        public string FormTypeId { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别Id
        /// </summary>
        public string FormGroupId { get; set; } = string.Empty;

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
        /// 是否启用（1、是 0、否）
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 表单类型描述（中文）
        /// </summary>
        public string DescriptionCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型描述（英文）
        /// </summary>
        public string DescriptionEn { get; set; } = string.Empty;
    }
}
