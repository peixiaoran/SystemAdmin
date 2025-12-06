using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.CommonSetup.Options
{
    public class JwtSettings
    {
        /// <summary>签发方 Issuer，例如：your-system</summary>
        public string? Issuer { get; set; }

        /// <summary>受众 Audience，例如：your-client</summary>
        public string? Audience { get; set; }

        /// <summary>算法，目前只支持 ES256</summary>
        public string? Algorithm { get; set; }

        /// <summary>过期时间（分钟）</summary>
        public int ExpiresInMinutes { get; set; }

        /// <summary>时间偏移容忍（秒）</summary>
        public int ClockSkewSeconds { get; set; }

        /// <summary>Key Id（kid），用于区分密钥版本</summary>
        public string? KeyId { get; set; }

        /// <summary>公钥 PEM（可为空，空则用私钥导出）</summary>
        public string? PublicKey { get; set; }

        /// <summary>私钥 PEM</summary>
        public string? PrivateKey { get; set; }
    }
}
