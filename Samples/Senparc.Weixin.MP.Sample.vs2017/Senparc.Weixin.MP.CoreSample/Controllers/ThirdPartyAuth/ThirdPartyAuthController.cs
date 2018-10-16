using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.WorkMessageHandler;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using Senparc.Weixin.MP.Sample.CommonService.WorkMessageHandlers;
using Senparc.Weixin.HttpUtility;
using Senparc.CO2NET.HttpUtility;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    /// <summary>
    /// 企业号对接测试
    /// </summary>
    public class ThirdPartyAuthController : Controller
    {
        public static readonly string Token = Config.SenparcWeixinSetting.Token;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.EncodingAESKey;//与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string SuiteId = "tj8946e399c3b2936f";//与微信企业账号后台的EncodingAESKey设置保持一致，区分大小写。


        public ThirdPartyAuthController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信企业后台应用的“修改配置”的Url填写如：http://weixin.senparc.com/ThirdPartyAuth
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string msg_signature = "", string timestamp = "", string nonce = "", string echostr = "")
        {
            //return Content(echostr); //返回随机字符串则表示验证通过
            var verifyUrl = Work.Signature.VerifyURL(Token, EncodingAESKey, SuiteId, msg_signature, timestamp, nonce,
                echostr);
            if (verifyUrl != null)
            {
                var fileStream = System.IO.File.OpenWrite(Server.GetMapPath("~/1.txt"));
                fileStream.Write(Encoding.Default.GetBytes(verifyUrl), 0, Encoding.Default.GetByteCount(verifyUrl));
                fileStream.Close();
                //return Content(verifyUrl); //返回解密后的随机字符串则表示验证通过
                return Content("Success");
            }
            else
            {
                var fileStream = System.IO.File.OpenWrite(Server.GetMapPath("~/1.txt"));
                fileStream.Write(Encoding.Default.GetBytes("asd"), 0, Encoding.Default.GetByteCount("asd"));
                fileStream.Close();
                return Content("如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 微信后台验证地址（使用Post），微信企业后台应用的“修改配置”的Url填写如：http://weixin.senparc.com/ThirdPartyAuth
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            var maxRecordCount = 10;

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.CorpId = SuiteId;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WorkCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);

            if (messageHandler.RequestMessage == null)
            {
                //验证不通过或接受信息有错误
            }

            try
            {
                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.RequestDocument.Save(Server.GetMapPath("~/App_Data/Work/" + DateTime.Now.Ticks + "_Request_" + messageHandler.RequestMessage.FromUserName + ".txt"));
                //执行微信处理过程
                messageHandler.Execute();
                //测试时可开启，帮助跟踪数据

                if (!string.IsNullOrEmpty(messageHandler.TextResponseMessage))
                {
                    //messageHandler.ResponseDocument.Save(Server.GetMapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"));
                    var responseText = messageHandler.TextResponseMessage;
                    using (StreamWriter sw = new StreamWriter(Server.GetMapPath("~/App_Data/Work/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"), true))
                    {
                        sw.Write(responseText);
                    }

                    if (messageHandler.FinalResponseDocument != null)
                    {
                        //messageHandler.FinalResponseDocument.Save(Server.GetMapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_FinalResponse_" + messageHandler.ResponseMessage.ToUserName + ".txt"));
                        using (StreamWriter sw = new StreamWriter(Server.GetMapPath("~/App_Data/Work/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"), true))
                        {
                            sw.Write(messageHandler.FinalResponseDocument.ToString());
                        }
                    }
                }

                //自动返回加密后结果
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.GetMapPath("~/App_Data/Work_Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.FinalResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.FinalResponseDocument.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content("");
            }
        }

        /// <summary>
        /// 这是一个最简洁的过程演示
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MiniPost(PostModel postModel)
        {
            var maxRecordCount = 10;

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.CorpId = SuiteId;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WorkCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);
            //执行微信处理过程
            messageHandler.Execute();
            //自动返回加密后结果
            return new FixWeixinBugWeixinResult(messageHandler);
        }
    }
}