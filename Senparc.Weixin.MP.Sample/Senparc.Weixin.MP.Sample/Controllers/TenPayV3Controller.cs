using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.TenPayLibV3;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    /// <summary>
    /// 根据官方的Webforms Demo改写，所以可以看到直接Response.Write()之类的用法，实际项目中不提倡这么做。
    /// </summary>
    public class TenPayV3Controller : Controller
    {
        private static TenPayV3Info _tenPayV3Info;

        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"]];
                }
                return _tenPayV3Info;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取用户的OpenId
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOpenId()
        {
            ViewData["UrlUserInfo"] = OAuth.GetAuthorizeUrl(TenPayV3Info.AppId, "http://weixin.senparc.com/TenPayV3/JsApi", "JeffreySu", OAuthScope.snsapi_userinfo);

            return View();
        }

        public ActionResult JsApi(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var openIdResult = OAuth.GetAccessToken(TenPayV3Info.AppId, TenPayV3Info.AppSecret, code);
            if (openIdResult.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + openIdResult.errmsg);
            }

            string timeStamp = "";
            string nonceStr = "";
            string paySign = "";

            string sp_billno = Request["order_no"];
            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            if (null == sp_billno)
            {
                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(28);
            }
            else
            {
                sp_billno = Request["order_no"].ToString();
            }

            //创建支付应答对象
            RequestHandler packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();
            //packageReqHandler.SetKey(""/*TenPayV3Info.Key*/);

            timeStamp = TenPayUtil.GetTimestamp();
            nonceStr = TenPayUtil.GetNoncestr();

            //设置package订单参数
            packageReqHandler.SetParameter("appid", TenPayV3Info.AppId);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", TenPayV3Info.MchId);		  //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);                    //随机字符串
            packageReqHandler.SetParameter("body", "test");
            packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
            packageReqHandler.SetParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("spbill_create_ip", Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("notify_url", TenPayV3Info.TenPayV3Notify);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());	                    //交易类型
            packageReqHandler.SetParameter("openid", openIdResult.openid);	                    //用户的openId

            string sign = packageReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);
            packageReqHandler.SetParameter("sign", sign);	                    //签名

            string data = packageReqHandler.ParseXML();

            var result = TenPayV3.Unifiedorder(data);
            var res = XDocument.Parse(result);
            string prepayId = res.Element("xml").Element("prepay_id").Value;

            //设置支付参数
            RequestHandler paySignReqHandler = new RequestHandler(null);
            paySignReqHandler.SetParameter("appId", TenPayV3Info.AppId);
            paySignReqHandler.SetParameter("timeStamp", timeStamp);
            paySignReqHandler.SetParameter("nonceStr", nonceStr);
            paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", prepayId));
            paySignReqHandler.SetParameter("signType", "MD5");
            paySign = paySignReqHandler.CreateMd5Sign("key", TenPayV3Info.Key);

            ViewData["appId"] = TenPayV3Info.AppId;
            ViewData["timeStamp"] = timeStamp;
            ViewData["nonceStr"] = nonceStr;
            ViewData["package"] = string.Format("prepay_id={0}", prepayId);
            ViewData["paySign"] = paySign;

            return View();
        }

        public ActionResult PayNotifyUrl()
        {
            var fileStream = System.IO.File.OpenWrite(Server.MapPath("~/1.txt"));
            var bytes = Encoding.UTF8.GetBytes(Request.InputStream.ToString());
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
            return Content("");
        }
    }
}
