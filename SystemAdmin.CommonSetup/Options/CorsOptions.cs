namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// CORS（跨域资源共享）配置选项
    /// </summary>
    public sealed class CorsOptions
    {
        /// <summary>
        /// CORS 策略名称，用于 AddCors 与 UseCors。
        /// </summary>
        public string PolicyName { get; set; } = "DefaultCors";

        /// <summary>
        /// 允许跨域访问的前端 Origin 列表（需精确匹配协议/域名/端口）。
        /// AllowCredentials=true 时不能为空。
        /// </summary>
        public string[] Origins { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 允许的 HTTP 方法（需包含 OPTIONS 以支持预检请求）。
        /// </summary>
        public string[] Methods { get; set; } = { "POST", "OPTIONS" };

        /// <summary>
        /// 是否允许任意请求头（如 Authorization、Accept-Language 等）。
        /// </summary>
        public bool AllowAnyHeader { get; set; } = true;

        /// <summary>
        /// 是否允许携带凭据（Cookie / HttpOnly Cookie）。
        /// 启用时必须显式指定 Origins，不能使用通配符。
        /// </summary>
        public bool AllowCredentials { get; set; } = true;
    }
}
