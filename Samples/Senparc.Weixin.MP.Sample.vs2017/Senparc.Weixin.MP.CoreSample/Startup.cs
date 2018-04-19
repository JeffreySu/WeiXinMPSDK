using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;
using Senparc.Weixin.Threads;

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

            new ServiceCollection();

            //添加Senparc.Weixin配置文件（内容可以根据需要对应修改）
            services.Configure<SenparcWeixinSetting>(Configuration.GetSection("SenparcWeixinSetting"));

            //添加Memcached配置（按需）
            services.AddSenparcMemcached(options =>
            {
                options.AddServer("memcached", 11211);
                //options.AddPlainTextAuthenticator("", "usename", "password");
            });
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

            #region 微信相关

            ////注册微信
            //AccessTokenContainer.Register(senparcWeixinSetting.Value.WeixinAppId, senparcWeixinSetting.Value.WeixinAppSecret);

            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting.Value;

            //提供网站根目录
            if (env.ContentRootPath != null)
            {
                Senparc.Weixin.Config.RootDictionaryPath = env.ContentRootPath;
                Senparc.Weixin.MP.Sample.CommonService.Utilities.Server.AppDomainAppPath = env.ContentRootPath;// env.ContentRootPath;
            }
            Senparc.Weixin.MP.Sample.CommonService.Utilities.Server.WebRootPath = env.WebRootPath;// env.ContentRootPath;



            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            RegisterWeixinCache(app);       //注册分布式缓存（按需，如果需要，必须放在第一个）
            ConfigWeixinTraceLog();         //配置微信跟踪日志（按需）
            RegisterWeixinThreads();        //激活微信缓存及队列线程（必须）
            RegisterSenparcWeixin();        //注册Demo所用微信公众号的账号信息（按需）
            RegisterSenparcWorkWeixin();    //注册Demo所用企业微信的账号信息（按需）
            RegisterWeixinPay();            //注册微信支付（按需）
            RegisterWeixinThirdParty();     //注册微信第三方平台（按需）

            /* 微信配置结束 */

            #endregion
        }

        /// <summary>
        /// 自定义缓存策略
        /// </summary>
        private void RegisterWeixinCache(IApplicationBuilder app)
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //如果留空，默认为localhost（默认端口）

            #region  Redis配置
            var redisConfiguration = senparcWeixinSetting.Cache_Redis_Configuration;
            RedisManager.ConfigurationOption = redisConfiguration;

            //如果不执行下面的注册过程，则默认使用本地缓存

            if (!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置")
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//Redis
            }

            #endregion

            #region Memcached 配置

            app.UseEnyimMemcached();

            //var memcachedConfig = new Dictionary<string, int>()
            //{
            //    { "localhost",9101 }
            //};
            //MemcachedObjectCacheStrategy.RegisterServerList(memcachedConfig);

            #endregion

            //CacheStrategyFactory.RegisterContainerCacheStrategy(() => MemcachedContainerStrategy.Instance);//Memcached
        }

        /// 配置微信跟踪日志
        /// </summary>
        private void ConfigWeixinTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.WeixinTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //自定义日志记录回调
            Senparc.Weixin.WeixinTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            Senparc.Weixin.WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员
                var eventService = new Senparc.Weixin.MP.Sample.CommonService.EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);
            };
        }

        /// <summary>
        /// 激活微信缓存
        /// </summary>
        private void RegisterWeixinThreads()
        {
            ThreadUtility.Register();//如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
        }


        /// <summary>
        /// 注册Demo所用微信公众号的账号信息
        /// </summary>
        private void RegisterSenparcWeixin()
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //注册公众号
            AccessTokenContainer.Register(
                senparcWeixinSetting.WeixinAppId,
                senparcWeixinSetting.WeixinAppSecret,
                "【盛派网络小助手】公众号");

            //注册小程序（完美兼容）
            AccessTokenContainer.Register(
                senparcWeixinSetting.WxOpenAppId,
                senparcWeixinSetting.WxOpenAppSecret,
                "【盛派互动】小程序");
        }

        /// <summary>
        /// 注册Demo所用企业微信的账号信息
        /// </summary>
        private void RegisterSenparcWorkWeixin()
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            Senparc.Weixin.Work.Containers.AccessTokenContainer.Register(
                senparcWeixinSetting.WeixinCorpId,
                senparcWeixinSetting.WeixinCorpSecret,
                "【盛派网络】企业微信"
                );
        }


        /// <summary>
        /// 注册微信支付
        /// </summary>
        private void RegisterWeixinPay()
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //提供微信支付信息
            var weixinPay_PartnerId = senparcWeixinSetting.WeixinPay_PartnerId;
            var weixinPay_Key = senparcWeixinSetting.WeixinPay_Key;
            var weixinPay_AppId = senparcWeixinSetting.WeixinPay_AppId;
            var weixinPay_AppKey = senparcWeixinSetting.WeixinPay_AppKey;
            var weixinPay_TenpayNotify = senparcWeixinSetting.WeixinPay_TenpayNotify;

            var tenPayV3_MchId = senparcWeixinSetting.TenPayV3_MchId;
            var tenPayV3_Key = senparcWeixinSetting.TenPayV3_Key;
            var tenPayV3_AppId = senparcWeixinSetting.TenPayV3_AppId;
            var tenPayV3_AppSecret = senparcWeixinSetting.TenPayV3_AppSecret;
            var tenPayV3_TenpayNotify = senparcWeixinSetting.TenPayV3_TenpayNotify;

            var weixinPayInfo = new TenPayInfo(weixinPay_PartnerId, weixinPay_Key, weixinPay_AppId, weixinPay_AppKey, weixinPay_TenpayNotify);
            TenPayInfoCollection.Register(weixinPayInfo);//微信V2（旧版）

            var tenPayV3Info = new TenPayV3Info(tenPayV3_AppId, tenPayV3_AppSecret, tenPayV3_MchId, tenPayV3_Key, tenPayV3_TenpayNotify);
            TenPayV3InfoCollection.Register(tenPayV3Info);//微信V3（新版）
        }

        /// <summary>
        /// 注册微信第三方平台
        /// </summary>
        private void RegisterWeixinThirdParty()
        {
            Func<string, string> getComponentVerifyTicketFunc = componentAppId =>
            {
                var dir = Path.Combine(Senparc.Weixin.Config.RootDictionaryPath, "App_Data\\OpenTicket");
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
            };

            Func<string, string, string> getAuthorizerRefreshTokenFunc = (componentAppId, auhtorizerId) =>
            {
                var dir = Path.Combine(Senparc.Weixin.Config.RootDictionaryPath, "App_Data\\AuthorizerInfo\\" + componentAppId);
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
            };

            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc = (componentAppId, auhtorizerId, refreshResult) =>
            {
                var dir = Path.Combine(Senparc.Weixin.Config.RootDictionaryPath, "App_Data\\AuthorizerInfo\\" + componentAppId);
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
            };

            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //执行注册
            ComponentContainer.Register(
                senparcWeixinSetting.Component_Appid,
                senparcWeixinSetting.Component_Secret,
                getComponentVerifyTicketFunc,
                getAuthorizerRefreshTokenFunc,
                authorizerTokenRefreshedFunc,
                "【盛派网络】开放平台");
        }
    }
}
