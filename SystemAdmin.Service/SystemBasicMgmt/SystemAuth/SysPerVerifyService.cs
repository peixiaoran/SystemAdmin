using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Repository.SystemBasicMgmt.SystemAuth;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemAuth
{
    public class SysPerVerifyService
    {
        private readonly ILogger<SysPerVerifyService> _logger;
        private readonly SysPerVerifyRepository _sysPerVerifyRepo;

        public SysPerVerifyService(ILogger<SysPerVerifyService> logger, SqlSugarScope db, SysPerVerifyRepository sysPerVerifyRepo)
        {
            _logger = logger;
            _sysPerVerifyRepo = sysPerVerifyRepo;
        }

        /// <summary>
        /// 验证是否有权限访问接口
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="routePath"></param>
        /// <returns></returns>
        public async Task<bool> HasPermission(long userId, string routePath)
        {
            try
            {
                return await _sysPerVerifyRepo.HasPermission(userId, routePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
