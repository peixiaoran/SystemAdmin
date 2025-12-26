using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Service.SystemBasicMgmt.SystemAuth;

namespace SystemAdmin.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class RoutingAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            var http = context.HttpContext;
            var loginuser = http.RequestServices.GetRequiredService<CurrentUser>();
            var acl = http.RequestServices.GetRequiredService<SysPerVerifyService>();

            var userId = loginuser.UserId;
            if (userId == 0)
            {
                SetErrorResponse(context, Result<bool>.Failure(401, "Unauthorized: Invalid or expired token."));
                return;
            }

            var resourceKey = BuildResourceKey(context);
            if (string.IsNullOrEmpty(resourceKey))
            {
                SetErrorResponse(context, Result<bool>.Failure(400, "Invalid routing path."));
                return;
            }

            var ok = await acl.HasPermission(userId, resourceKey);
            if (!ok)
            {
                SetErrorResponse(context, Result<bool>.Failure(403, "No permission to access this resource."));
                return;
            }
        }

        private static string BuildResourceKey(AuthorizationFilterContext context)
        {
            var cad = context.ActionDescriptor as ControllerActionDescriptor;
            var actionName = cad?.ActionName ?? string.Empty;

            string key;
            var template = cad?.AttributeRouteInfo?.Template;
            if (!string.IsNullOrWhiteSpace(template))
                key = template.StartsWith("/") ? template : "/" + template;
            else
                key = string.IsNullOrEmpty(context.HttpContext.Request.Path.Value)
                    ? "/"
                    : context.HttpContext.Request.Path.Value;

            var colon = key.IndexOf(':');
            if (colon > 0 && colon <= 8)
            {
                bool looksLikeVerb = true;
                for (int i = 0; i < colon; i++)
                {
                    if (!char.IsLetter(key[i])) { looksLikeVerb = false; break; }
                }
                if (looksLikeVerb)
                    key = key[(colon + 1)..].TrimStart();
            }

            int lastSlash = key.LastIndexOf('/');
            if (lastSlash > 0)
            {
                var lastSeg = key[(lastSlash + 1)..];

                if (!string.IsNullOrEmpty(actionName) &&
                    lastSeg.Equals(actionName, StringComparison.OrdinalIgnoreCase))
                {
                    key = key[..lastSlash];
                }
                else if (lastSeg.Equals("[action]", StringComparison.OrdinalIgnoreCase) ||
                         lastSeg.Equals("{action}", StringComparison.OrdinalIgnoreCase))
                {
                    key = key[..lastSlash];
                }
            }

            while (key.Length > 1 && key.StartsWith("//")) key = key[1..];
            if (key.Length > 1 && key.EndsWith("/")) key = key.TrimEnd('/');

            return key;
        }

        private static void SetErrorResponse(AuthorizationFilterContext context, Result<bool> result)
        {
            var statusCode = result.Code switch
            {
                401 => StatusCodes.Status401Unauthorized,
                403 => StatusCodes.Status403Forbidden,
                400 => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            context.Result = new JsonResult(result) { StatusCode = statusCode };
        }
    }
}
