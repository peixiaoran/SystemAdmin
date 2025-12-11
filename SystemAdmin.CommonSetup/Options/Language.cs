using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.CommonSetup.Options
{
    public class Language
    {
        /// <summary>
        /// 当前请求的 UI 语言，例如 "zh-cn"
        /// </summary>
        public string Locale { get; }

        /// <summary>
        /// 是否中文（包含 zh-cn, zh-TW 等）
        /// </summary>
        public bool IsChinese { get; }

        /// <summary>
        /// 是否英文（包含 en-us, en-GB 等）
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
