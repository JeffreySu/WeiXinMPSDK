using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Cache.Memcached;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.RegisterServices;
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
            services.AddMemoryCache();//使用本地缓存必须添加
            services.AddSession();//使用Session

            /*
             * CO2NET 是从 Senparc.Weixin 分离的底层公共基础模块，经过了长达 6 年的迭代优化。
             * 关于 CO2NET 在所有项目中的通用设置可参考 CO2NET 的 Sample：
             * https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore/Startup.cs
             */

            services.AddSenparcGlobalServices(Configuration)//Senparc.CO2NET 全局注册
                    .AddSenparcWeixinServices(Configuration);//Senparc.Weixin 注册

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            //引入EnableRequestRewind中间件
            app.UseEnableRequestRewind();
            app.UseSession();

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

            // 启动 CO2NET 全局注册，必须！
            IRegisterService register = RegisterService.Start(env, senparcSetting.Value)
                                                        .UseSenparcGlobal(false, () => GetExCacheStrategies(senparcSetting.Value));

            //如果需要自动扫描自定义扩展缓存，可以这样使用：
            //register.UseSenparcWeixin(true);
            //如果需要指定自定义扩展缓存，可以这样用：
            //register.UseSenparcWeixin(false, GetExCacheStrategies);

            #region CO2NET 全局配置

            #region 缓存配置（按需）

            //当同一个分布式缓存同时服务于多个网站（应用程序池）时，可以使用命名空间将其隔离（非必须）
            register.ChangeDefaultCacheNamespace("DefaultCO2NETCache");

            //配置全局使用Redis缓存（按需，独立）
            var redisConfigurationStr = senparcSetting.Value.Cache_Redis_Configuration;
            var useRedis = !string.IsNullOrEmpty(redisConfigurationStr) && redisConfigurationStr != "Redis配置";
            if (useRedis)//这里为了方便不同环境的开发者进行配置，做成了判断的方式，实际开发环境一般是确定的
            {
                //设置Redis链接信息，并在全局立即启用Redis缓存。
                register.RegisterCacheRedis(redisConfigurationStr, redisConfiguration => RedisObjectCacheStrategy.Instance);

                //此外还可以通过这种方式修改 Redis 链接信息（不立即启用）：
                //RedisManager.ConfigurationOption = redisConfigurationStr;

                //以下会立即将全局缓存设置为Redis（不修改配置）。
                //如果要使用其他缓存，同样可以在任意地方使用这个方法，修改 RedisObjectCacheStrategy 为其他缓存策略
                //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            }
            //如果这里不进行Redis缓存启用，则目前还是默认使用内存缓存 


            //配置Memcached缓存（按需，独立）
            //这里配置的是 CO2NET 的 Memcached 缓存（如果执行了下面的 app.UseSenparcWeixinCacheMemcached()，
            //会自动包含本步骤，这一步注册可以忽略）
            var useMemcached = false;
            app.UseWhen(h => useMemcached, a =>
            {
                a.UseEnyimMemcached();
                //确保Memcached连接可用后，启用下面的做法：
                //var memcachedConnStr = senparcSetting.Value.Cache_Memcached_Configuration;
                //var memcachedConnDic = new Dictionary<string, int>() {/*进行配置 { "localhost", 9101 }*/ };//可以由 memcachedConnStr 分割得到，或直接填写
                //register.RegisterCacheMemcached(memcachedConnDic, memcachedConfig => MemcachedObjectCacheStrategy.Instance);
            });


            #endregion

            #region 注册日志（按需，建议）

            register.RegisterTraceLog(ConfigTraceLog);//配置TraceLog

            #endregion

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



            //开始注册微信信息，必须！
            register.UseSenparcWeixin(senparcWeixinSetting.Value, senparcSetting.Value)
                //注意：上一行没有 ; 下面可接着写 .RegisterXX()

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
                    "【盛派网络小助手】小程序")//注意：小程序和公众号的AppId/Secret属于并列关系，这里name需要区分开

                //除此以外，仍然可以在程序任意地方注册公众号或小程序：
                //AccessTokenContainer.Register(appId, appSecret, name);//命名空间：Senparc.Weixin.MP.Containers
            #endregion

            #region 注册企业号（按需）

                //注册企业微信
                .RegisterWorkAccount(
                    senparcWeixinSetting.Value.WeixinCorpId,
                    senparcWeixinSetting.Value.WeixinCorpSecret,
                    "【盛派网络】企业微信")
                //还可注册任意多个企业号

                //除此以外，仍然可以在程序任意地方注册企业微信：
                //AccessTokenContainer.Register(corpId, corpSecret, name);//命名空间：Senparc.Weixin.Work.Containers
            #endregion

            #region 注册微信支付（按需）

                //注册旧微信支付版本（V2）
                .RegisterTenpayOld(() =>
                {
                    //提供微信支付（旧版本）信息
                    var weixinPayInfo = new TenPayInfo(senparcWeixinSetting.Value);
                    return weixinPayInfo;
                },
                "【盛派网络小助手】公众号"//这里的 name 和第一个 RegisterMpAccount() 中的一致，会被记录到同一个 SenparcWeixinSettingItem 对象中
                )
                //注册最新微信支付版本（V3）
                .RegisterTenpayV3(() =>
                {
                    //提供微信支付（新版本 V3）信息
                    var tenPayV3Info = new TenPayV3Info(senparcWeixinSetting.Value);
                    return tenPayV3Info;
                }, "【盛派网络小助手】公众号")//记录到同一个 SenparcWeixinSettingItem 对象中

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

            //除此以外，仍然可以在程序任意地方注册开放平台：
            //ComponentContainer.Register();//命名空间：Senparc.Weixin.Open.Containers
            #endregion

            ;

            /* 微信配置结束 */

            #endregion
        }


        /// <summary>
        /// 配置微信跟踪日志
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
            WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员
                var eventService = new Senparc.Weixin.MP.Sample.CommonService.EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);
            };
        }

        /// <summary>
        /// 获取扩展缓存策略
        /// </summary>
        /// <returns></returns>
        private IList<IDomainExtensionCacheStrategy> GetExCacheStrategies(SenparcSetting senparcSetting)
        {
            var exContainerCacheStrategies = new List<IDomainExtensionCacheStrategy>();
            senparcSetting = senparcSetting ?? new SenparcSetting();

            //注意：以下两个 if 判断仅作为演示，方便大家添加自定义的扩展缓存策略，
            //      只要进行了 register.UseSenparcWeixin() 操作，Container 的缓存策略下的 Local、Redis 和 Memcached 系统已经默认自动注册，无需操作！

            #region 演示扩展缓存注册方法

            //判断Redis是否可用
            var redisConfiguration = senparcSetting.Cache_Redis_Configuration;
            if ((!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置"))
            {
                exContainerCacheStrategies.Add(RedisContainerCacheStrategy.Instance);
            }

            //判断Memcached是否可用
            var memcachedConfiguration = senparcSetting.Cache_Memcached_Configuration;
            if ((!string.IsNullOrEmpty(memcachedConfiguration) && memcachedConfiguration != "Memcached配置"))
            {
                exContainerCacheStrategies.Add(MemcachedContainerCacheStrategy.Instance);//TODO:如果没有进行配置会产生异常
            }

            #endregion

            //扩展自定义的缓存策略

            return exContainerCacheStrategies;
        }

    }
}
