using SqlSugar;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Entity
{
    /// <summary>
    /// 料号信息实体类
    /// </summary>
    [SugarTable("[CustMat].[PartNumberInfo]")]
    public class PartNumberInfoEntity
    {
        /// <summary>
        /// 料号Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long PartNumberId { get; set; }

        /// <summary>
        /// 所属厂商Id
        /// </summary>
        public long ManufacturerId { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string PartNumberNo { get; set; } = string.Empty;

        /// <summary>
        /// 品名
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 规格
        /// </summary>
        public string Specifications { get; set; } = string.Empty;

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
