using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity
{
    /// <summary>
    /// 字典实体类
    /// </summary>
    [SugarTable("[Basic].[DictionaryInfo]")]
    public class DictionaryInfoEntity
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long DicId { get; set; }

        /// <summary>
        /// 所属模块Id
        /// </summary>
        public long ModuleId { get; set; }

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

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
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
    }
}
