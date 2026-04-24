using Microsoft.Extensions.Logging;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemAuth;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemAuth
{
    public class SysModuleMenuService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<SysModuleMenuService> _logger;
        private readonly SysModuleMenuRepository _sysModuleMenuRepo;

        public SysModuleMenuService(CurrentUser loginuser, ILogger<SysModuleMenuService> logger, SysModuleMenuRepository sysModuleMenuRepo)
        {
            _loginuser = loginuser;
            _logger = logger;
            _sysModuleMenuRepo = sysModuleMenuRepo;
        }

        /// <summary>
        /// 查询模块列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<SysModuleInfoDto>>> GetModuleList()
        {
            try
            {
                List<SysModuleInfoDto> moduleList = await _sysModuleMenuRepo.GetModuleList(_loginuser.UserId);
                return Result<List<SysModuleInfoDto>>.Ok(moduleList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<SysModuleInfoDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询菜单树
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<Result<List<SysMenuInfoDto>>> GetMenuTreeList(string moduleId)
        {
            try
            {
                List<SysMenuInfoDto> menuTree = await _sysModuleMenuRepo.GetMenuTreeList(long.Parse(moduleId), _loginuser.UserId);
                return Result<List<SysMenuInfoDto>>.Ok(menuTree, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<SysMenuInfoDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
