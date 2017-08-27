using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Sample.CommonService;
using Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.WebSocket;
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;
using Senparc.Weixin.Threads;

namespace Senparc.Weixin.MP.Sample
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterWebSocket();        //微信注册WebSocket模块（按需，必须执行在RouteConfig.RegisterRoutes()之前）

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            RegisterWeixinCache();      //注册分布式缓存（按需，如果需要，必须放在第一个）
            ConfigWeixinTraceLog();     //配置微信跟踪日志（按需）
            RegisterWeixinThreads();    //激活微信缓存及队列线程（必须）
            RegisterSenparcWeixin();    //注册Demo所用微信公众号的账号信息（按需）
            RegisterSenparcQyWeixin();  //注册Demo所用微信企业号的账号信息（按需）
            RegisterSenparcWorkWeixin();  //注册Demo所用企业微信的账号信息（按需）
            RegisterWeixinPay();        //注册微信支付（按需）
            RegisterWeixinThirdParty(); //注册微信第三方平台（按需）

            /* 微信配置结束 */
        }

        /// <summary>
        /// 自定义缓存策略
        /// </summary>
        private void RegisterWeixinCache()
        {
            //如果留空，默认为localhost（默认端口）

            #region  Redis配置
            var redisConfiguration = System.Configuration.ConfigurationManager.AppSettings["Cache_Redis_Configuration"];
            RedisManager.ConfigurationOption = redisConfiguration;

            //如果不执行下面的注册过程，则默认使用本地缓存

            if (!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置")
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//Redis
            }

            #endregion

            #region Memcached 配置

            var memcachedConfig = new Dictionary<string, int>()
            {
                { "localhost",9101 }
            };
            MemcachedObjectCacheStrategy.RegisterServerList(memcachedConfig);


            #endregion

            //CacheStrategyFactory.RegisterContainerCacheStrategy(() => MemcachedContainerStrategy.Instance);//Memcached
        }

        /// <summary>
        /// 注册WebSocket模块（可用于小程序或独立WebSocket应用）
        /// </summary>
        private void RegisterWebSocket()
        {
            Senparc.WebSocket.WebSocketConfig.RegisterRoutes(RouteTable.Routes);
            Senparc.WebSocket.WebSocketConfig.RegisterMessageHandler<CustomWebSocketMessageHandler>();
        }

        /// <summary>
        /// 激活微信缓存
        /// </summary>
        private void RegisterWeixinThreads()
        {
            ThreadUtility.Register();
        }

        /// <summary>
        /// 注册Demo所用微信公众号的账号信息
        /// </summary>
        private void RegisterSenparcWeixin()
        {
            //注册公众号
            AccessTokenContainer.Register(
                System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"],
                System.Configuration.ConfigurationManager.AppSettings["WeixinAppSecret"],
                "【盛派网络小助手】公众号");

            //注册小程序（完美兼容）
            AccessTokenContainer.Register(
                System.Configuration.ConfigurationManager.AppSettings["WxOpenAppId"],
                System.Configuration.ConfigurationManager.AppSettings["WxOpenAppSecret"],
                "【盛派互动】小程序");
        }

        /// <summary>
        /// 注册Demo所用微信企业号的账号信息
        /// </summary>
        private void RegisterSenparcQyWeixin()
        {
            Senparc.Weixin.Work.Containers.ProviderTokenContainer.Register(
                System.Configuration.ConfigurationManager.AppSettings["WeixinCorpId"],
                System.Configuration.ConfigurationManager.AppSettings["WeixinCorpSecret"],
                "【盛派网络】企业微信"
                );
        }

        /// <summary>
        /// 注册Demo所用企业微信的账号信息
        /// </summary>
        private void RegisterSenparcWorkWeixin()
        {
            Senparc.Weixin.Work.Containers.AccessTokenContainer.Register(
                System.Configuration.ConfigurationManager.AppSettings["WeixinCorpId"],
                System.Configuration.ConfigurationManager.AppSettings["WeixinCorpSecret"],
                "【盛派网络】企业微信"
                );

            Senparc.Weixin.Work.Containers.ProviderTokenContainer.Register(
                System.Configuration.ConfigurationManager.AppSettings["WeixinCorpId"],
                System.Configuration.ConfigurationManager.AppSettings["WeixinCorpSecret"],
                "【盛派网络】企业微信"
                );
        }

        /// <summary>
        /// 注册微信支付
        /// </summary>
        private void RegisterWeixinPay()
        {
            //提供微信支付信息
            var weixinPay_PartnerId = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_PartnerId"];
            var weixinPay_Key = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_Key"];
            var weixinPay_AppId = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_AppId"];
            var weixinPay_AppKey = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_AppKey"];
            var weixinPay_TenpayNotify = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_TenpayNotify"];

            var tenPayV3_MchId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"];
            var tenPayV3_Key = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_Key"];
            var tenPayV3_AppId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppId"];
            var tenPayV3_AppSecret = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppSecret"];
            var tenPayV3_TenpayNotify = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_TenpayNotify"];

            var weixinPayInfo = new TenPayInfo(weixinPay_PartnerId, weixinPay_Key, weixinPay_AppId, weixinPay_AppKey, weixinPay_TenpayNotify);
            TenPayInfoCollection.Register(weixinPayInfo);
            var tenPayV3Info = new TenPayV3Info(tenPayV3_AppId, tenPayV3_AppSecret, tenPayV3_MchId, tenPayV3_Key,
                                                tenPayV3_TenpayNotify);
            TenPayV3InfoCollection.Register(tenPayV3Info);
        }

        /// <summary>
        /// 注册微信第三方平台
        /// </summary>
        private void RegisterWeixinThirdParty()
        {
            Func<string, string> getComponentVerifyTicketFunc = componentAppId =>
            {
                var dir = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\OpenTicket");
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
                 var dir = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\AuthorizerInfo\\" + componentAppId);
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
                     BinaryFormatter binFormat = new BinaryFormatter();
                     var result = (RefreshAuthorizerTokenResult)binFormat.Deserialize(fs);
                     return result.authorizer_refresh_token;
                 }
             };

            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc = (componentAppId, auhtorizerId, refreshResult) =>
              {
                  var dir = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\AuthorizerInfo\\" + componentAppId);
                  if (!Directory.Exists(dir))
                  {
                      Directory.CreateDirectory(dir);
                  }

                  var file = Path.Combine(dir, string.Format("{0}.bin", auhtorizerId));
                  using (Stream fs = new FileStream(file, FileMode.Create))
                  {
                      //这里存了整个对象，实际上只存RefreshToken也可以，有了RefreshToken就能刷新到最新的AccessToken
                      BinaryFormatter binFormat = new BinaryFormatter();
                      binFormat.Serialize(fs, refreshResult);
                      fs.Flush();
                  }
              };

            //执行注册
            ComponentContainer.Register(
                ConfigurationManager.AppSettings["Component_Appid"],
                ConfigurationManager.AppSettings["Component_Secret"],
                getComponentVerifyTicketFunc,
                getAuthorizerRefreshTokenFunc,
                authorizerTokenRefreshedFunc,
                "【盛派网络】开放平台");
        }

        /// <summary>
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
                var eventService = new EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);
            };
        }
    }
}
