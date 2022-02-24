using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.AspNet;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MessageHandlers.Middleware;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Sample.MP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//使用本地缓存必须添加
builder.Services.AddMemoryCache();

//Senparc.Weixin 注册（必须）
builder.Services.AddSenparcWeixinServices(builder.Configuration);

var app = builder.Build();


var senparcWeixinSetting = app.Services.GetService<Microsoft.Extensions.Options.IOptions<Senparc.Weixin.Entities.SenparcWeixinSetting>>()!.Value;

//启用微信配置（必须）
var registerService = app.UseSenparcWeixin(app.Environment, null, null, register => { }, (register, weixinSetting) =>
{
    //注册公众号信息（可以执行多次注册多个公众号）
    register.RegisterMpAccount(weixinSetting, "【盛派网络小助手】公众号");
});

#region 使用 MessageHadler 中间件，用于取代创建独立的 Controller
//MessageHandler 中间件介绍：https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html

//使用公众号的 MessageHandler 中间件（不再需要创建 Controller）                       --DPBMARK MP
app.UseMessageHandlerForMp("/WeixinAsync", CustomMessageHandler.GenerateMessageHandler, options =>
{
    /* 说明：
     * 1、此代码块中演示了较为全面的功能点，简化的使用可以参考下面小程序和企业微信
     * 2、使用中间件也支持多账号，可以使用 URL 添加参数的方式（如：/Weixin?id=1），
     *    在options.AccountSettingFunc = context => {...} 中，从 context.Request 中获取 [id] 值，
     *    并反馈对应的 senparcWeixinSetting 信息
     */

    #region 配置 SenparcWeixinSetting 参数，以自动提供 Token、EncodingAESKey 等参数

    //此处为委托，可以根据条件动态判断输入条件（必须）
    options.AccountSettingFunc = context =>
        //方法一：使用默认配置
        Senparc.Weixin.Config.SenparcWeixinSetting;

    //方法二：使用指定配置：
    //Config.SenparcWeixinSetting["<Your SenparcWeixinSetting's name filled with Token, AppId and EncodingAESKey>"]; 

    //方法三：结合 context 参数动态判断返回Setting值

    #endregion

    //对 MessageHandler 内异步方法未提供重写时，调用同步方法（按需）
    options.DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod;

    options.EnableRequestLog = true;//默认就为 true，如需关闭日志，可设置为 false
    options.EnbleResponseLog = true;//默认就为 true，如需关闭日志，可设置为 false

    //对发生异常进行处理（可选）
    options.AggregateExceptionCatch = ex =>
    {
        //逻辑处理...
        return false;//系统层面抛出异常
    };
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
