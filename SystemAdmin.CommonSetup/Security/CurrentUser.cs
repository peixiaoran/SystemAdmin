using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SystemAdmin.CommonSetup.Security
{
    /// <summary>
    /// 当前登录用户信息访问器（基于 HttpContext.User）
    /// </summary>
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

        /// <summary>是否已登录</summary>
        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

        /// <summary>用户 Id（从 uid Claim 获取）</summary>
        public long? UserId
        {
            get
            {
                var value = Principal?.FindFirst("uid")?.Value;
                if (long.TryParse(value, out var id))
                    return id;
                return null;
            }
        }

        /// <summary>用户编号（从 uno Claim 获取）</summary>
        public string? UserNo => Principal?.FindFirst("uno")?.Value;

        /// <summary>原始 ClaimsPrincipal</summary>
        public ClaimsPrincipal? ClaimsPrincipal => Principal;
    }
}
