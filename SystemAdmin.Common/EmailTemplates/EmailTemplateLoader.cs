using System.Collections.Concurrent;
using System.Reflection;

namespace SystemAdmin.Common.EmailTemplates
{
    public static class EmailTemplateLoader
    {
        private static readonly Assembly _assembly = typeof(EmailTemplateLoader).Assembly;
        private static readonly ConcurrentDictionary<string, string> _cache = new();

        /// <summary>
        /// 读取审批通知邮件模板
        /// </summary>
        public static string GetReviewNotice() => Load("ReviewNotice.html");

        private static string Load(string fileName)
        {
            return _cache.GetOrAdd(fileName, name =>
            {
                var resourceName = $"SystemAdmin.Common.EmailTemplates.{name}";

                using var stream = _assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                {
                    // 把所有实际的资源名印出来
                    var allNames = string.Join("\n", _assembly.GetManifestResourceNames());
                    throw new InvalidOperationException(
                        $"找不到嵌入资源：{resourceName}\n实际存在的资源：\n{allNames}");
                }

                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            });
        }
    }
}
