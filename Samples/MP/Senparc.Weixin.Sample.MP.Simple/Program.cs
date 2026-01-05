using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.AspNet;
using Senparc.Weixin.MP.MessageHandlers.Middleware;
using Senparc.Weixin.Sample.MP;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
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
var registerService = app.UseSenparcWeixin(app.Environment,
        senparcSetting: null /* 传入 null 则覆盖 appsettings 中的 SenpacSetting 配置*/,
        senparcWeixinSetting: null /* 传入 null 则覆盖 appsettings 中的 SenpacWeixinSetting 配置*/,
        globalRegisterConfigure: register => { /* CO2NET 全局配置 */ },
        weixinRegisterConfigure: (register, weixinSetting) => {/* 注册公众号或开放平台账号信息（可以执行多次，注册多个公众号）*/},
        autoRegisterAllPlatforms: true /* 自动注册所有平台 */
        );

#region 使用 MessageHandler 中间件直接接收消息，无需 Controller

// MessageHandler 中间件功能：https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html
// 使用公众号的 MessageHandler 中间件，不再需要编写 Controller
app.UseMessageHandlerForMp("/WeixinAsync", CustomMessageHandler.GenerateMessageHandler, options =>
{
    // 获取默认微信配置
    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting;

    // [必填] 指定微信配置
    options.AccountSettingFunc = context => weixinSetting;

    // [可选] 设置文本返回长度限制；如需超长消息可通过客服接口分段回复
    options.TextResponseLimitOptions = new TextResponseLimitOptions(2048, weixinSetting.WeixinAppId);
});

#endregion

#endregion

#region 逻辑接口调用示例

app.MapGroup("/").MapGet("/TryApi", async () =>
{
    // 展示如何获取已关注用户的 OpenId（仅获取的第一个）

    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting.MpSetting;
    var users = await Senparc.Weixin.MP.AdvancedAPIs.UserApi.GetAsync(weixinSetting.WeixinAppId, null);

    Console.WriteLine($"展示前 {users.count} 个 OpenId");

    return users.data.openid;
});

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
