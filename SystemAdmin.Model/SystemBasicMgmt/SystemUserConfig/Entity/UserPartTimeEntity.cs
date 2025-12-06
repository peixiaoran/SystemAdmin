using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity
{
    /// <summary>
    /// 员工兼任实体表
    /// </summary>
    [SugarTable("[Basic].[UserPartTime]")]
    public class UserPartTimeEntity
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 兼任部门Id
        /// </summary>
        public long PartTimeDeptId { get; set; }

        /// <summary>
        /// 兼任职级Id
        /// </summary>
        public long PartTimePositionId { get; set; }

        /// <summary>
        /// 兼任职业Id
        /// </summary>
        public long PartTimeLaborId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

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
