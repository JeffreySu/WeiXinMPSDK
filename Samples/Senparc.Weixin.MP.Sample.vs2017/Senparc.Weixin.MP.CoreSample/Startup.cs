using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Work;
using System.Collections.Generic;
using System.IO;

namespace Senparc.Weixin.MP.CoreSample
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
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSenparcGlobalServices()//Senparc.CO2NET 全局注册
                                               //添加Senparc.Weixin配置文件（内容可以根据需要对应修改）
                    .Configure<SenparcWeixinSetting>(Configuration.GetSection("SenparcWeixinSetting"));


            #region Senparc.CO2NET Memcached 配置（按需）

            //添加Memcached配置（按需）
            services.AddSenparcMemcached(options =>
                    {
                        options.AddServer("memcached", 11211);
                        //options.AddPlainTextAuthenticator("", "usename", "password");
                    });

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            //引入EnableRequestRewind中间件
            app.UseEnableRequestRewind();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            #region 提供网站根目录（当前 Sample 用到，和 SDK 无关）
            if (env.ContentRootPath != null)
            {
                Senparc.Weixin.MP.Sample.CommonService.Utilities.Server.AppDomainAppPath = env.ContentRootPath;// env.ContentRootPath;
            }
            Senparc.Weixin.MP.Sample.CommonService.Utilities.Server.WebRootPath = env.WebRootPath;// env.ContentRootPath;
            #endregion


            var isDebug = true;//当前是否是Debug状态
            IRegisterService register = RegisterService.Start(env, isDebug);

            #region 注册线程（必须） 在Start()中已经自动注册，此处也可以省略，仅作演示

            register.RegisterThreads();  //启动线程，RegisterThreads()也可以省略，在RegisterService.Start()中已经自动注册

            #endregion

            #region 缓存配置（按需）

            // 当同一个分布式缓存同时服务于多个网站（应用程序池）时，可以使用命名空间将其隔离（非必须）
            register.ChangeDefaultCacheNamespace("DefaultWeixinCache");

            //配置Redis缓存（按需，独立）
            var redisConfigurationStr = senparcWeixinSetting.Value.Cache_Redis_Configuration;
            var useRedis = !string.IsNullOrEmpty(redisConfigurationStr) && redisConfigurationStr != "Redis配置";
            if (useRedis)//这里为了方便不同环境的开发者进行配置，做成了判断的方式，实际开发环境一般是确定的
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
                //如果不进行任何设置，则默认使用内存缓存
            }


            //配置Memcached缓存（按需，独立）
            //这里配置的是 CO2NET 的 Memcached 缓存（如果执行了下面的 app.UseSenparcWeixinCacheMemcached()，
            //会自动包含本步骤，这一步注册可以忽略）
            var useMemcached = false;
            app.UseWhen(h => useMemcached, a => a.UseEnyimMemcached());

            #endregion

            #region 注册日志（按需）

            register.RegisterTraceLog(ConfigTraceLog);//配置TraceLog

            #endregion

            #region 微信相关配置


            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            //注册开始

            #region 微信缓存（按需，必须在 register.UseSenparcWeixin() 之前）

            // 微信的 Memcached 缓存，如果不使用则注释掉（开启前必须保证配置有效，否则会抛错）
            if (useMemcached)
            {
                app.UseSenparcWeixinCacheMemcached();
            }
            //app.UseWhen(h => useMemcached, a => a.UseSenparcWeixinCacheMemcached());//如果连接而配置未生效，不能这么使用

            //微信的 Redis 缓存，如果不使用则注释掉（开启前必须保证配置有效，否则会抛错）
            if (useRedis)
            {
                app.UseSenparcWeixinCacheRedis();
            }
            //app.UseWhen(h => useRedis, a => a.UseSenparcWeixinCacheRedis());//如果连接而配置未生效，不能这么使用

            #endregion

            //开始注册微信信息
            register.UseSenparcWeixin(senparcWeixinSetting.Value, isDebug/*此处为单独用于微信的调试状态*/, () => GetExContainerCacheStrategies(senparcWeixinSetting.Value)) //注意：这里没有 ; 下面可接着写

            #region 注册公众号或小程序（按需）

                //注册公众号
                .RegisterMpAccount(
                    senparcWeixinSetting.Value.WeixinAppId,
                    senparcWeixinSetting.Value.WeixinAppSecret,
                    "【盛派网络小助手】公众号")
                //注册多个公众号或小程序
                .RegisterMpAccount(
                    senparcWeixinSetting.Value.WxOpenAppId,
                    senparcWeixinSetting.Value.WxOpenAppSecret,
                    "【盛派互动】小程序")

            #endregion

            #region 注册企业号（按需）

                //注册企业号
                .RegisterWorkAccount(
                    senparcWeixinSetting.Value.WeixinCorpId,
                    senparcWeixinSetting.Value.WeixinCorpSecret,
                    "【盛派网络】企业微信")
                //还可注册任意多个企业号

            #endregion

            #region 注册微信支付（按需）

                //注册旧微信支付版本（V2）
                .RegisterTenpayOld(() =>
                {
                    //提供微信支付信息
                    var weixinPay_PartnerId = senparcWeixinSetting.Value.WeixinPay_PartnerId;
                    var weixinPay_Key = senparcWeixinSetting.Value.WeixinPay_Key;
                    var weixinPay_AppId = senparcWeixinSetting.Value.WeixinPay_AppId;
                    var weixinPay_AppKey = senparcWeixinSetting.Value.WeixinPay_AppKey;
                    var weixinPay_TenpayNotify = senparcWeixinSetting.Value.WeixinPay_TenpayNotify;
                    var weixinPayInfo = new TenPayInfo(weixinPay_PartnerId, weixinPay_Key,
                            weixinPay_AppId, weixinPay_AppKey, weixinPay_TenpayNotify);
                    return weixinPayInfo;
                })
                //注册最新微信支付版本（V3）
                .RegisterTenpayV3(() =>
                {
                    //提供微信支付信息
                    var tenPayV3_MchId = senparcWeixinSetting.Value.TenPayV3_MchId;
                    var tenPayV3_Key = senparcWeixinSetting.Value.TenPayV3_Key;
                    var tenPayV3_AppId = senparcWeixinSetting.Value.TenPayV3_AppId;
                    var tenPayV3_AppSecret = senparcWeixinSetting.Value.TenPayV3_AppSecret;
                    var tenPayV3_TenpayNotify = senparcWeixinSetting.Value.TenPayV3_TenpayNotify;
                    var tenPayV3Info = new TenPayV3Info(tenPayV3_AppId, tenPayV3_AppSecret,
                        tenPayV3_MchId, tenPayV3_Key, tenPayV3_TenpayNotify);
                    return tenPayV3Info;
                })

            #endregion

            #region 注册微信第三方平台（按需）

                .RegisterOpenComponent(
                    senparcWeixinSetting.Value.Component_Appid,
                    senparcWeixinSetting.Value.Component_Secret,

                    //getComponentVerifyTicketFunc
                    componentAppId =>
                    {
                        var dir = Path.Combine(Server.GetMapPath("~/App_Data/OpenTicket"));
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        var file = Path.Combine(dir, string.Format("{0}.txt", componentAppId));
                        using (var fs = new FileStream(file, FileMode.Open))
                        {
                            using (var sr = new StreamReader(fs))
                            {
                                var ticket = sr.ReadToEnd();
                                return ticket;
                            }
                        }
                    },

                     //getAuthorizerRefreshTokenFunc
                     (componentAppId, auhtorizerId) =>
                     {
                         var dir = Path.Combine(Server.GetMapPath("~/App_Data/AuthorizerInfo/" + componentAppId));
                         if (!Directory.Exists(dir))
                         {
                             Directory.CreateDirectory(dir);
                         }

                         var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                         if (!File.Exists(file))
                         {
                             return null;
                         }

                         using (Stream fs = new FileStream(file, FileMode.Open))
                         {
                             var binFormat = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                             var result = (RefreshAuthorizerTokenResult)binFormat.Deserialize(fs);
                             return result.authorizer_refresh_token;
                         }
                     },

                     //authorizerTokenRefreshedFunc
                     (componentAppId, auhtorizerId, refreshResult) =>
                     {
                         var dir = Path.Combine(Server.GetMapPath("~/App_Data/AuthorizerInfo/" + componentAppId));
                         if (!Directory.Exists(dir))
                         {
                             Directory.CreateDirectory(dir);
                         }

                         var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                         using (Stream fs = new FileStream(file, FileMode.Create))
                         {
                             //这里存了整个对象，实际上只存RefreshToken也可以，有了RefreshToken就能刷新到最新的AccessToken
                             var binFormat = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                             binFormat.Serialize(fs, refreshResult);
                             fs.Flush();
                         }
                     }, "【盛派网络】开放平台")

            #endregion

            ;

            /* 微信配置结束 */

            #endregion
        }

        /// 配置微信跟踪日志
        /// </summary>
        private void ConfigTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭

            //如果全局的IsDebug（Senparc.CO2NET.Config.IsDebug）为false，此处可以单独设置true，否则自动为true
            Config.IsDebug = true;
            CO2NET.Trace.SenparcTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //全局自定义日志记录回调
            CO2NET.Trace.SenparcTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员
                var eventService = new Senparc.Weixin.MP.Sample.CommonService.EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);
            };
        }

        /// <summary>
        /// 获取Container扩展缓存策略
        /// </summary>
        /// <returns></returns>
        private IList<IDomainExtensionCacheStrategy> GetExContainerCacheStrategies(SenparcWeixinSetting senparcWeixinSetting)
        {
            var exContainerCacheStrategies = new List<IDomainExtensionCacheStrategy>();

            //判断Redis是否可用
            var redisConfiguration = senparcWeixinSetting.Cache_Redis_Configuration;
            if ((!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置"))
            {
                exContainerCacheStrategies.Add(RedisContainerCacheStrategy.Instance);
            }

            //判断Memcached是否可用
            var memcachedConfiguration = senparcWeixinSetting.Cache_Memcached_Configuration;
            if ((!string.IsNullOrEmpty(memcachedConfiguration) && redisConfiguration != "Memcached配置"))
            {
                exContainerCacheStrategies.Add(MemcachedContainerCacheStrategy.Instance);
            }

            //也可扩展自定义的缓存策略

            return exContainerCacheStrategies;
        }

    }
}
