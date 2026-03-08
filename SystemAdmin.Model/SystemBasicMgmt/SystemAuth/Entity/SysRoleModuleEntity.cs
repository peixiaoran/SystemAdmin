using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Entity
{
    /// <summary>
    /// 权限模块实体类
    /// </summary>
    [SugarTable("[Basic].[RoleModule]")]
    public class SysRoleModuleEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public long ModuleId { get; set; }
    }
}
