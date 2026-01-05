using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Entity
{
    /// <summary>
    /// 模块实体类
    /// </summary>
    [SugarTable("[Basic].[ModuleInfo]")]
    public class SysModuleInfoEntity
    {
        /// <summary>
        /// 模块主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long ModuleId { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（中文）
        /// </summary>
        public string ModuleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称（英文）
        /// </summary>
        public string ModuleNameEn { get; set; } = string.Empty;

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
        public int IsVisible { get; set; }

        /// <summary>
        /// 模块路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 备注（中午）
        /// </summary>
        public string RemarkCh { get; set; } = string.Empty;

        /// <summary>
        /// 备注（英文）
        /// </summary>
        public string RemarkEn { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<ModuleInfoEntity> ModuleChildList { get; set; } = new List<ModuleInfoEntity>();
    }
}
