using Microsoft.AspNetCore.Hosting;
using Scalar.AspNetCore;
using SystemAdmin.CommonSetup.DependencyInjection;
using SystemAdmin.Hosting.DependencyInjection;
using SystemAdmin.WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// 注入 Serilog
builder.Host.AddSerilogSetup();

// 添加控制器
builder.Services.AddControllers();

// 注入 HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// 注入 Cors
builder.Services.AddCorsSetup(builder.Configuration);

// 注入 OpenApi
builder.Services.AddOpenApi();

// 注入 OpenApi规范
builder.Services.AddCustomOpenApiAction();

// 注入 Jwt
builder.Services.AddJwtSetup(builder.Configuration);

// 注入 Mapster
builder.Services.AddMapsterSetup();

// 注入 Minio
builder.Services.AddMinioSetup(builder.Configuration);

// 注入 MailKit
builder.Services.AddMailKitSetup(builder.Configuration);

// 注入 获取语言服务
builder.Services.AddLocalizationSetup();

// 注入 全局语言中间件
builder.Services.AddLanguage();

// 注入 SqlSugar
builder.Services.SqlSugarScopeSetup(builder.Configuration);

// 注入 业务服务和仓储
builder.Services.AddServiceAndRepository();

// 注入 AddHybridCache缓存服务
builder.Services.AddCache();

// 配置 Kestrel
builder.WebHost.ConfigureKestrel((context, options) =>
{
    context.Configuration.GetSection("Kestrel").Bind(options);
});

var app = builder.Build();

app.MapScalarApiReference("/systemadmin");
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCorsSetup(builder.Configuration);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
