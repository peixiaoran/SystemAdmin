using SqlSugar;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Entity
{
    /// <summary>
    /// 厂商信息实体类
    /// </summary>
    [SugarTable("[CustMat].[ManufacturerInfo]")]
    public class ManufacturerInfoEntity
    {
        /// <summary>
        /// 厂商Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long ManufacturerId { get; set; }

        /// <summary>
        /// 厂商编码
        /// </summary>
        public string ManufacturerCode { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称（中文）
        /// </summary>
        public string ManufacturerNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称（英文）
        /// </summary>
        public string ManufacturerNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 厂商邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 厂商传真
        /// </summary>
        public string Fax { get; set; } = string.Empty;

        /// <summary>
        /// 厂商描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
