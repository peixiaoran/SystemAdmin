using Microsoft.Extensions.Logging;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.Auth.Dto;
using SystemAdmin.Model.SystemBasicMgmt.Auth.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.Auth;

namespace SystemAdmin.Service.SystemBasicMgmt.Auth
{
    public class SysModuleMenuService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<SysModuleMenuService> _logger;
        private readonly SysModuleMenuRepository _sysModuleMenuRepository;

        public SysModuleMenuService(CurrentUser loginuser, ILogger<SysModuleMenuService> logger, SysModuleMenuRepository sysModuleMenuRepository)
        {
            _loginuser = loginuser;
            _logger = logger;
            _sysModuleMenuRepository = sysModuleMenuRepository;
        }

        /// <summary>
        /// 查询模块列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<SysModuleInfoDto>>> GetModuleList()
        {
            try
            {
                List<SysModuleInfoDto> moduleList = await _sysModuleMenuRepository.GetModuleList(_loginuser.UserId);
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
        /// <param name="getSysMenu"></param>
        /// <returns></returns>
        public async Task<Result<List<SysMenuInfoDto>>> GetMenuTreeList(GetSysMenu getSysMenu)
        {
            try
            {
                List<SysMenuInfoDto> menuTree = await _sysModuleMenuRepository.GetMenuTreeList(getSysMenu, _loginuser.UserId);
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
