using Microsoft.Extensions.Logging;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemConfig;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemConfig
{
    public class UserLoginLogService
    {
        private readonly ILogger<UserLoginLogService> _logger;
        private readonly UserLoginLogRepository _userLoginLogRepository;

        public UserLoginLogService(ILogger<UserLoginLogService> logger, UserLoginLogRepository userLoginLogRepository)
        {
            _logger = logger;
            _userLoginLogRepository = userLoginLogRepository;
        }

        /// <summary>
        /// 查询员工登录日志分页
        /// </summary>
        /// <param name="getUserLoginLogPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLogOutDto>> GetUserLoginLogPage(GetUserLoginLogPage getUserLoginLogPage)
        {
            try
            {
                var userLoginLogPage = await _userLoginLogRepository.GetUserLoginLogPage(getUserLoginLogPage);
                return userLoginLogPage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserLogOutDto>.Failure(500, ex.Message);
            }
        }
    }
}
