using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.QY.Entities.Request;
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
            string postData = null;

            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                postData = sr.ReadToEnd();
            }

            //get test Msg

            StringBuilder sb=new StringBuilder();
            sb.AppendFormat("{0}={1}\r\n", "msg_signature", postModel.Msg_Signature);
            sb.AppendFormat("{0}={1}\r\n", "timestamp", postModel.Timestamp);
            sb.AppendFormat("{0}={1}\r\n", "nonce", postModel.Nonce);
            sb.AppendFormat("{0}={1}\r\n", "postData", postData);

            try
            {
                string msgXml = null;
                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(Token, EncodingAESKey, CorpId);
                var result = msgCrype.DecryptMsg(postModel.Msg_Signature, postModel.Timestamp, postModel.Nonce, postData, ref msgXml);
                sb.AppendFormat("{0}={1}\r\n", "msgXml", msgXml);
            }
            catch (Exception ex)
            {
                sb.AppendFormat("{0}={1}\r\n", "Exception", ex.Message);
            }

            using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/QY/QY_" + DateTime.Now.Ticks + ".txt")))
            {
                tw.WriteLine(sb.ToString());
                tw.Flush();
                tw.Close();
            }

            return Content("");//TODO还没做完
        }
    }
}