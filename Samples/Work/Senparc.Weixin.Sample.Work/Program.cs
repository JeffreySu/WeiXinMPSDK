using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Entities;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//使用本地缓存必须添加
builder.Services.AddMemoryCache();

#region 添加微信配置（一行代码）

//Senparc.Weixin 注册（必须）
builder.Services.AddSenparcWeixin(builder.Configuration);

#endregion


var app = builder.Build();

#region 启用微信配置（一句代码）

//启用微信配置（必须）
var registerService = app.UseSenparcWeixin(app.Environment, null, null, 
    register => { },
    (register, weixinSetting) =>
{
    //注册企业微信（可以执行多次，注册多个账号）
    register.RegisterWorkAccount(weixinSetting, "【盛派网络】企业微信");
});

#region 使用 MessageHadler 中间件，用于取代创建独立的 Controller

//MessageHandler 中间件介绍（参考公众号）：https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html

//使用企业微信的 MessageHandler 中间件（不再需要创建 Controller）                       --DPBMARK Work
app.UseMessageHandlerForWork("/WorkAsync", WorkCustomMessageHandler.GenerateMessageHandler, options =>
{
    //获取默认微信配置
    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting;

    //[必须] 设置微信配置
    options.AccountSettingFunc = context => weixinSetting;

    //[可选] 设置最大文本长度回复限制（超长后会调用客服接口分批次回复）
    var appKey = AccessTokenContainer.BuildingKey(weixinSetting.WorkSetting);
    options.TextResponseLimitOptions = new TextResponseLimitOptions(2048, appKey);
});

//使用企业微信机器人 MessageHandler 中间件（不再需要创建 Controller）           --DPBMARK WorkBot
app.UseMessageHandlerForWork("/WorkBotAsync", WorkBotCustomMessageHandler.GenerateMessageHandler, options =>
{
    // 为机器人通道使用“企业微信机器人”这组配置（若未设置则回退到默认 WorkSetting）
    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting;
    var botSetting = Senparc.Weixin.Config.SenparcWeixinSetting["企业微信机器人"] as ISenparcWeixinSettingForWork;

    // 仍然返回全局配置集合，供中间件识别；
    // Bot 路由下的文本长度限制，基于机器人所用的 CorpId/Secret 计算 AppKey
    options.AccountSettingFunc = context => weixinSetting;

    var appKey = botSetting != null
        ? AccessTokenContainer.BuildingKey(botSetting)
        : AccessTokenContainer.BuildingKey(weixinSetting.WorkSetting);
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

#region 此部分代码为 Sample 共享文件需要而添加，实际项目无需添加
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
