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
        private readonly string _this = "SystemBasicMgmt.SystemMgmt.Role";

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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertRole(RoleInfoUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                RoleInfoEntity entity = new RoleInfoEntity
                {
                    RoleId = SnowFlakeSingle.Instance.NextId(),
                    RoleCode = upsert.RoleCode,
                    RoleNameCn = upsert.RoleNameCn,
                    RoleNameEn = upsert.RoleNameEn,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                    Remark = upsert.Remark
                };
                int count = await _roleRepository.InsertRole(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteRole(RoleInfoUpsert upsert)
        {
            try
            {
                // 查询角色是否被使用
                bool isRoleUsed = await _roleRepository.GetUserRoleIsExist(long.Parse(upsert.RoleId));
                if (!isRoleUsed)
                {

                    await _db.BeginTranAsync();
                    // 删除角色
                    int delRoleCount = await _roleRepository.DeleteRole(long.Parse(upsert.RoleId));
                    // 删除角色模块绑定
                    int delRoleModuleCount = await _roleRepository.DeleleRoleModule(long.Parse(upsert.RoleId));
                    // 删除角色菜单绑定
                    int delRoleMenuCount = await _roleRepository.DeleteRoleMenu(long.Parse(upsert.RoleId));
                   
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateRole(RoleInfoUpsert upsert)
        {
            try
            {
                RoleInfoEntity entity = new RoleInfoEntity
                {
                    RoleId = long.Parse(upsert.RoleId),
                    RoleCode = upsert.RoleCode,
                    RoleNameCn = upsert.RoleNameCn,
                    RoleNameEn = upsert.RoleNameEn,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                    Remark = upsert.Remark
                };

                await _db.BeginTranAsync();
                int count = await _roleRepository.UpdateRole(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<RoleInfoDto>> GetRoleEntity(GetRoleInfoEntity getEntity)
        {
            try
            {
                var entity = await _roleRepository.GetRoleEntity(long.Parse(getEntity.RoleId));
                return Result<RoleInfoDto>.Ok(entity, "");
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<RoleInfoDto>> GetRolePage(GetRoleInfoPage getPage)
        {
            try
            {
                var page = await _roleRepository.GetRolePage(getPage);
                return page;
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
        /// <param name="getList"></param>
        /// <returns></returns>
        public async Task<Result<List<RoleModuleDto>>> GetRoleModuleList(GetRoleModuleList getList)
        {
            try
            {
                var list = await _roleRepository.GetRoleModuleList(long.Parse(getList.RoleId));
                return Result<List<RoleModuleDto>>.Ok(list, "");
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
        /// <param name="getTree"></param>
        /// <returns></returns>
        public async Task<Result<List<RoleMenuDto>>> GetRoleMenuTree(GetRoleMenuTree getTree)
        {
            try
            {
                var list = await _roleRepository.GetRoleMenuTree(long.Parse(getTree.RoleId), long.Parse(getTree.ModuleId));
                return Result<List<RoleMenuDto>>.Ok(list, "");
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
                List<long> unSelectdModuleIds = roleModuleUpsert.UnSelectedModuleIds
                                                .Where(moduleId => long.TryParse(moduleId, out _))
                                                .Select(long.Parse)
                                                .ToList();
                // 查询未选中模块下的菜单Id
                var delMenuIds = await _roleRepository.GetMenuIds(unSelectdModuleIds);
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
                                           CreatedDate = DateTime.Now,
                                           ModifiedBy = _loginuser.UserId,
                                           ModifiedDate = DateTime.Now,
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
        /// 角色模块下拉
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
                                         CreatedDate = DateTime.Now,
                                         ModifiedBy = _loginuser.UserId,
                                         ModifiedDate = DateTime.Now,
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
