using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class JwtExtensions
    {
        /// <summary>
        /// JWT 注入：绑定配置、注册 Token 服务与当前用户、配置 JwtBearer（支持 Header + Cookie）
        /// </summary>
        public static IServiceCollection AddJwtSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // 绑定配置并清理 PEM 中可能的换行
            var settings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
            settings.PublicKey = (settings.PublicKey ?? string.Empty).Trim();
            settings.PrivateKey = (settings.PrivateKey ?? string.Empty).Trim();

            // 写回 Options 供其它服务使用
            services.Configure<JwtSettings>(opts =>
            {
                opts.Algorithm = settings.Algorithm;
                opts.Issuer = settings.Issuer;
                opts.Audience = settings.Audience;
                opts.ExpiresInMinutes = settings.ExpiresInMinutes;
                opts.ClockSkewSeconds = settings.ClockSkewSeconds;
                opts.KeyId = settings.KeyId;
                opts.PublicKey = settings.PublicKey;
                opts.PrivateKey = settings.PrivateKey;
                opts.CookieName = settings.CookieName;
                opts.CookieSecure = settings.CookieSecure;
                opts.CookieSameSite = settings.CookieSameSite;
            });

            services.AddSingleton<JwtTokenService>();
            services.AddScoped<CurrentUser>();

            // 构造验证用公钥
            var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(settings.PublicKey);
            var securityKey = new ECDsaSecurityKey(ecdsa) { KeyId = settings.KeyId };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Audience = settings.Audience;
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = securityKey,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(settings.ClockSkewSeconds),
                        ValidAlgorithms = new[] { SecurityAlgorithms.EcdsaSha256 },
                    };

                    // 优先从 Authorization Header 读取，其次从 Cookie
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = ExtractToken(context.Request, settings.CookieName);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
            return services;
        }

        private static string? ExtractToken(HttpRequest request, string? cookieName)
        {
            var authHeader = request.Headers.Authorization.ToString();
            if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader["Bearer ".Length..].Trim();
                if (!string.IsNullOrWhiteSpace(token)) return token;
            }

            var name = string.IsNullOrWhiteSpace(cookieName) ? "AccessToken" : cookieName;
            return request.Cookies.TryGetValue(name, out var cookieToken) && !string.IsNullOrWhiteSpace(cookieToken)
                ? cookieToken
                : null;
        }
    }
}
