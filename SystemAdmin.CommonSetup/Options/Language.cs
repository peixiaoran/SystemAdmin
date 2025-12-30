namespace SystemAdmin.CommonSetup.Options
{
    /// <summary>
    /// 语言配置类
    /// </summary>
    public class Language
    {
        /// <summary>
        /// 当前请求的 UI 语言，例如 "zh-CN"
        /// </summary>
        public string Locale { get; }

        /// <summary>
        /// 是否中文（包含 zh-CN等）
        /// </summary>
        public bool IsChinese { get; }

        /// <summary>
        /// 是否英文（包含 en-US等）
        /// </summary>
        public bool IsEnglish { get; }

        public Language(string uiLanguage)
        {
            Locale = uiLanguage;
            IsChinese = uiLanguage.StartsWith("zh", System.StringComparison.OrdinalIgnoreCase);
            IsEnglish = uiLanguage.StartsWith("en", System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
