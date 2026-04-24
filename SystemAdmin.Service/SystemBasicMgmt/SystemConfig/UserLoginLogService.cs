using Microsoft.Extensions.Logging;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemConfig;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemConfig
{
    public class UserLoginLogService
    {
        private readonly ILogger<UserLoginLogService> _logger;
        private readonly UserLoginLogRepository _userLoginLogRepo;

        public UserLoginLogService(ILogger<UserLoginLogService> logger, UserLoginLogRepository userLoginLogRepo)
        {
            _logger = logger;
            _userLoginLogRepo = userLoginLogRepo;
        }

        /// <summary>
        /// 查询员工登录日志分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLogOutDto>> GetUserLoginLogPage(GetUserLoginLogPage getPage)
        {
            try
            {
                var page = await _userLoginLogRepo.GetUserLoginLogPage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserLogOutDto>.Failure(500, ex.Message);
            }
        }
    }
}
