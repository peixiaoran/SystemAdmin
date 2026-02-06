using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class JwtSetupExtensions
    {
        /// <summary>
        /// Jwt 全量依赖注入：
        /// - 绑定 JwtSettings
        /// - 注册 JwtTokenService（Singleton）
        /// - 注册 CurrentUser（Scoped）
        /// - 配置 JwtBearer 验证（支持 Header + Cookie）
        /// </summary>
        public static IServiceCollection AddJwtSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. 绑定配置到 settings（临时对象）
            var settings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(settings);

            // 修正 JSON 里的 \n
            settings.PublicKey = (settings.PublicKey ?? string.Empty)
                .Trim();

            settings.PrivateKey = (settings.PrivateKey ?? string.Empty)
                .Trim();

            if (string.IsNullOrEmpty(settings.PrivateKey))
                throw new InvalidOperationException("JwtSettings.PrivateKey 不能为空。");

            if (string.IsNullOrEmpty(settings.PublicKey))
                throw new InvalidOperationException("JwtSettings.PublicKey 不能为空。");

            // 2. 把“修正后的 settings”写回 Options，供 JwtTokenService / 其它地方使用
            services.Configure<JwtSettings>(_ =>
            {
                _.Algorithm = settings.Algorithm;
                _.Issuer = settings.Issuer;
                _.Audience = settings.Audience;
                _.ExpiresInMinutes = settings.ExpiresInMinutes;
                _.ClockSkewSeconds = settings.ClockSkewSeconds;
                _.KeyId = settings.KeyId;
                _.PublicKey = settings.PublicKey;
                _.PrivateKey = settings.PrivateKey;

                // 如果你在 JwtSettings 里加了 Cookie 相关字段，就同步一下
                _.CookieName = settings.CookieName;
                _.CookieSecure = settings.CookieSecure;
                _.CookieSameSite = settings.CookieSameSite;
            });

            // 3. 注册 JwtTokenService（生成 Token & 写 Cookie，用的是私钥） → Singleton
            services.AddSingleton<JwtTokenService>();

            // 4. 注册 CurrentUser（基于 HttpContext.User 的当前用户访问器） → Scoped
            services.AddScoped<CurrentUser>();

            // 5. 构造验证用 ECDSA 公钥（JwtBearer 用）
            var ecdsa = ECDsa.Create();
            try
            {
                ecdsa.ImportFromPem(settings.PublicKey);
            }
            catch (Exception ex)
            {
                throw new Exception("JWT 公钥 PEM 格式错误，请检查 JwtSettings.PublicKey（必须包含 BEGIN/END 行）。", ex);
            }

            var securityKey = new ECDsaSecurityKey(ecdsa)
            {
                KeyId = settings.KeyId
            };

            // 6. 配置 Authentication + JwtBearer（支持 Header 和 HttpOnly Cookie）
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
                        ValidAlgorithms = new[] { SecurityAlgorithms.EcdsaSha256 }
                    };

                    // ⭐ 关键：从 Header 或 Cookie 读取 Token
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            string? token = null;

                            // 1) 先看 Authorization: Bearer xxx
                            var authHeader = context.Request.Headers["Authorization"].ToString();
                            if (!string.IsNullOrWhiteSpace(authHeader) &&
                                authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            {
                                token = authHeader["Bearer ".Length..].Trim();
                            }

                            // 2) 没有再从 Cookie 获取
                            if (string.IsNullOrEmpty(token))
                            {
                                var cookieName = string.IsNullOrWhiteSpace(settings.CookieName)
                                    ? "AccessToken"
                                    : settings.CookieName;

                                if (context.Request.Cookies.TryGetValue(cookieName, out var cookieToken) &&
                                    !string.IsNullOrWhiteSpace(cookieToken))
                                {
                                    token = cookieToken;
                                }
                            }

                            context.Token = token;
                            return Task.CompletedTask;
                        }
                    };
                });

            // 7. Authorization
            services.AddAuthorization();

            return services;
        }
    }
}
