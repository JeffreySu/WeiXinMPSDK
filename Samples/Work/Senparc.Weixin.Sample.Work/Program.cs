using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.AspNet;
using Senparc.Weixin.Work.MessageHandlers.Middleware;
using Senparc.Weixin.Sample.Work.MessageHandlers;
using Senparc.Weixin.Work.Containers;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 使用内存缓存必须添加
builder.Services.AddMemoryCache();

#region 添加微信配置（一行代码）

// Senparc.Weixin 注册（必须）
builder.Services.AddSenparcWeixin(builder.Configuration);

#endregion


var app = builder.Build();

#region 启用微信配置（一句代码）

// 启用微信配置（必须）
var registerService = app.UseSenparcWeixin(app.Environment, null, null, 
    register => { },
    (register, weixinSetting) =>
{
    // 注册企业微信账号信息（可以执行多次，注册多个账号）
    register.RegisterWorkAccount(weixinSetting, "【盛派网络】企业微信");
});

#region 使用 MessageHandler 中间件直接接收消息，无需 Controller

// MessageHandler 中间件功能（参考公众号）：https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html

// 使用企业微信的 MessageHandler 中间件，不再需要编写 Controller                       --DPBMARK Work
app.UseMessageHandlerForWork("/WorkAsync", WorkCustomMessageHandler.GenerateMessageHandler, options =>
{
    // 获取默认微信配置
    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting;

    // [必填] 指定微信配置
    options.AccountSettingFunc = context => weixinSetting;

    // [可选] 设置文本返回长度限制；如需超长消息可通过客服接口分段回复
    var appKey = AccessTokenContainer.BuildingKey(weixinSetting.WorkSetting);
    options.TextResponseLimitOptions = new TextResponseLimitOptions(2048, appKey);
});
#endregion

#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

#region 此段代码仅为 Sample 示例需要，实际项目可按需处理
#if DEBUG
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new ManifestEmbeddedFileProvider(Assembly.GetExecutingAssembly(), "wwwroot"),
//    RequestPath = new PathString("")
//});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"..", "..", "Shared", "Senparc.Weixin.Sample.Shared", "wwwroot")),
    RequestPath = new PathString("")
});
#endif
#endregion

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
