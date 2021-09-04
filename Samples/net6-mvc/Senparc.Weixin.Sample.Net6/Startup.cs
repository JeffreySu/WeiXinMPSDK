using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.AspNet;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Memcached;//DPBMARK Memcached DPBMARK_END
using Senparc.CO2NET.Utilities;
using Senparc.CO2NET.WebApi.WebApiEngines;
using Senparc.NeuChar.MessageHandlers;
using Senparc.WebSocket;//DPBMARK WebSocket DPBMARK_END
using Senparc.Weixin.Cache.CsRedis;//DPBMARK Redis DPBMARK_END
using Senparc.Weixin.Cache.Memcached;//DPBMARK Memcached DPBMARK_END
using Senparc.Weixin.Cache.Redis;//DPBMARK Redis DPBMARK_END
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;//DPBMARK MP DPBMARK_END
using Senparc.Weixin.MP.MessageHandlers.Middleware;//DPBMARK MP DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;//DPBMARK MP DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.WebSocket;//DPBMARK WebSocket DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.WorkMessageHandlers;//DPBMARK Work DPBMARK_END
using Senparc.Weixin.MP.Sample.CommonService.WxOpenMessageHandler;//DPBMARK MiniProgram DPBMARK_END
using Senparc.Weixin.Open;//DPBMARK Open DPBMARK_END
using Senparc.Weixin.Open.ComponentAPIs;//DPBMARK Open DPBMARK_END
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Sample.NetCore3.WebSocket.Hubs;//DPBMARK WebSocket DPBMARK_END
using Senparc.Weixin.TenPay;//DPBMARK TenPay DPBMARK_END
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.Work;//DPBMARK Work DPBMARK_END
using Senparc.Weixin.Work.MessageHandlers.Middleware;//DPBMARK Work DPBMARK_END
using Senparc.Weixin.WxOpen;//DPBMARK MiniProgram DPBMARK_END
using Senparc.Weixin.WxOpen.MessageHandlers.Middleware;//DPBMARK MiniProgram DPBMARK_END
using System;
using System.IO;
using System.Text;

namespace Senparc.Weixin.Sample.NetCore3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();//使用Session（实践证明需要在配置 Mvc 之前）

            var builder = services.AddControllersWithViews()
                                  .AddNewtonsoftJson();// 支持 NewtonsoftJson

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();//使用本地缓存必须添加

            services.AddSenparcWeixinServices(Configuration);//Senparc.Weixin 注册（必须）
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            var registerService = app
                    //使用 Senparc.CO2NET 引擎
                    .UseSenparcGlobal(env, senparcSetting.Value, g => { })
                    //使用 Senparc.Weixin SDK
                    .UseSenparcWeixin(senparcWeixinSetting.Value, weixinRegister =>
                    {
                        //注册最新的 TenPay V3
                        weixinRegister.RegisterTenpayRealV3(senparcWeixinSetting.Value, "【盛派网络小助手】公众号-RealV3");
                    });
        }

        /// <summary>
        /// 配置微信跟踪日志（演示，按需）
        /// </summary>
        private void ConfigTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭

            //如果全局的IsDebug（Senparc.CO2NET.Config.IsDebug）为false，此处可以单独设置true，否则自动为true
            CO2NET.Trace.SenparcTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //全局自定义日志记录回调
            CO2NET.Trace.SenparcTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            WeixinTrace.OnWeixinExceptionFunc = async ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员                                   -- DPBMARK Redis
                var eventService = new Senparc.Weixin.MP.Sample.CommonService.EventService();
                await eventService.ConfigOnWeixinExceptionFunc(ex);      // DPBMARK_END
            };
        }

        // -- DPBMARK Redis
        /// <summary>
        /// 判断当前配置是否满足使用 Redis（根据是否已经修改了默认配置字符串判断）
        /// </summary>
        /// <param name="senparcSetting"></param>
        /// <returns></returns>
        private bool UseRedis(SenparcSetting senparcSetting, out string redisConfigurationStr)
        {
            redisConfigurationStr = senparcSetting.Cache_Redis_Configuration;
            var useRedis = !string.IsNullOrEmpty(redisConfigurationStr) && redisConfigurationStr != "#{Cache_Redis_Configuration}#"/*默认值，不启用*/;
            return useRedis;
        }
        // -- DPBMARK_END

        // -- DPBMARK Memcached
        /// <summary>
        /// 初步判断当前配置是否满足使用 Memcached（根据是否已经修改了默认配置字符串判断）
        /// </summary>
        /// <param name="senparcSetting"></param>
        /// <returns></returns>
        private bool UseMemcached(SenparcSetting senparcSetting, out string memcachedConfigurationStr)
        {
            memcachedConfigurationStr = senparcSetting.Cache_Memcached_Configuration;
            var useMemcached = !string.IsNullOrEmpty(memcachedConfigurationStr) && memcachedConfigurationStr != "#{Cache_Memcached_Configuration}#";
            return useMemcached;
        }
        // -- DPBMARK_END

    }
}
