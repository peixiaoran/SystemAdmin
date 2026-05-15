using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class ServiceRepositoryExtensions
    {
        public static IServiceCollection AddProjectClasses(this IServiceCollection services)
        {
            RegisterAllClasses(services, "SystemAdmin.Service");
            RegisterAllClasses(services, "SystemAdmin.Repository");

            return services;
        }

        private static void RegisterAllClasses(IServiceCollection services, string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);

            var types = assembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && !t.IsGenericTypeDefinition
                            && t.IsPublic                                    // 只注册 public 类
                            && !t.Name.StartsWith("<")                       // 排除编译器生成的 <XXX> 类型
                            && !Attribute.IsDefined(t, typeof(CompilerGeneratedAttribute))); // 双保险

            foreach (var type in types)
            {
                services.AddScoped(type);
            }
        }
    }
}
