/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotController.cs
    文件功能描述：企业微信智能机器人BotController
    
    
    创建标识：WangQian - 20250910
----------------------------------------------------------------*/

/*
     重要提示
     
  1. 当前 Controller 展示了有特殊自定义需求的 MessageHandler 处理方案，
     可以高度控制消息处理过程的每一个细节，
     如果仅常规项目使用，可以直接使用中间件方式，参考 Program.cs：
     app.UseMessageHandlerForWork("/WorkBotAsync", WorkBotCustomMessageHandler.GenerateMessageHandler, options => ...);

  2. 目前 Senparc.Weixin SDK 已经全面转向异步方法驱动，
     因此建议使用异步方法（如：messageHandler.ExecuteAsync()），不再推荐同步方法。

 */


using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.AspNet.HttpUtility;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.AspNet.MvcExtension;
using Senparc.Weixin.Sample.Work.MessageHandlers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Sample.Net8.Controllers
{
    /// <summary>
    /// 企业号对接测试
    /// </summary>
    public class WorkBotController : Controller
    {
        // 读取“企业微信机器人”的专用配置，不影响默认 WorkSetting
        private static ISenparcWeixinSettingForWork BotSetting => Senparc.Weixin.Config.SenparcWeixinSetting["企业微信机器人"];

        public static readonly string Token = BotSetting?.WeixinCorpToken ?? Config.SenparcWeixinSetting.WorkSetting.WeixinCorpToken;//与企业微信机器人后台的 Token 设置保持一致。
        public static readonly string EncodingAESKey = BotSetting?.WeixinCorpEncodingAESKey ?? Config.SenparcWeixinSetting.WorkSetting.WeixinCorpEncodingAESKey;//与企业微信机器人后台的 EncodingAESKey 设置保持一致。
        public static readonly string CorpId = "";//企业微信智能机器人的CorpId为空字符串

        public WorkBotController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），企业微信后台应用的“修改配置”的Url填写如：https://sdk.weixin.senparc.com/WorkBot
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> Get(string msg_signature = "", string timestamp = "", string nonce = "", string echostr = "")
        {
            //return Content(echostr); //返回随机字符串则表示验证通过
            var verifyUrl = Signature.VerifyURL(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce, echostr);
            if (verifyUrl != null)
            {
                return Content(verifyUrl); //返回解密后的随机字符串则表示验证通过
            }
            else
            {
                return Content("如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 微信后台验证地址（使用Post），企业微信后台应用的“修改配置”的Url填写如：https://sdk.weixin.senparc.com/WorkBot
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> Post(PostModel postModel)
        {
            var maxRecordCount = 10;

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.CorpId = CorpId;


            #region 用于生产环境测试原始数据
            //var ms = new MemoryStream();
            //Request.InputStream.CopyTo(ms);
            //ms.Seek(0, SeekOrigin.Begin);

            //var sr = new StreamReader(ms);
            //var xml = sr.ReadToEnd();
            //var doc = XDocument.Parse(xml);
            //doc.Save(ServerUtility.ContentRootMapPath("~/App_Data/TestWork.log"));
            //return null;
            #endregion

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WorkBotCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);

            if (messageHandler.RequestMessage == null)
            {
                //验证不通过或接受信息有错误
            }

            try
            {
                Senparc.Weixin.WeixinTrace.SendApiLog("企业微信 Bot 收到消息", messageHandler.RequestJsonStr);

                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

                await messageHandler.ExecuteAsync(new CancellationToken());//执行微信处理过程（关键）

                messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

                //自动返回加密后结果
                return Content(messageHandler.FinalResponseJsonStr, "application/json");
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(ServerUtility.ContentRootMapPath("~/App_Data/Work_Error_" + SystemTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (!string.IsNullOrEmpty(messageHandler.FinalResponseJsonStr))
                    {
                        tw.WriteLine(messageHandler.FinalResponseJsonStr);
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
        public async Task<ActionResult> MiniPost(PostModel postModel)
        {
            var maxRecordCount = 10;

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.CorpId = CorpId;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WorkBotCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);
            //执行微信处理过程
            await messageHandler.ExecuteAsync(new CancellationToken());
            //自动返回加密后结果
            return Content(messageHandler.FinalResponseJsonStr, "application/json");
        }
    }
}
