using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemAuth.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemAuth;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemAuth
{
    [Route("api/SystemBasicMgmt/SystemAuth/[controller]/[action]")]
    [ApiController]
    public class SysUserOperate : ControllerBase
    {
        private readonly SysUserOperateService _sysUserOperateService;

        public SysUserOperate(SysUserOperateService sysUserOperateService)
        {
            _sysUserOperateService = sysUserOperateService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 员工登录")]
        [AllowAnonymous]
        public async Task<Result<SysUserLoginReturnDto>> UserLogin([FromBody] UserLogin sysLogin)
        {
            return await _sysUserOperateService.UserLogin(Response, sysLogin);
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 解锁账号发送验证码")]
        [AllowAnonymous]
        public async Task<Result<string>> UnLockSendVcCode([FromQuery] string userNo)
        {
            return await _sysUserOperateService.UnLockSendVcCode(userNo);
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 解锁账号（重置密码）")]
        [AllowAnonymous]
        public async Task<Result<int>> UserUnlock([FromBody] UserUnlock userUnlock)
        {
            return await _sysUserOperateService.UserUnLock(userUnlock);
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 密码过期发送验证码")]
        [AllowAnonymous]
        public async Task<Result<string>> UnExpirationSendVcCode([FromQuery] string userNo)
        {
            return await _sysUserOperateService.UnExpirationSendVcCode(userNo);
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 密码过期（重置密码）")]
        [AllowAnonymous]
        public async Task<Result<int>> UserPwdExpiration([FromBody] PwdExpiration pwdExpirationUpsert)
        {
            return await _sysUserOperateService.UserPwdExpiration(pwdExpirationUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统接口")]
        [EndpointSummary("[系统接口] 员工登出")]
        public async Task<Result<int>> UserLogOut()
        {
            return await _sysUserOperateService.UserLogOut();
        }
    }
}
