/*
 * 重要提示：
 * 从 Senparc.Weixin SDK v6.7 起，开始支持中间件注册方式，并默认支持异步方法，因此不再需要创建 Controller。
 * 中间件输出结果和 Controller 等效。
 */
 
 
 /*----------------------------------------------------------------
    Copyright (C) 2023 Senparc
    
    文件名：WeixinAsyncController.cs
    文件功能描述：此Controller为异步Controller（Action），使用异步线程处理并发请求。
    
    
    创建标识：Senparc - 20150312

    修改标识：Senparc - 20170123
    修改描述：使用异步MessageHandler方法

    修改标识：Senparc - 20191003
    修改描述：改用中间件形式，已经不需要 Controller

----------------------------------------------------------------*/


//DPBMARK_FILE MP

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//using Microsoft.AspNetCore.Mvc;
//using Senparc.Weixin.MP.Entities.Request;
//using Senparc.Weixin.MP.MvcExtension;
//using Senparc.Weixin.Sample.CommonService.CustomMessageHandler;
//using System.IO;
//using Senparc.Weixin.HttpUtility;
//using Senparc.Weixin.Sample.CommonService.Utilities;
//using Senparc.CO2NET.HttpUtility;
//using System.Xml.Linq;
//using System.Threading;
//using Senparc.NeuChar.MessageHandlers;
//using Senparc.Weixin.MP;

//namespace Senparc.Weixin.Sample.Net6.Controllers
//{
//    /// <summary>
//    /// 此Controller为异步Controller（Action），使用异步线程处理并发请求。
//    /// 为了方便演示，此Controller中没有加入多余的日志记录等示例，保持了最简单的Controller写法。日志等其他操作可以参考WeixinController.cs。
//    /// 提示：异步Controller并不是在任何情况下都能提升效率（响应时间），当请求量非常小的时候反而会增加一定的开销。
//    /// </summary>
//    public class WeixinAsyncController : Controller
//    {
//        public static readonly string Token = Config.SenparcWeixinSetting.Token ?? CheckSignature.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
//        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
//        public static readonly string AppId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。

//        readonly Func<string> _getRandomFileName = () => SystemTime.Now.ToString("yyyyMMdd-HHmmss") + "_Async_" + Guid.NewGuid().ToString("n").Substring(0, 6);


//        public WeixinAsyncController()
//        {
//        }


//        [HttpGet]
//        [ActionName("Index")]
//        public Task<ActionResult> Get(string signature, string timestamp, string nonce, string echostr)
//        {
//            return Task.Factory.StartNew(() =>
//                 {
//                     if (CheckSignature.Check(signature, timestamp, nonce, Token))
//                     {
//                         return echostr; //返回随机字符串则表示验证通过
//                     }
//                     else
//                     {
//                         return "failed:" + signature + "," + MP.CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
//                             "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
//                     }
//                 }).ContinueWith<ActionResult>(task => Content(task.Result));
//        }

//        public CustomMessageHandler MessageHandler = null;//开放出MessageHandler是为了做单元测试，实际使用过程中不需要

//        /// <summary>
//        /// 最简化的处理流程
//        /// </summary>
//        [HttpPost]
//        [ActionName("Index")]
//        public async Task<ActionResult> Post(PostModel postModel)
//        {
//            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
//            {
//                return new WeixinResult("参数错误！");
//            }

//            postModel.Token = Token;
//            postModel.EncodingAESKey = EncodingAESKey; //根据自己后台的设置保持一致
//            postModel.AppId = AppId; //根据自己后台的设置保持一致

//            var cancellationToken = new CancellationToken();//给异步方法使用

//            var messageHandler = new CustomMessageHandler(Request.GetRequestMemoryStream(), postModel, 10);

//            #region 没有重写的异步方法将默认尝试调用同步方法中的代码（为了偷懒）

//            /* 使用 SelfSynicMethod 的好处是可以让异步、同步方法共享同一套（同步）代码，无需写两次，
//             * 当然，这并不一定适用于所有场景，所以是否选用需要根据实际情况而定，这里只是演示，并不盲目推荐。*/
//            messageHandler.DefaultMessageHandlerAsyncEvent = DefaultMessageHandlerAsyncEvent.SelfSynicMethod;

//            #endregion

//            #region 设置消息去重 设置

//            /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
//             * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
//            messageHandler.OmitRepeatedMessage = true;//默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能

//            #endregion

//            messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

//            await messageHandler.ExecuteAsync(cancellationToken); //执行微信处理过程（关键）

//            messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

//            MessageHandler = messageHandler;//开放出MessageHandler是为了做单元测试，实际使用过程中这一行不需要

//            return new FixWeixinBugWeixinResult(messageHandler);
//        }
//    }
//}
