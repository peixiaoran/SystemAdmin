namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
    /// <summary>
    /// 模块新增/修改类
    /// </summary>
    public class ModuleInfoUpsert
    {
        /// <summary>
        /// 模块主键Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（中文）
        /// </summary>
        public string ModuleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（英文）
        /// </summary>
        public string ModuleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>
        /// 模块图标
        /// </summary>
        public string ModuleIcon { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        public int IsVisible { get; set; }

        /// <summary>
        /// 模块层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 模块路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 备注（中文）
        /// </summary>
        public string RemarkCh { get; set; } = string.Empty;

        /// <summary>
        /// 备注（英文）
        /// </summary>
        public string RemarkEn { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
