using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Entity
{
    /// <summary>
    /// 员工权限实体
    /// </summary>
    [SugarTable("[Basic].[UserRole]")]
    public class SysUserRoleEntity
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
    }
}
