using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemMgmt;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemMgmt
{
    public class RoleService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<RoleService> _logger;
        private readonly SqlSugarScope _db;
        private readonly RoleRepository _roleRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt_SystemMgmt_Role_";

        public RoleService(CurrentUser loginuser, ILogger<RoleService> logger, SqlSugarScope db, RoleRepository roleRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _roleRepository = roleRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertRole(RoleInfoUpsert roleUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var insertRoleEntity = new RoleInfoEntity
                {
                    RoleId = SnowFlakeSingle.Instance.NextId(),
                    RoleCode = roleUpsert.RoleCode,
                    RoleNameCn = roleUpsert.RoleNameCn,
                    RoleNameEn = roleUpsert.RoleNameEn,
                    Description = roleUpsert.Description,
                    IsEnabled = roleUpsert.IsEnabled,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Remark = roleUpsert.Remark
                };
                int insertRoleCount = await _roleRepository.InsertRole(insertRoleEntity);
                await _db.CommitTranAsync();

                return insertRoleCount >= 1
                    ? Result<int>.Ok(insertRoleCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                    : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteRole(RoleInfoUpsert roleUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 查询角色是否被使用
                bool isRoleUsed = await _roleRepository.GetUserRoleIsExist(long.Parse(roleUpsert.RoleId));
                if (!isRoleUsed)
                {
                    // 删除角色
                    int delRoleCount = await _roleRepository.DeleteRole(long.Parse(roleUpsert.RoleId));
                    // 删除角色模块绑定
                    int delRoleModuleCount = await _roleRepository.DeleleRoleModule(long.Parse(roleUpsert.RoleId));
                    // 删除角色菜单绑定
                    int delRoleMenuCount = await _roleRepository.DeleteRoleMenu(long.Parse(roleUpsert.RoleId));
                   
                    await _db.CommitTranAsync();
                    return delRoleCount >= 1
                           ? Result<int>.Ok(delRoleCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                           : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
                }
                else
                {
                    await _db.RollbackTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}RoleUsed"));
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateRole(RoleInfoUpsert roleUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var updateRoleEntity = new RoleInfoEntity
                {
                    RoleId = long.Parse(roleUpsert.RoleId),
                    RoleCode = roleUpsert.RoleCode,
                    RoleNameCn = roleUpsert.RoleNameCn,
                    RoleNameEn = roleUpsert.RoleNameEn,
                    Description = roleUpsert.Description,
                    IsEnabled = roleUpsert.IsEnabled,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Remark = roleUpsert.Remark
                };

                int updateRoleCount = await _roleRepository.UpdateRole(updateRoleEntity);
                await _db.CommitTranAsync();

                return updateRoleCount >= 1
                    ? Result<int>.Ok(updateRoleCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                    : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询角色实体
        /// </summary>
        /// <param name="getRoleEntity"></param>
        /// <returns></returns>
        public async Task<Result<RoleInfoDto>> GetRoleEntity(GetRoleInfoEntity getRoleEntity)
        {
            try
            {
                var roleEntity = await _roleRepository.GetRoleEntity(long.Parse(getRoleEntity.RoleId));
                return Result<RoleInfoDto>.Ok(roleEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<RoleInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询角色分页
        /// </summary>
        /// <param name="getRolePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<RoleInfoDto>> GetRolePage(GetRoleInfoPage getRolePage)
        {
            try
            {
                var rolePage = await _roleRepository.GetRolePage(getRolePage);
                return rolePage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<RoleInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询角色模块绑定列表
        /// </summary>
        /// <param name="getRoleModuleList"></param>
        /// <returns></returns>
        public async Task<Result<List<RoleModuleDto>>> GetRoleModuleList(GetRoleModuleList getRoleModuleList)
        {
            try
            {
                var roleModuleList = await _roleRepository.GetRoleModuleList(long.Parse(getRoleModuleList.RoleId));
                return Result<List<RoleModuleDto>>.Ok(roleModuleList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<RoleModuleDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询角色菜单绑定树
        /// </summary>
        /// <param name="getRoleMenuTree"></param>
        /// <returns></returns>
        public async Task<Result<List<RoleMenuDto>>> GetRoleMenuTree(GetRoleMenuTree getRoleMenuTree)
        {
            try
            {
                var roleMenuList = await _roleRepository.GetRoleMenuTree(long.Parse(getRoleMenuTree.RoleId), long.Parse(getRoleMenuTree.ModuleId));
                return Result<List<RoleMenuDto>>.Ok(roleMenuList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<RoleMenuDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改角色模块绑定
        /// </summary>
        /// <param name="roleModuleUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateRoleModuleList(RoleModuleUpsert roleModuleUpsert)
        {
            try
            {
                List<long> UnSelectdModuleIds = roleModuleUpsert.UnSelectedModuleIds
                                                       .Where(moduleId => long.TryParse(moduleId, out _))
                                                       .Select(long.Parse)
                                                       .ToList();
                // 查询未选中模块下的菜单Id
                var delMenuIds = await _roleRepository.GetMenuIds(UnSelectdModuleIds);
                // 删除角色菜单绑定
                var delRoleMenuCount = await _roleRepository.DeleleRoleMenu(long.Parse(roleModuleUpsert.RoleId), delMenuIds);
                // 删除角色全部模块绑定
                var delRoleModuleCount = await _roleRepository.DeleleRoleModule(long.Parse(roleModuleUpsert.RoleId));

                // 再新增角色模块绑定
                List<RoleModuleEntity> insertRoleModuleList = roleModuleUpsert.SelectedModuleIds
                                                      .Select(moduleId => new RoleModuleEntity
                                                      {
                                                          RoleId = long.Parse(roleModuleUpsert.RoleId),
                                                          ModuleId = long.Parse(moduleId),
                                                          CreatedBy = _loginuser.UserId,
                                                          CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                          ModifiedBy = _loginuser.UserId,
                                                          ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                      }).ToList();
                var insertRoleModuleCount = await _roleRepository.InsertRoleModule(insertRoleModuleList);
                await _db.CommitTranAsync();

                return Result<int>.Ok(insertRoleModuleCount, _localization.ReturnMsg($"{_this}RoleModuleUpdateSuccess")); 
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 角色模块下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<RoleModuleDropDto>>> GetRoleModuleDropDown(GetRoleModuleDropDown getRoleModuleDrop)
        {
            try
            {
                var roleModuleDrop = await _roleRepository.GetRoleModuleDropDown(long.Parse(getRoleModuleDrop.RoleId));
                return Result<List<RoleModuleDropDto>>.Ok(roleModuleDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<RoleModuleDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改角色菜单绑定
        /// </summary>
        /// <param name="roleMenuUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateRoleMenuList(RoleMenuUpsert roleMenuUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除角色全部菜单绑定
                var delMenuIds = await _roleRepository.GetModuleMenuIds(long.Parse(roleMenuUpsert.ModuleId));
                var delRoleMenuIdCount = await _roleRepository.DeleteRoleMenu(long.Parse(roleMenuUpsert.RoleId), delMenuIds);
                // 再新增角色菜单绑定
                List<RoleMenuEntity> insertRoleMenuList = roleMenuUpsert.SelectedMenuIds
                                                    .Select(menuid => new RoleMenuEntity
                                                    {
                                                        RoleId = long.Parse(roleMenuUpsert.RoleId),
                                                        MenuId = long.Parse(menuid),
                                                        CreatedBy = _loginuser.UserId,
                                                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                        ModifiedBy = _loginuser.UserId,
                                                        ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    }).ToList();
                var insertRoleMenuCount = await _roleRepository.InsertRoleMenu(insertRoleMenuList);
                await _db.CommitTranAsync();

                return Result<int>.Ok(insertRoleMenuCount, _localization.ReturnMsg($"{_this}RoleMenuUpdateSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
