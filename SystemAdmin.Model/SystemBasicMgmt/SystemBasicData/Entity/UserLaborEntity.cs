using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity
{
    /// <summary>
    /// 职业实体类
    /// </summary>
    [SugarTable("[Basic].[UserLabor]")]
    public class UserLaborEntity
    {
        /// <summary>
        /// 职业Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long LaborId { get; set; }

        /// <summary>
        /// 职业名称（中文）
        /// </summary>
        public string LaborNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称（英文）
        /// </summary>
        public string LaborNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职业描述
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
