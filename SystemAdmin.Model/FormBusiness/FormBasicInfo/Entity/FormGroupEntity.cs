using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity
{
    /// <summary>
    /// 表单组别实体类
    /// </summary>
    [SugarTable("[Form].[FormGroup]")]
    public class FormGroupEntity
    {
        /// <summary>
        /// 表单组别Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long FormGroupId { get; set; }

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
    }
}
