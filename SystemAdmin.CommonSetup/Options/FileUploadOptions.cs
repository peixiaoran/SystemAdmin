namespace SystemAdmin.CommonSetup.Options
{
    public class FileUploadOptions
    {
        /// <summary>
        /// 限制文件大小
        /// </summary>
        public int MaxSizeMB { get; set; }

        /// <summary>
        /// 限制允许的文件扩展名
        /// </summary>
        public string[] AllowExtensions { get; set; } = {
            ".xls", ".xlsx", ".csv",
            ".pdf", ".doc", ".docx", ".txt",
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp",
            ".zip", ".rar", ".7z"
        };
    }
}
