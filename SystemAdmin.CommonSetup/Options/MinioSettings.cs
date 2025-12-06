using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.CommonSetup.Options
{
    public class MinioSettings
    {
        /// <summary> Minio 服务地址，例如：127.0.0.1:9000 或 play.min.io </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary> 访问密钥 AccessKey </summary>
        public string AccessKey { get; set; } = string.Empty;

        /// <summary> 访问密钥 SecretKey </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary> 默认 Bucket 名称 </summary>
        public string DefaultBucket { get; set; } = "SystemAdmin";

        /// <summary> 是否使用 SSL（https） </summary>
        public bool UseSSL { get; set; } = false;
    }
}
