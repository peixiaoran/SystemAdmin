using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// <summary>
    /// 国籍实体类
    /// </summary>
    [SugarTable("[Basic].[NationalityInfo]")]
    public class NationalityInfoEntity
    {
        /// <summary>
        /// 国籍Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long NationId { get; set; }

        /// <summary>
        /// 国籍名称（中文）
        /// </summary>
        public string NationNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 国籍名称（英文）
        /// </summary>
        public string NationNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

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
