using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class ServiceRepositorySetupExtensions
    {
        /// <summary>
        /// 自动注册所有 Service 与 Repository
        /// </summary>
        public static IServiceCollection AddServiceAndRepository(this IServiceCollection services)
        {
            // 你的项目的程序集名称前缀
            const string serviceAssembly = "SystemAdmin.Service";
            const string repoAssembly = "SystemAdmin.Repository";

            RegisterByConvention(services, serviceAssembly);
            RegisterByConvention(services, repoAssembly);

            return services;
        }

        private static void RegisterByConvention(IServiceCollection services, string assemblyName)
        {
            var asm = Assembly.Load(assemblyName);

            var types = asm.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in types)
            {
                // 找接口
                var iface = type.GetInterfaces().FirstOrDefault();

                if (iface != null)
                {
                    // 默认注入 Scoped
                    services.AddScoped(iface, type);
                }
                else
                {
                    // 无接口的类也注入，会用于简单场景
                    services.AddScoped(type);
                }
            }
        }
    }
}
