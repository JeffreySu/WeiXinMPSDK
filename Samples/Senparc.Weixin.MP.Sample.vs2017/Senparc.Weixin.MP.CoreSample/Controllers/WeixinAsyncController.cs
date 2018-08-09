/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：WeixinAsyncController.cs
    文件功能描述：此Controller为异步Controller（Action），使用异步线程处理并发请求。
    
    
    创建标识：Senparc - 20150312

    修改标识：Senparc - 20170123
    修改描述：使用异步MessageHandler方法

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using System.IO;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.CO2NET.HttpUtility;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 此Controller为异步Controller（Action），使用异步线程处理并发请求。
    /// 为了方便演示，此Controller中没有加入多余的日志记录等示例，保持了最简单的Controller写法。日志等其他操作可以参考WeixinController.cs。
    /// 提示：异步Controller并不是在任何情况下都能提升效率（响应时间），当请求量非常小的时候反而会增加一定的开销。
    /// </summary>
    public class WeixinAsyncController : Controller
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token ?? CheckSignature.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。

        readonly Func<string> _getRandomFileName = () => DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_Async_" + Guid.NewGuid().ToString("n").Substring(0, 6);


        public WeixinAsyncController()
        {
        }


        [HttpGet]
        [ActionName("Index")]
        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
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
                 }).ContinueWith<ActionResult>(task => Content(task.Result));
        }

        public CustomMessageHandler MessageHandler = null;//开放出MessageHandler是为了做单元测试，实际使用过程中不需要

        /// <summary>
        /// 最简化的处理流程
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return new WeixinResult("参数错误！");
            }

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey; //根据自己后台的设置保持一致
            postModel.AppId = AppId; //根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, 10);

            messageHandler.DefaultMessageHandlerAsyncEvent = Weixin.MessageHandlers.DefaultMessageHandlerAsyncEvent.SelfSynicMethod;//没有重写的异步方法将默认尝试调用同步方法中的代码（为了偷懒）

            #region 设置消息去重

            /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
             * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
            messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

            #endregion

            #region 记录 Request 日志

            var logPath = Server.GetMapPath(string.Format("~/App_Data/MP/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
            messageHandler.RequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_{1}_{2}.txt", _getRandomFileName(),
                messageHandler.RequestMessage.FromUserName,
                messageHandler.RequestMessage.MsgType)));
            if (messageHandler.UsingEcryptMessage)
            {
                messageHandler.EcryptRequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_Ecrypt_{1}_{2}.txt", _getRandomFileName(),
                    messageHandler.RequestMessage.FromUserName,
                    messageHandler.RequestMessage.MsgType)));
            }

            #endregion

            await messageHandler.ExecuteAsync(); //执行微信处理过程

            #region 记录 Response 日志

            //测试时可开启，帮助跟踪数据

            //if (messageHandler.ResponseDocument == null)
            //{
            //    throw new Exception(messageHandler.RequestDocument.ToString());
            //}
            if (messageHandler.ResponseDocument != null)
            {
                messageHandler.ResponseDocument.Save(Path.Combine(logPath, string.Format("{0}_Response_{1}_{2}.txt", _getRandomFileName(),
                    messageHandler.ResponseMessage.ToUserName,
                    messageHandler.ResponseMessage.MsgType)));
            }

            if (messageHandler.UsingEcryptMessage && messageHandler.FinalResponseDocument != null)
            {
                //记录加密后的响应信息
                messageHandler.FinalResponseDocument.Save(Path.Combine(logPath, string.Format("{0}_Response_Final_{1}_{2}.txt", _getRandomFileName(),
                    messageHandler.ResponseMessage.ToUserName,
                    messageHandler.ResponseMessage.MsgType)));
            }

            #endregion

            MessageHandler = messageHandler;//开放出MessageHandler是为了做单元测试，实际使用过程中不需要

            return new FixWeixinBugWeixinResult(messageHandler);
        }

        /// <summary>
        /// 为测试并发性能而建
        /// </summary>
        /// <returns></returns>
        public Task<ActionResult> ForTest()
        {
            //异步并发测试（提供给单元测试使用）
            return Task.Factory.StartNew<ActionResult>(() =>
            {
                DateTime begin = DateTime.Now;
                int t1, t2, t3;
                System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
                System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.1));
                DateTime end = DateTime.Now;
                var thread = System.Threading.Thread.CurrentThread;
                var result = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
                    thread.ManagedThreadId,
                    HttpContext.GetHashCode(),
                    begin,
                    end,
                    t2 - t1
                    );
                return Content(result);
            });
        }
    }
}
