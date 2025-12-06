using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.Auth.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.Auth
{
    public class SysPerVerifyRepository
    {
        private readonly SqlSugarScope _db;

        public SysPerVerifyRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 验证员工是否有权限访问接口
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <param name="routePath"></param>
        /// <returns></returns>
        public async Task<bool> HasPermission(long loginUserId, string routePath)
        {
            var hasPermissionList = await _db.Queryable<SysUserInfoEntity>()
                .With(SqlWith.NoLock)
                .LeftJoin<SysUserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                .LeftJoin<SysRoleInfoEntity>((user, userrole, role) => userrole.RoleId == role.RoleId)
                .LeftJoin<SysRoleMenuEntity>((user, userrole, role, rolemenu) => role.RoleId == rolemenu.RoleId)
                .LeftJoin<SysMenuInfoEntity>((user, userrole, role, rolemenu, menu) => rolemenu.MenuId == menu.MenuId)
                .Where((user, userrole, role, rolemenu, menu) => user.UserId == loginUserId && menu.RoutePath == routePath)
                .AnyAsync();
            return hasPermissionList;
        }
    }
}
