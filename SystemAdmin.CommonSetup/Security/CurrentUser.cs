using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SystemAdmin.CommonSetup.Security
{
    /// <summary>
    /// 当前登录用户信息访问器
    /// </summary>
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public const string ClaimUserId = "uid";
        public const string ClaimUserNo = "uno";

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

        public long UserId => GetLongClaim(ClaimUserId);

        public string UserNo => GetStringClaim(ClaimUserNo);

        public ClaimsPrincipal? ClaimsPrincipal => Principal;

        private string GetStringClaim(string claimType)
        {
            var value = Principal?.FindFirst(claimType)?.Value;
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        private long GetLongClaim(string claimType)
        {
            var value = Principal?.FindFirst(claimType)?.Value;
            return long.TryParse(value, out var id) ? id : 0L;
        }
    }
}
