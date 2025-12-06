using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class JwtSetupExtensions
    {
        /// <summary>
        /// Jwt 全量依赖注入：
        /// - 构造 JwtSettings（环境变量优先，其次 appsettings）
        /// - 注册 JwtTokenService
        /// - 注册 CurrentUser
        /// - 注册 Authentication + JwtBearer + Authorization
        /// </summary>
        public static IServiceCollection AddJwtSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. 先从配置获取一份“配置值版本”的 settings
            var settings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(settings);

            // 2. 把最终 settings 注册到 Options，给 JwtTokenService 用
            services.Configure<JwtSettings>(_ =>
            {
                _.Issuer = settings.Issuer;
                _.Audience = settings.Audience;
                _.Algorithm = settings.Algorithm;
                _.ExpiresInMinutes = settings.ExpiresInMinutes;
                _.ClockSkewSeconds = settings.ClockSkewSeconds;
                _.KeyId = settings.KeyId;
                _.PublicKey = settings.PublicKey;
                _.PrivateKey = settings.PrivateKey;
            });

            // 3. 当前用户 + TokenService
            services.AddScoped<CurrentUser>();
            services.AddSingleton<JwtTokenService>();

            // 4. 构造验证用公钥
            var ecdsa = ECDsa.Create();
            if (!string.IsNullOrWhiteSpace(settings.PublicKey))
            {
                ecdsa.ImportFromPem(settings.PublicKey.AsSpan());
            }

            var securityKey = new ECDsaSecurityKey(ecdsa)
            {
                KeyId = settings.KeyId
            };

            // 5. Authentication + JwtBearer
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
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
                });

            services.AddAuthorization();

            return services;
        }
    }
}
