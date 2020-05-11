﻿/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
    
    文件名：WorkController.cs
    文件功能描述：企业号对接测试（从QYController.cs移植）
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

/*
     重要提示
     
  1. 当前 Controller 展示了有特殊自定义需求的 MessageHandler 处理方案，
     可以高度控制消息处理过程的每一个细节，
     如果仅常规项目使用，可以直接使用中间件方式，参考 startup.cs：
     app.UseMessageHandlerForWork("/WorkAsync", WorkCustomMessageHandler.GenerateMessageHandler, options => ...);

  2. 目前 Senparc.Weixin SDK 已经全面转向异步方法驱动，
     因此建议使用异步方法（如：messageHandler.ExecuteAsync()），不再推荐同步方法。

 */


//DPBMARK_FILE Work
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.AspNet.HttpUtility;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.WorkMessageHandlers;
using Senparc.Weixin.Work.Entities;
using System;
using System.IO;

namespace Senparc.Weixin.Sample.NetCore3.Controllers
{
    /// <summary>
    /// 企业号对接测试
    /// </summary>
    public class WorkController : Controller
    {
        public static readonly string Token = Config.SenparcWeixinSetting.WorkSetting.WeixinCorpToken;//与企业微信账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.WorkSetting.WeixinCorpEncodingAESKey;//与微信企业账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string CorpId = Config.SenparcWeixinSetting.WorkSetting.WeixinCorpId;//与微信企业账号后台的CorpId设置保持一致，区分大小写。


        public WorkController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），企业微信后台应用的“修改配置”的Url填写如：http://sdk.weixin.senparc.com/work
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string msg_signature = "", string timestamp = "", string nonce = "", string echostr = "")
        {
            //return Content(echostr); //返回随机字符串则表示验证通过
            var verifyUrl = Work.Signature.VerifyURL(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce, echostr);
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
        /// 微信后台验证地址（使用Post），企业微信后台应用的“修改配置”的Url填写如：http://sdk.weixin.senparc.com/work
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
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
            var messageHandler = new WorkCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);

            if (messageHandler.RequestMessage == null)
            {
                //验证不通过或接受信息有错误
            }

            try
            {
                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

                messageHandler.Execute();//执行微信处理过程（关键）

                messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

                //自动返回加密后结果
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(ServerUtility.ContentRootMapPath("~/App_Data/Work_Error_" + SystemTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.FinalResponseDocument != null && messageHandler.FinalResponseDocument.Root != null)
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
            postModel.CorpId = CorpId;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WorkCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);
            //执行微信处理过程
            messageHandler.Execute();
            //自动返回加密后结果
            return new FixWeixinBugWeixinResult(messageHandler);
        }
    }
}