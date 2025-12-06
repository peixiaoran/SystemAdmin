using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.Security
{
    public sealed class JwtTokenService : IDisposable
    {
        private readonly JwtSettings _settings;
        private readonly JwtSecurityTokenHandler _handler = new();
        private readonly ECDsa _ecdsaPrivate;
        private readonly ECDsa _ecdsaPublic;
        private readonly TokenValidationParameters _validationParameters;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (!string.Equals(_settings.Algorithm, "ES256", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException("当前实现仅支持 ES256 算法。");
            }

            // 私钥必需
            _ecdsaPrivate = CreateEcdsaFromPem(_settings.PrivateKey);

            // 公钥可选，空则用私钥导出
            _ecdsaPublic = string.IsNullOrWhiteSpace(_settings.PublicKey)
                ? CreateEcdsaFromPem(_settings.PrivateKey)
                : CreateEcdsaFromPem(_settings.PublicKey);

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
        /// 生成访问 Token，包含 uid（long） 和 uno（string）
        /// </summary>
        public string GenerateToken(long userId, string userNo)
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

        private static ECDsa CreateEcdsaFromPem(string? pem)
        {
            if (string.IsNullOrWhiteSpace(pem))
                throw new InvalidOperationException("JwtSettings 中的公钥/私钥不能为空，请通过环境变量或配置正确设置。");

            var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(pem.AsSpan());
            return ecdsa;
        }

        public void Dispose()
        {
            _ecdsaPrivate?.Dispose();
            _ecdsaPublic?.Dispose();
        }
    }
}