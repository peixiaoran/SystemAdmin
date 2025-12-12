using Microsoft.AspNetCore.Http;

public class JwtSettings
{
    /// <summary>
    /// JWT 签名算法。
    /// 常见值：
    /// - HS256：对称密钥（简单但密钥泄露风险高）
    /// - RS256 / ES256：非对称密钥（推荐，生产环境首选）
    /// 当前使用 ES256（ECDSA），安全性高、性能好。
    /// </summary>
    public string Algorithm { get; set; } = "ES256";

    /// <summary>
    /// Token 的签发者（Issuer）。
    /// 用于校验 Token 是否由“可信系统”签发。
    /// 示例：SystemsAdmin
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Token 的接收方（Audience）。
    /// 表示 Token 允许被哪些系统使用。
    /// 示例：SystemsAdmin.WebApi
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Token 有效期（分钟）。
    /// 超过该时间即失效，需要重新登录或刷新。
    /// 常见设置：60 / 120
    /// </summary>
    public int ExpiresInMinutes { get; set; } = 120;

    /// <summary>
    /// 时间偏移容忍值（秒）。
    /// 用于解决服务器之间时间不完全一致的问题。
    /// 例如：Token 刚生成就被认为过期。
    /// </summary>
    public int ClockSkewSeconds { get; set; } = 10;

    /// <summary>
    /// 密钥 ID（KeyId / kid）。
    /// 用于支持密钥轮换（Key Rotation）。
    /// 当存在多把公钥时，通过 kid 快速定位正确的那一把。
    /// </summary>
    public string KeyId { get; set; } = string.Empty;

    /// <summary>
    /// JWT 验签用的公钥（PEM 格式）。
    /// 后端在校验 Token 时使用该公钥。
    /// 公钥可以安全暴露，常用于分布式或多服务校验。
    /// </summary>
    public string PublicKey { get; set; } = string.Empty;

    /// <summary>
    /// JWT 签名用的私钥（PEM 格式）。
    /// 只用于签发 Token，必须严格保密。
    /// 建议仅存在于安全配置或环境变量中。
    /// </summary>
    public string PrivateKey { get; set; } = string.Empty;


    // ===================== Cookie 相关配置 =====================

    /// <summary>
    /// 存储 JWT 的 Cookie 名称。
    /// 示例：AccessToken
    /// 前后端需保持一致。
    /// </summary>
    public string? CookieName { get; set; } = "AccessToken";

    /// <summary>
    /// 是否仅允许在 HTTPS 下发送 Cookie。
    /// true：生产环境强烈推荐
    /// false：仅限本地开发（HTTP）
    /// </summary>
    public bool CookieSecure { get; set; } = true;

    /// <summary>
    /// Cookie 的 SameSite 策略。
    /// - None：允许跨站点发送（前后端分离必须）
    /// - Lax：同站点优先，部分跨站允许
    /// - Strict：完全禁止跨站
    /// 前后端分离 + Cookie 登录，必须使用 None。
    /// </summary>
    public SameSiteMode CookieSameSite { get; set; } = SameSiteMode.None;
}
