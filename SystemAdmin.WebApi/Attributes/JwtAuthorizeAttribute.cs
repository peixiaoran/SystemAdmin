using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SystemAdmin.WebApi.Attributes
{
    /// <summary>
    /// 最基础 JWT 验证，判断是否已认证
    /// 使用：[JwtAuthorize]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JwtAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user?.Identity?.IsAuthenticated != true)
            {
                // 未认证 => 401
                context.Result = new UnauthorizedResult();
            }

            return Task.CompletedTask;
        }
    }
}
