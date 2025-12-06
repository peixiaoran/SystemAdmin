namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Commands
{
    /// <summary>
    /// 字典新增/修改类
    /// </summary>
    public class DictionaryInfoUpsert
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        public string DicId { get; set; } = string.Empty;

        /// <summary>
        /// 所属模块Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DicType { get; set; } = string.Empty;

        /// <summary>
        /// 字典编码
        /// </summary>
        public int DicCode { get; set; }

        /// <summary>
        /// 字典名称（中文）
        /// </summary>
        public string DicNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称（英文）
        /// </summary>
        public string DicNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 字典排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
