namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Commands
{
    /// <summary>
    /// 表单组别新增/修改类
    /// </summary>
    public class FormGroupUpsert
    {
        /// <summary>
        /// 表单组别Id
        /// </summary>
        public string FormGroupId { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别名称（中文）
        /// </summary>
        public string FormGroupNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别名称（英文）
        /// </summary>
        public string FormGroupNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 表单组别描述（中文）
        /// </summary>
        public string DescriptionCn { get; set; } = string.Empty;

        /// <summary>
        /// 表单组别描述（英文）
        /// </summary>
        public string DescriptionEn { get; set; } = string.Empty;
    }
}
