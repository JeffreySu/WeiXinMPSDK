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
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.ComponentAPIs;

namespace Senparc.Weixin.MP.Sample
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            RegisterWeixinPay();//注册微信支付
            RegisterWeixinThirdParty(); //注册微信第三方平台

            Senparc.Weixin.Config.IsDebug = true;//这里设为Debug状态时，/App_Data/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭
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
                var file = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\OpenTicket", string.Format("{0}.txt", componentAppId));
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var ticket = sr.ReadToEnd();
                        return ticket;
                    }
                }
            };

            Func<string, string> getAuthorizerRefreshTokenFunc = auhtorizerId =>
            {
                var file = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\AuthorizerInfo", string.Format("{0}.bin", auhtorizerId));
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

            Action<string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc = (auhtorizerId, refreshResult) =>
             {
                 var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\AuthorizerInfo");
                 if (!Directory.Exists(filePath))
                 {
                     Directory.CreateDirectory(filePath);
                 }

                 var file = Path.Combine(filePath, string.Format("{0}.bin", auhtorizerId));
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
                authorizerTokenRefreshedFunc);
        }
    }
}
