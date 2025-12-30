namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// Minio 配置选项
    /// </summary>
    public class MinioSettings
    {
        /// <summary>
        /// Minio 服务地址
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// 访问密钥 AccessKey
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;

        /// <summary> 
        /// 访问密钥 SecretKey 
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary> 
        /// 默认 Bucket 名称 
        /// </summary>
        public string DefaultBucket { get; set; } = "systemadmin";

        /// <summary> 
        /// 是否使用 SSL（https） 
        /// </summary>
        public bool UseSSL { get; set; } = false;
    }
}
