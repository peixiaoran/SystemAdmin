using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Text.Json.Nodes;

namespace SystemAdmin.Hosting.DependencyInjection
{
    public class OpenApiTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
    {
        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

            // 处理 JWT 认证方案
            if (authenticationSchemes.Any(authScheme => authScheme.Name == JwtBearerDefaults.AuthenticationScheme))
            {
                // 初始化 Components
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                // 定义 JWT Bearer 安全方案
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme."
                };

                // 注册到 Components
                const string schemeKey = "Bearer"; // key 用于引用
                document.Components.SecuritySchemes[schemeKey] = jwtSecurityScheme;

                // 初始化全局 SecurityRequirements
                document.Security ??= new List<OpenApiSecurityRequirement>();

                // 使用 OpenApiSecuritySchemeReference 添加全局安全要求
                document.Security.Add(
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecuritySchemeReference(schemeKey, document)
                        ] = new List<string>()
                    }
                );
            }

            // 创建 Accept-Language 参数
            var acceptLanguageParameter = new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "语言偏好设置：zh-cn（简体中文）、en-us（英文）",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String,

                    // 默认值
                    Default = "zh-cn",

                    // 枚举：zh-cn、en-us
                    Enum = new List<JsonNode>
                    {
                        JsonValue.Create("zh-cn")!,
                        JsonValue.Create("en-us")!
                    }
                }
            };

            // 将参数添加到每个 Operation
            foreach (var path in document.Paths.Values)
            {
                foreach (var operation in path.Operations?.Values ?? Enumerable.Empty<OpenApiOperation>())
                {
                    if (operation.Parameters == null)
                    {
                        operation.Parameters = new List<IOpenApiParameter>();
                    }
                    operation.Parameters.Add(acceptLanguageParameter);
                }
            }
        }
    }
}
