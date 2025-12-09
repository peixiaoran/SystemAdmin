using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.Security
{
    /// <summary>
    /// Jwt Token 生成 & 校验服务（ES256 + HttpOnly Cookie 支持）
    /// </summary>
    public sealed class JwtTokenService : IDisposable
    {
        private const string DefaultCookieName = "AccessToken";

        private readonly JwtSettings _settings;
        private readonly JwtSecurityTokenHandler _handler = new();
        private readonly ECDsa _ecdsaPrivate;
        private readonly ECDsa _ecdsaPublic;
        private readonly TokenValidationParameters _validationParameters;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (!string.Equals(_settings.Algorithm, "ES256", StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException("当前 JwtTokenService 仅支持 ES256 算法。");

            // 私钥（必须有）
            _ecdsaPrivate = CreateEcdsaFromPem(_settings.PrivateKey, isPrivateKey: true);

            // 公钥（可选）为空则用私钥导出公钥
            _ecdsaPublic = string.IsNullOrWhiteSpace(_settings.PublicKey)
                ? CreateEcdsaFromPem(_settings.PrivateKey, isPrivateKey: true)
                : CreateEcdsaFromPem(_settings.PublicKey, isPrivateKey: false);

            var securityKey = new ECDsaSecurityKey(_ecdsaPublic)
            {
                KeyId = _settings.KeyId
            };

            _validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(_settings.ClockSkewSeconds),
                ValidAlgorithms = new[] { SecurityAlgorithms.EcdsaSha256 }
            };
        }

        /// <summary>
        /// 登录成功时调用：生成 Token 并写入 HttpOnly Cookie。
        /// 前端不需要拿到 Token 字符串。
        /// </summary>
        public void SetAuthCookie(HttpResponse response, long userId, string userNo)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            var token = GenerateTokenInternal(userId, userNo);

            var cookieName = string.IsNullOrWhiteSpace(_settings.CookieName)
                ? DefaultCookieName
                : _settings.CookieName;

            var expires = DateTimeOffset.UtcNow.AddMinutes(_settings.ExpiresInMinutes);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = _settings.CookieSecure,           // 建议生产环境 true
                SameSite = _settings.CookieSameSite,      // 可以在 JwtSettings 里配置，默认 Lax
                Expires = expires
            };

            response.Cookies.Append(cookieName, token, cookieOptions);
        }

        /// <summary>
        /// 可选：需要时仍然可以拿到 Token 字符串（比如给第三方系统用）。
        /// 一般业务登录不需要调用这个。
        /// </summary>
        public string GenerateTokenString(long userId, string userNo)
        {
            return GenerateTokenInternal(userId, userNo);
        }

        /// <summary>
        /// 手动验证 Token（不通过 ASP.NET Core 中间件时可以使用）
        /// </summary>
        public ClaimsPrincipal? ValidateToken(string token, bool validateLifetime = true)
        {
            var p = _validationParameters.Clone();
            p.ValidateLifetime = validateLifetime;

            try
            {
                var principal = _handler.ValidateToken(token, p, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 具体生成 Token 的内部实现。
        /// </summary>
        private string GenerateTokenInternal(long userId, string userNo)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new("uid", userId.ToString()),
                new("uno", userNo ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(
                    JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                )
            };

            var signingKey = new ECDsaSecurityKey(_ecdsaPrivate)
            {
                KeyId = _settings.KeyId
            };

            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.EcdsaSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.ExpiresInMinutes),
                signingCredentials: credentials);

            return _handler.WriteToken(token);
        }

        /// <summary>
        /// 从 PEM 字符串创建 ECDSA（同时兼容 JSON 中的 \n）
        /// </summary>
        private static ECDsa CreateEcdsaFromPem(string? pem, bool isPrivateKey)
        {
            if (string.IsNullOrWhiteSpace(pem))
                throw new InvalidOperationException("JwtSettings 中的公钥/私钥不能为空。");

            var fixedPem = pem.Replace("\\n", "\n").Trim();

            var ecdsa = ECDsa.Create();

            try
            {
                ecdsa.ImportFromPem(fixedPem);
            }
            catch (CryptographicException ex)
            {
                var type = isPrivateKey ? "PrivateKey" : "PublicKey";
                throw new CryptographicException($"导入 ECDSA {type} 失败，请检查 PEM 格式是否正确。", ex);
            }

            return ecdsa;
        }

        public void Dispose()
        {
            _ecdsaPrivate.Dispose();
            _ecdsaPublic.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
