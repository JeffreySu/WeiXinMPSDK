using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{

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
        public ActionResult Get(string msg_signature = "", string signature = "", string timestamp = "", string nonce = "", string echostr = "")
        {
            //return Content(echostr); //返回随机字符串则表示验证通过
            var verifyUrl = QY.Signature.VerifyURL(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce,
                echostr);
            if (verifyUrl!=null)
            {
                return Content(verifyUrl); //返回解密后的随机字符串则表示验证通过
            }
            else
            {
                var msgEncrypt = QY.Signature.EncryptMsg(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce);
                return Content("签名:" + signature + "," + QY.Signature.GenarateSinature(Token, timestamp, nonce, msgEncrypt) + "。" +
                   "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }
    }
}