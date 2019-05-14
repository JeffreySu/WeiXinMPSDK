/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WeixinController.cs
    文件功能描述：用于处理微信回调的信息


    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

//DPBMARK_FILE MP
using System;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Entities.Request;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Senparc.CO2NET.Cache;
    using Senparc.CO2NET.HttpUtility;
    using Senparc.CO2NET.Utilities;
    using Senparc.Weixin.Entities;
    using Senparc.Weixin.HttpUtility;
    using Senparc.Weixin.MP.MvcExtension;
    using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
    using Senparc.Weixin.MP.Sample.CommonService.Utilities;
    using System.Text;
    using System.Threading.Tasks;

    public partial class WeixinController : Controller
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。

        readonly Func<string> _getRandomFileName = () => SystemTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);

        SenparcWeixinSetting _senparcWeixinSetting;

        public WeixinController(IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
            _senparcWeixinSetting = senparcWeixinSetting.Value;
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://sdk.weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// PS：此方法为简化方法，效果与OldPost一致。
        /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }

            #region 打包 PostModel 信息

            postModel.Token = Token;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致（必须提供）

            #endregion

            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
            var maxRecordCount = 10;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);

            #region 设置消息去重

            /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
             * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
            messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

            #endregion

            try
            {
                messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

                messageHandler.Execute();//执行微信处理过程（关键）

                messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

                //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
                //return new WeixinResult(messageHandler);//v0.8+
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
            }
            catch (Exception ex)
            {
                #region 异常处理
                WeixinTrace.Log("MessageHandler错误：{0}", ex.Message);

                using (TextWriter tw = new StreamWriter(ServerUtility.ContentRootMapPath("~/App_Data/Error_" + _getRandomFileName() + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }

                    if (ex.InnerException != null)
                    {
                        tw.WriteLine("========= InnerException =========");
                        tw.WriteLine(ex.InnerException.Message);
                        tw.WriteLine(ex.InnerException.Source);
                        tw.WriteLine(ex.InnerException.StackTrace);
                    }

                    tw.Flush();
                    tw.Close();
                }
                return Content("");
                #endregion
            }
        }

        /// <summary>
        /// 最简化的处理流程（不加密）
        /// </summary>
        [HttpPost]
        [ActionName("MiniPost")]
        public ActionResult MiniPost(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, 10);

            messageHandler.Execute();//执行微信处理过程

            //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
            //return new WeixinResult(messageHandler);//v0.8+
            return new FixWeixinBugWeixinResult(messageHandler);//v0.8+
        }

        /*
         * v0.3.0之前的原始Post方法见：WeixinController_OldPost.cs
         *
         * 注意：虽然这里提倡使用CustomerMessageHandler的方法，但是MessageHandler基类最终还是基于OldPost的判断逻辑，
         * 因此如果需要深入了解Senparc.Weixin.MP内部处理消息的机制，可以查看WeixinController_OldPost.cs中的OldPost方法。
         * 目前为止OldPost依然有效，依然可用于生产。
         */


        /// <summary>
        /// 为测试并发性能而建
        /// </summary>
        /// <returns></returns>
        public ActionResult ForTest()
        {
            //异步并发测试（提供给单元测试使用）
            var begin = SystemTime.Now;
            int t1, t2, t3;
            System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
            System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
            var end = SystemTime.Now;
            var thread = System.Threading.Thread.CurrentThread;
            var result = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
                    thread.ManagedThreadId,
                    HttpContext.GetHashCode(),
                    begin,
                    end,
                    t2 - t1
                    );
            return Content(result);
        }


        /// <summary>
        /// 多账号注册测试
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> MultiAccountTest()
        {
            //说明：这里的多账号通过 appsettings.json 直接注入，如果您在自己的服务上进行测试，请使用自己对应的 appId、secret 等信息

            //对本接口调用设置限制（如果此站点部署至公网，务必对刷新AccessToken接口做限制！
            var cache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            var cacheKey = "LastMultiAccountTestTime";
            if (!Request.IsLocal() && await cache.CheckExistedAsync(cacheKey))
            {
                var lastMultiAccountTest = await cache.GetAsync<DateTimeOffset>("LastMultiAccountTestTime");
                if ((SystemTime.Now - lastMultiAccountTest).TotalSeconds < 30)
                {
                    return Content("访问频次过快，请稍后再试！");
                }
            }
            //储存当前访问时间，用于限制刷新频次
            await cache.SetAsync(cacheKey, SystemTime.Now);

            //演示通过 key 来获取 SenparcWeixinSetting 储存信息，如果有明确的WeixinAppId，这一步也可以省略
            var setting1 = Weixin.Config.SenparcWeixinSetting.Items["Default"];
            var setting2 = Weixin.Config.SenparcWeixinSetting.Items["第二个公众号"];

            var sb = new StringBuilder();

            //获取一轮AccessToken
            var token1_1 = await MP.Containers.AccessTokenContainer.GetAccessTokenResultAsync(setting1.WeixinAppId, true);
            var token2_1 = await MP.Containers.AccessTokenContainer.GetAccessTokenResultAsync(setting2.WeixinAppId, true);
            sb.Append($"AccessToken 1-1:{token1_1.access_token.Substring(1,20)}...<br>");
            sb.Append($"AccessToken 2-1:{token2_1.access_token.Substring(1, 20)}...<br><br>");

            //重新获取一轮
            var token1_2 = await MP.Containers.AccessTokenContainer.GetAccessTokenResultAsync(setting1.WeixinAppId, true);
            var token2_2 = await MP.Containers.AccessTokenContainer.GetAccessTokenResultAsync(setting2.WeixinAppId, true);
            sb.Append($"AccessToken 1-1:{token1_2.access_token.Substring(1, 20)}...<br>");
            sb.Append($"AccessToken 2-1:{token2_2.access_token.Substring(1, 20)}...<br><br>");

            //使用高级接口返回消息
            var result1 = await MP.AdvancedAPIs.UrlApi.ShortUrlAsync(setting1.WeixinAppId, "long2short", "https://sdk.weixin.senparc.com");
            var result2 = await MP.AdvancedAPIs.UrlApi.ShortUrlAsync(setting2.WeixinAppId, "long2short", "https://weixin.senparc.com");
            sb.Append($"Result 1:{result1.short_url}<br>");
            sb.Append($"Result 2:{result2.short_url}<br><br>");

            return Content(sb.ToString(), "text/html",Encoding.UTF8 );
        }
    }
}
