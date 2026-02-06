using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using SystemAdmin.Hosting.DependencyInjection;

namespace SystemAdmin.WebApi.DependencyInjection
{
    public static class OpenApiExtensions
    {
        /// <summary>
        /// 注册自定义 OpenAPI 配置（基于 Microsoft.AspNetCore.OpenApi v10）。
        /// </summary>
        /// <param name="services">DI 容器。</param>
        /// <param name="documentName">
        /// OpenAPI 文档名称（对应 /openapi/{documentName}.json），默认 v1。
        /// </param>
        public static IServiceCollection AddCustomOpenApiAction(this IServiceCollection services, string documentName = "v1")
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddOpenApi(documentName, options =>
            {
                // 指定 OpenAPI 规范版本
                options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;

                // 文档元信息
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    ConfigureDocumentInfo(document, documentName);
                    return Task.CompletedTask;
                });

                // 注册自定义文档转换器（需实现 IOpenApiDocumentTransformer）
                options.AddDocumentTransformer<OpenApiTransformer>();
            });

            return services;
        }

        /// <summary>
        /// OpenAPI 文档元信息配置。
        /// </summary>
        private static void ConfigureDocumentInfo(OpenApiDocument document, string documentName)
        {
            document.Info ??= new OpenApiInfo();
            document.Info.Title = "SystemAdmin";
            document.Info.Version = documentName; // 与文档名一致，更直观
            document.Info.Description = "SystemAdmin API Documentation";
        }
    }
}
