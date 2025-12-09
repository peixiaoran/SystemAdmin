using Microsoft.AspNetCore.Http;

public class JwtSettings
{
    public string Algorithm { get; set; } = "ES256";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiresInMinutes { get; set; } = 120;
    public int ClockSkewSeconds { get; set; } = 10;
    public string KeyId { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;

    // 新增：
    public string? CookieName { get; set; } = "AccessToken";
    public bool CookieSecure { get; set; } = true;
    public SameSiteMode CookieSameSite { get; set; } = SameSiteMode.None;
}
