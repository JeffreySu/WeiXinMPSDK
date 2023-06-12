var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

#region 添加微信配置（一行代码）

//Senparc.Weixin 注册（必须）
builder.Services.AddSenparcWeixinServices(builder.Configuration);

#endregion

var app = builder.Build();

#region 启用微信配置(一句代码）

//启用微信配置（必须）
var registerService = app.UseSenparcWeixin(app.Environment,
    null /* 不为 null 则覆盖 appsettings  中的 SenpacSetting 配置*/,
    null /* 不为 null 则覆盖 appsettings  中的 SenpacWeixinSetting 配置*/,
    register => { /* CO2NET 全局配置 */ },
    (register, weixinSetting) =>
{
    //注册公众号信息（可以执行多次，注册多个公众号）
    register.RegisterMpAccount(weixinSetting, "【盛派网络小助手】公众号");
    //注册微信支付（可以执行多次，注册多个微信支付）
    register.RegisterTenpayApiV3(weixinSetting, "【盛派网络小助手】微信支付（ApiV3）");
});

#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();

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
