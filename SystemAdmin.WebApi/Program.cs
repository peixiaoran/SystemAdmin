using Scalar.AspNetCore;
using SystemAdmin.CommonSetup.DependencyInjection;
using SystemAdmin.Hosting.DependencyInjection;
using SystemAdmin.WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// 配置Serilog日志
builder.Host.AddSerilogSetup();

builder.Services.AddControllers();

// 注入 HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// 注入Cors服务
builder.Services.AddCorsSetup();

// 注入OpenApi服务
builder.Services.AddOpenApi();

// 注入 OpenApi 自定义配置
builder.Services.AddCustomOpenApiAction();

// 注入Jwt服务
builder.Services.AddJwtSetup(builder.Configuration);

// 注入Localization服务
builder.Services.AddLocalizationSetup();

// 注入Mapster服务
builder.Services.AddMapsterSetup();

// 注入Minio服务
builder.Services.AddMinioSetup(builder.Configuration);

// 注入邮件服务
builder.Services.AddMailKitSetup(builder.Configuration);

// 注入请求语言处理中间件
builder.Services.AddLanguage();

// 注入SqlSugarScope服务
builder.Services.SqlSugarScopeSetup(builder.Configuration);

// 注入自定义的服务和仓储
builder.Services.AddServiceAndRepository();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference("/systemadmin");
    app.MapOpenApi();
}

app.UseCorsSetup();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
