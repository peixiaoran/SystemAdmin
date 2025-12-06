using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// 职级实体
    /// </summary>
    [SugarTable("[Basic].[UserPosition]")]
    public class UserPositionEntity
    {
        /// <summary>
        /// 职级Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long PositionId { get; set; }

        /// <summary>
        /// 职级编码
        /// </summary>
        public string PositionNo { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称（中文）
        /// </summary>
        public string PositionNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称（英文）
        /// </summary>
        public string PositionNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职级排序
        /// </summary>
        public int PositionOrderBy { get; set; }

        /// <summary>
        /// 职级描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
