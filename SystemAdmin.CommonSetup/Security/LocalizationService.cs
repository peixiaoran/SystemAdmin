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
        /// 获取多语言文案（支持 {0} 格式化）
        /// 用法：_msg.ReturnMsg($"{_this}InsertFailed", arg1, arg2...)
        /// </summary>
        public string ReturnMsg(string key, params object?[] args)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            var culture = GetCultureFromHeader();

            // 1. 按请求语言读取
            var value = _resourceManager.GetString(key, culture);

            // 2. zh-cn 兜底
            if (string.IsNullOrEmpty(value))
                value = _resourceManager.GetString(key, new CultureInfo("zh-cn"));

            // 3. en-us 兜底
            if (string.IsNullOrEmpty(value))
                value = _resourceManager.GetString(key, new CultureInfo("en-us"));

            // 4. 全部没有 → 返回 key 本身
            if (string.IsNullOrEmpty(value))
                return key;

            // 5. 格式化占位符 {0}，确保格式化安全不抛异常
            try
            {
                return (args is { Length: > 0 })
                    ? string.Format(value, args)
                    : value;
            }
            catch
            {
                // 如果格式化失败，返回原文案，避免 FormatException 影响业务
                return value;
            }
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
                return new CultureInfo("zh-cn");
            }

            // Accept-Language 可能类似： "zh-cn,zh;q=0.9,en-us;q=0.8,en;q=0.7"
            var first = langHeader.Split(',')[0].Trim();

            try
            {
                return new CultureInfo(first);
            }
            catch
            {
                // 解析失败给一个默认
                return new CultureInfo("zh-cn");
            }
        }
    }
}
