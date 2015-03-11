using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class WeixinAsyncController : AsyncController
    {
        public static readonly string Token = WebConfigurationManager.AppSettings["WeixinToken"];//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = WebConfigurationManager.AppSettings["WeixinEncodingAESKey"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。

        public WeixinAsyncController()
        {
        }


        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Index(string signature, string timestamp, string nonce, string echostr)
        {
            return Task.Factory.StartNew(() =>
                 {
                     if (CheckSignature.Check(signature, timestamp, nonce, Token))
                     {
                         return echostr; //返回随机字符串则表示验证通过
                     }
                     else
                     {
                         return "failed:" + signature + "," + MP.CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                             "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
                     }
                 }).ContinueWith<ActionResult>(task =>
                    {
                        return Content((string)task.Result);
                    });
        }


        /// <summary>
        /// 最简化的处理流程
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public Task<ActionResult> MiniPost(PostModel postModel)
        {
            return Task.Factory.StartNew(() =>
            {
                if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
                {
                    return new WeixinResult("参数错误！");
                }

                postModel.Token = Token;
                postModel.EncodingAESKey = EncodingAESKey; //根据自己后台的设置保持一致
                postModel.AppId = AppId; //根据自己后台的设置保持一致

                var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, 10);

                messageHandler.Execute(); //执行微信处理过程

                return new FixWeixinBugWeixinResult(messageHandler);

            }).ContinueWith<ActionResult>(task =>
            {
                return task;
            });




        }
    }
}
