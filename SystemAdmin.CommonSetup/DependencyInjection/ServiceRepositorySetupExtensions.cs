using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SystemAdmin.CommonSetup.DependencyInjection
{
    public static class ServiceRepositorySetupExtensions
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
                            && (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")));

            foreach (var type in types)
            {
                services.AddScoped(type);
            }
        }
    }
}
