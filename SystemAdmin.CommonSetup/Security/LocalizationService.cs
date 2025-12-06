using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using SystemAdmin.Localization.Resources;

namespace SystemAdmin.CommonSetup.Security
{
    /// <summary>
    /// 多语言消息服务：
    /// - 通过请求头 Accept-Language 获取语言
    /// - 从 SystemAdmin.Localization.Resources.Messages 读取 resx 文本
    /// - 暴露 ReturnMsg($"{_this}InsertFailed") 用法
    /// </summary>
    public class LocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ResourceManager _resourceManager;

        public LocalizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            // 使用 resx 自动生成的 ResourceManager
            _resourceManager = Messages.ResourceManager;
        }

        /// <summary>
        /// 获取多语言文案：
        /// _msg.ReturnMsg($"{_this}InsertFailed")
        /// </summary>
        public string ReturnMsg(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            var culture = GetCultureFromHeader();

            // 1. 根据请求语言取
            var value = _resourceManager.GetString(key, culture);

            if (!string.IsNullOrEmpty(value))
                return value;

            // 2. 兜底用 zh-CN
            var zhCn = _resourceManager.GetString(key, new CultureInfo("zh-CN"));
            if (!string.IsNullOrEmpty(zhCn))
                return zhCn!;

            // 3. 再兜底用 en-US
            var enUs = _resourceManager.GetString(key, new CultureInfo("en-US"));
            if (!string.IsNullOrEmpty(enUs))
                return enUs!;

            // 4. 全部都没有就返回 key，方便发现漏配
            return key;
        }

        /// <summary>
        /// 从 Accept-Language 请求头转换为 CultureInfo
        /// </summary>
        private CultureInfo GetCultureFromHeader()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var langHeader = httpContext?.Request.Headers["Accept-Language"].ToString();

            if (string.IsNullOrWhiteSpace(langHeader))
            {
                // 默认中文
                return new CultureInfo("zh-CN");
            }

            // Accept-Language 可能类似： "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7"
            var first = langHeader.Split(',')[0].Trim();

            try
            {
                return new CultureInfo(first);
            }
            catch
            {
                // 解析失败给一个默认
                return new CultureInfo("zh-CN");
            }
        }
    }
}
