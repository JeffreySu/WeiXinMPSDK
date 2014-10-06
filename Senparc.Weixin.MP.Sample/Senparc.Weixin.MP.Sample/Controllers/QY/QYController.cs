using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.Sample.CommonService.QyMessageHandlers;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.Entities.Response;
using Tencent;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    /// <summary>
    /// 企业号对接测试
    /// </summary>
    public class QYController : Controller
    {
        public static readonly string Token = "fzBsmSaI8XE1OwBh";//与微信企业账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "9J8CQ7iF9mLtQDZrUM1loOVQ6oNDxVtBi1DBU2oaewl";//与微信企业账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string CorpId = "wx7618c0a6d9358622";//与微信企业账号后台的EncodingAESKey设置保持一致，区分大小写。


        public QYController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信企业后台应用的“修改配置”的Url填写如：http://weixin.senparc.com/qy
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string msg_signature = "", string timestamp = "", string nonce = "", string echostr = "")
        {
            using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/QY/QY_Get_" + DateTime.Now.Ticks + ".txt")))
            {
                tw.WriteLine(msg_signature);
                tw.Flush();
                tw.Close();
            }

            //return Content(echostr); //返回随机字符串则表示验证通过
            var verifyUrl = QY.Signature.VerifyURL(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce,
                echostr);
            if (verifyUrl != null)
            {
                return Content(verifyUrl); //返回解密后的随机字符串则表示验证通过
            }
            else
            {
                var msgEncrypt = QY.Signature.EncryptMsg(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce);
                return Content("签名:" + msg_signature + "," + QY.Signature.GenarateSinature(Token, timestamp, nonce, msgEncrypt) + "。" +
                   "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 微信后台验证地址（使用Post），微信企业后台应用的“修改配置”的Url填写如：http://weixin.senparc.com/qy
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            var maxRecordCount = 10;

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.CorpId = CorpId;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new QyCustomMessageHandler(Request.InputStream, postModel);

            try
            {
                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.RequestDocument.Save(Server.MapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_Request_" + messageHandler.RequestMessage.FromUserName + ".txt"));
                //执行微信处理过程
                messageHandler.Execute();
                //测试时可开启，帮助跟踪数据
                messageHandler.ResponseDocument.Save(Server.MapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"));

                //直接加密（后期会放到SDK中）
                var encryptResponseMessage = new EncryptResponseMessage()
               {
                   MsgSignature = postModel.Msg_Signature,
                   TimeStamp = postModel.Timestamp,
                   Nonce = postModel.Nonce,
               };

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(Token, EncodingAESKey, CorpId);
                string postMsg = null;
                msgCrype.EncryptMsg(messageHandler.ResponseDocument.ToString(), encryptResponseMessage.TimeStamp,
                    encryptResponseMessage.Nonce, ref postMsg);//TODO:这里官方的方法已经把EncryptResponseMessage对应的XML输出出来了

                encryptResponseMessage.Encrypt = postMsg;


                return Content(encryptResponseMessage.Encrypt);

            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Qy_Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content("");
            }


            //string postData = null;

            //using (StreamReader sr = new StreamReader(Request.InputStream))
            //{
            //    postData = sr.ReadToEnd();
            //}

            ////get test Msg

            //StringBuilder sb=new StringBuilder();
            //sb.AppendFormat("{0}={1}\r\n", "msg_signature", postModel.Msg_Signature);
            //sb.AppendFormat("{0}={1}\r\n", "timestamp", postModel.Timestamp);
            //sb.AppendFormat("{0}={1}\r\n", "nonce", postModel.Nonce);
            //sb.AppendFormat("{0}={1}\r\n", "postData", postData);

            //try
            //{
            //    string msgXml = null;
            //    WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(Token, EncodingAESKey, CorpId);
            //    var result = msgCrype.DecryptMsg(postModel.Msg_Signature, postModel.Timestamp, postModel.Nonce, postData, ref msgXml);
            //    sb.AppendFormat("{0}={1}\r\n", "msgXml", msgXml);
            //}
            //catch (Exception ex)
            //{
            //    sb.AppendFormat("{0}={1}\r\n", "Exception", ex.Message);
            //}

            //using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/QY/QY_" + DateTime.Now.Ticks + ".txt")))
            //{
            //    tw.WriteLine(sb.ToString());
            //    tw.Flush();
            //    tw.Close();
            //}

            //return Content("");//TODO还没做完
        }
    }
}