/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：WeixinController.cs
    文件功能描述：用于处理微信回调的信息


    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using Senparc.Weixin.Entities;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;

#if NET45
using System.Web
using System.Web.Mvc;
using Senparc.Weixin.MP.MvcExtension;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Senparc.Weixin.MP.MvcExtension;
#endif

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    public partial class WeixinController : Controller
    {
        readonly Func<string> _getRandomFileName = () => DateTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);

        //public WeixinController()
        //{

        //}

        private string appId;
        private string appSecret;
        private string token;
        private string encodingAESKey;

        SenparcWeixinSetting _senparcWeixinSetting;

        public WeixinController(IOptions<SenparcWeixinSetting> senparcWeixinSetting)
        {
#if NET45
       appId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的Token设置保持一致，区分大小写。
       appSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
       EncodingAESKey = WebConfigurationManager.AppSettings["WeixinEncodingAESKey"];//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
       string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
#else
            _senparcWeixinSetting = senparcWeixinSetting.Value;
            appId = _senparcWeixinSetting.WeixinAppId;
            appSecret = _senparcWeixinSetting.WeixinAppSecret;
            token = _senparcWeixinSetting.Token;
            encodingAESKey = _senparcWeixinSetting.EncodingAESKey;
#endif
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://sdk.weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, token) + "。" +
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
        public ActionResult Post(PostModel postModel/*,[FromBody]string requestXml*/)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                return Content("参数错误！");
            }

            #region 打包 PostModel 信息

            postModel.Token = token;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = encodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = appId;//根据自己后台的设置保持一致

            #endregion

            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
            var maxRecordCount = 10;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。

#if NET45
            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, maxRecordCount);
#else

            string body = new StreamReader(Request.Body).ReadToEnd();
            byte[] requestData = Encoding.UTF8.GetBytes(body);
            Stream inputStream = new MemoryStream(requestData);

            //var inputStream = Request.Body;
            //byte[] buffer = new byte[Request.ContentLength ?? 0];
            //inputStream.Read(buffer, 0, buffer.Length);
            //var requestXmlStream = new MemoryStream(buffer);
            //var requestXml = Encoding.UTF8.GetString(buffer);


            //int bytesRead = 0;
            //while ((bytesRead = Request.Body.Read(buffer, 0, buffer.Length)) != 0)
            //{
            //    inputStream.Write(buffer, 0, bytesRead);
            //}

            //Request.Body.CopyTo(inputStream);

            var messageHandler = new CustomMessageHandler(inputStream, postModel, maxRecordCount);
#endif

            try
            {

                #region 记录 Request 日志

                var logPath = Server.GetMapPath(string.Format("~/App_Data/MP/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。

                var requestDocumentFileName = Path.Combine(logPath, string.Format("{0}_Request_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName));
                var ecryptRequestDocumentFileName = Path.Combine(logPath, string.Format("{0}_Request_Ecrypt_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName));
#if NET45
                messageHandler.RequestDocument.Save(requestDocumentFileName);
                if (messageHandler.UsingEcryptMessage)
                {
                    messageHandler.EcryptRequestDocument.Save(ecryptRequestDocumentFileName);
                }
#else
                using (FileStream fs = new FileStream(requestDocumentFileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    messageHandler.RequestDocument.Save(fs);
                }
                if (messageHandler.UsingEcryptMessage)
                {
                    using (FileStream fs = new FileStream(ecryptRequestDocumentFileName, FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        messageHandler.EcryptRequestDocument.Save(fs);
                    }
                }
#endif


                #endregion

                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;

                //执行微信处理过程
                messageHandler.Execute();

                #region 记录 Response 日志

                //测试时可开启，帮助跟踪数据

                //if (messageHandler.ResponseDocument == null)
                //{
                //    throw new Exception(messageHandler.RequestDocument.ToString());
                //}

                var responseDocumentFileName = Path.Combine(logPath, string.Format("{0}_Response_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName));
                var ecryptResponseDocumentFileName = Path.Combine(logPath, string.Format("{0}_Response_Final_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName));

                if (messageHandler.ResponseDocument != null)
                {
                    using (FileStream fs = new FileStream(responseDocumentFileName, FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        messageHandler.ResponseDocument.Save(fs);
                    }
                }

                if (messageHandler.UsingEcryptMessage && messageHandler.FinalResponseDocument != null)
                {
                    using (FileStream fs = new FileStream(ecryptResponseDocumentFileName, FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        //记录加密后的响应信息
                        messageHandler.FinalResponseDocument.Save(fs);
                    }
                }

                #endregion`

                //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
                //return new WeixinResult(messageHandler);//v0.8+
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
            }
            catch (Exception ex)
            {
                #region 异常处理
                WeixinTrace.Log("MessageHandler错误：{0}", ex.Message);


                using (var fs = new FileStream(Server.GetMapPath("~/App_Data/Error_" + _getRandomFileName() + ".txt"), FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    using (TextWriter tw = new StreamWriter(fs))
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
                        //tw.Close();
                    }
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
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            postModel.Token = token;
            postModel.EncodingAESKey = encodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = appId;//根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(Request.Body, postModel, 10);

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


#if NET45
        /// <summary>
        /// 为测试并发性能而建
        /// </summary>
        /// <returns></returns>
        public ActionResult ForTest()
        {
            //异步并发测试（提供给单元测试使用）
            DateTime begin = DateTime.Now;
            int t1, t2, t3;
            System.Threading.ThreadPool.GetAvailableThreads(out t1, out t3);
            System.Threading.ThreadPool.GetMaxThreads(out t2, out t3);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
            DateTime end = DateTime.Now;
            var thread = System.Threading.Thread.CurrentThread;
            var result = string.Format("TId:{0}\tApp:{1}\tBegin:{2:mm:ss,ffff}\tEnd:{3:mm:ss,ffff}\tTPool：{4}",
                    thread.ManagedThreadId,
                    HttpContext.ApplicationInstance.GetHashCode(),
                    begin,
                    end,
                    t2 - t1
                    );
            return Content(result);
        }
#endif
    }
}
