using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [SugarTable("[Basic].[RoleInfo]")]
    public class RoleInfoEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long RoleId { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称（中文）
        /// </summary>
        public string RoleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称（英文）
        /// </summary>
        public string RoleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
