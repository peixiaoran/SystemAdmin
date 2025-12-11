using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.CommonSetup.Security
{
    /// <summary>
    /// 从 HttpContext 中读取 Accept-Language，并生成 RequestLanguage。
    /// </summary>
    public class LanguageAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguageAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Language GetRequestLanguage()
        {
            var header = _httpContextAccessor.HttpContext?
                .Request.Headers["Accept-Language"].ToString();

            // 格式： "zh-cn,zh;q=0.9,en-us;q=0.8"
            var ui = "zh-cn"; // 默认语言

            if (!string.IsNullOrWhiteSpace(header))
            {
                var first = header.Split(',')[0].Trim();
                if (!string.IsNullOrEmpty(first))
                    ui = first;
            }

            return new Language(ui);
        }
    }
}
