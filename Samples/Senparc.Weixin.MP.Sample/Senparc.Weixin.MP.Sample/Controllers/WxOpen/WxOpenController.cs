using System;
using System.IO;
using System.Web.Configuration;
using System.Web.Mvc;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.CommonService.TemplateMessage.WxOpen;
using Senparc.Weixin.MP.Sample.CommonService.WxOpenMessageHandler;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Helpers;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.TenPay.V3;

namespace Senparc.Weixin.MP.Sample.Controllers.WxOpen
{
    /// <summary>
    /// 微信小程序Controller
    /// </summary>
    public partial class WxOpenController : Controller
    {
        public static readonly string Token = Config.SenparcWeixinSetting.WxOpenToken;//与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.WxOpenEncodingAESKey;//与微信小程序后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string WxOpenAppId = Config.SenparcWeixinSetting.WxOpenAppId;//与微信小程序账号后台的AppId设置保持一致，区分大小写。
        public static readonly string WxOpenAppSecret = Config.SenparcWeixinSetting.WxOpenAppSecret;//与微信小程序账号后台的AppId设置保持一致，区分大小写。

        readonly Func<string> _getRandomFileName = () => DateTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);


        /// <summary>
        /// GET请求用于处理微信小程序后台的URL验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信小程序后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }

            postModel.Token = Token;//根据自己后台的设置保持一致
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = WxOpenAppId;//根据自己后台的设置保持一致

            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
            var maxRecordCount = 10;

            var logPath = Server.MapPath(string.Format("~/App_Data/WxOpen/{0}/", DateTime.Now.ToString("yyyy-MM-dd")));
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomWxOpenMessageHandler(Request.InputStream, postModel, maxRecordCount);


            try
            {
                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.RequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)));
                if (messageHandler.UsingEcryptMessage)
                {
                    messageHandler.EcryptRequestDocument.Save(Path.Combine(logPath, string.Format("{0}_Request_Ecrypt_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)));
                }

                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;


                //执行微信处理过程
                messageHandler.Execute();

                //测试时可开启，帮助跟踪数据

                //if (messageHandler.ResponseDocument == null)
                //{
                //    throw new Exception(messageHandler.RequestDocument.ToString());
                //}

                if (messageHandler.ResponseDocument != null)
                {
                    messageHandler.ResponseDocument.Save(Path.Combine(logPath, string.Format("{0}_Response_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)));
                }

                if (messageHandler.UsingEcryptMessage)
                {
                    //记录加密后的响应信息
                    messageHandler.FinalResponseDocument.Save(Path.Combine(logPath, string.Format("{0}_Response_Final_{1}.txt", _getRandomFileName(), messageHandler.RequestMessage.FromUserName)));
                }

                //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
                                                                    //return new WeixinResult(messageHandler);//v0.8+
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_WxOpen_" + _getRandomFileName() + ".txt")))
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
                    tw.Close();
                }
                return Content("");
            }
        }

        [HttpPost]
        public ActionResult RequestData(string nickName)
        {
            var data = new
            {
                msg = string.Format("服务器时间：{0}，昵称：{1}", DateTime.Now, nickName)
            };
            return Json(data);
        }

        /// <summary>
        /// wx.login登陆成功之后发送的请求
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OnLogin(string code)
        {
            var jsonResult = SnsApi.JsCode2Json(WxOpenAppId, WxOpenAppSecret, code);
            if (jsonResult.errcode == ReturnCode.请求成功)
            {
                //Session["WxOpenUser"] = jsonResult;//使用Session保存登陆信息（不推荐）
                //使用SessionContainer管理登录信息（推荐）
                var unionId = "";
                var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

                //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
                return Json(new { success = true, msg = "OK", sessionId = sessionBag.Key, sessionKey = sessionBag.SessionKey });
            }
            else
            {
                return Json(new { success = false, msg = jsonResult.errmsg });
            }
        }

        [HttpPost]
        public ActionResult CheckWxOpenSignature(string sessionId, string rawData, string signature)
        {
            try
            {
                var checkSuccess = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.CheckSignature(sessionId, rawData, signature);
                return Json(new { success = checkSuccess, msg = checkSuccess ? "签名校验成功" : "签名校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DecodeEncryptedData(string type, string sessionId, string encryptedData, string iv)
        {
            DecodeEntityBase decodedEntity = null;
            switch (type.ToUpper())
            {
                case "USERINFO"://wx.getUserInfo()
                    decodedEntity = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeUserInfoBySessionId(
                        sessionId,
                        encryptedData, iv);
                    break;
                default:
                    break;
            }

            //检验水印
            var checkWartmark = false;
            if (decodedEntity != null)
            {
                checkWartmark = decodedEntity.CheckWatermark(WxOpenAppId);
            }

            //注意：此处仅为演示，敏感信息请勿传递到客户端！
            return Json(new
            {
                success = checkWartmark,
                //decodedEntity = decodedEntity,
                msg = string.Format("水印验证：{0}",
                        checkWartmark ? "通过" : "不通过")
            });
        }

        [HttpPost]
        public ActionResult TemplateTest(string sessionId, string formId)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            var openId = sessionBag != null ? sessionBag.OpenId : "用户未正确登陆";

            string title = null;
            decimal price = 100;
            string productName = null;
            string orderNumber = null;

            if (formId.StartsWith("prepay_id="))
            {
                formId = formId.Replace("prepay_id=", "");
                title = "这是来自小程序支付的模板消息";

                var cacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance();
                var unifiedorderRequestData = cacheStrategy.Get<TenPayV3UnifiedorderRequestData>($"WxOpenUnifiedorderRequestData-{openId}");//获取订单请求信息缓存
                var unifedorderResult = cacheStrategy.Get<UnifiedorderResult>($"WxOpenUnifiedorderResultData-{openId}");//获取订单信息缓存

                if (unifedorderResult != null && formId == unifedorderResult.prepay_id)
                {
                    price = unifiedorderRequestData.TotalFee;
                    productName = unifiedorderRequestData.Body + "/缓存获取 prepay_id 成功";
                    orderNumber = unifiedorderRequestData.OutTradeNo;
                }
                else
                {
                    productName = "缓存获取 prepay_id 失败";
                    orderNumber = "1234567890";
                }
                productName += " | 注意：这条消息是从小程序发起的！仅作为UI上支付成功的演示！不能确定支付真实成功！ | prepay_id：" + unifedorderResult.prepay_id;
            }
            else
            {
                title = "在线购买（小程序Demo测试）";
                productName = "商品名称-模板消息测试";
                orderNumber = "9876543210";
            }

            var data = new WxOpenTemplateMessage_PaySuccessNotice(title, DateTime.Now, productName, orderNumber, price,
                            "400-031-8816", "https://sdk.senparc.weixin.com");

            try
            {
                Senparc.Weixin.WxOpen.AdvancedAPIs
                    .Template.TemplateApi
                    .SendTemplateMessage(
                        WxOpenAppId, openId, data.TemplateId, data, formId, "pages/index/index", "图书", "#fff00");

                return Json(new { success = true, msg = "发送成功，请返回消息列表中的【服务通知】查看模板消息。\r\n点击模板消息还可重新回到小程序内。" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, openId = openId, formId = formId, msg = ex.Message });
            }
        }

        public ActionResult DecryptPhoneNumber(string sessionId, string encryptedData, string iv)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            try
            {
                var phoneNumber = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecryptPhoneNumber(sessionId, encryptedData,
               iv);

                //throw new WeixinException("解密PhoneNumber异常测试");//启用这一句，查看客户端返回的异常信息

                return Json(new { success = true, phoneNumber = phoneNumber });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });

            }
        }

        public ActionResult GetPrepayid(string sessionId)
        {
            try
            {
                var sessionBag = SessionContainer.GetSession(sessionId);
                var openId = sessionBag.OpenId;


                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                var sp_billno = string.Format("{0}{1}{2}", Config.SenparcWeixinSetting.TenPayV3_MchId /*10位*/, DateTime.Now.ToString("yyyyMMddHHmmss"),
                        TenPayV3Util.BuildRandomStr(6));

                var timeStamp = TenPayV3Util.GetTimestamp();
                var nonceStr = TenPayV3Util.GetNoncestr();

                var body = "小程序微信支付Demo";
                var price = 1;//单位：分
                var xmlDataInfo = new TenPayV3UnifiedorderRequestData(WxOpenAppId, Config.SenparcWeixinSetting.TenPayV3_MchId, body, sp_billno,
                    price, Request.UserHostAddress, Config.SenparcWeixinSetting.TenPayV3_WxOpenTenpayNotify, TenPay.TenPayV3Type.JSAPI, openId, Config.SenparcWeixinSetting.TenPayV3_Key, nonceStr);

                var result = TenPayV3.Unifiedorder(xmlDataInfo);//调用统一订单接口

                WeixinTrace.SendCustomLog("统一订单接口调用结束", "请求：" + xmlDataInfo.ToJson() + "\r\n\r\n返回结果：" + result.ToJson());

                var packageStr = "prepay_id=" + result.prepay_id;

                //记录到缓存

                var cacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance();
                cacheStrategy.Set($"WxOpenUnifiedorderRequestData-{openId}", xmlDataInfo, TimeSpan.FromDays(4));//3天内可以发送模板消息
                cacheStrategy.Set($"WxOpenUnifiedorderResultData-{openId}", result, TimeSpan.FromDays(4));//3天内可以发送模板消息

                return Json(new
                {
                    success = true,
                    prepay_id = result.prepay_id,
                    appId = Config.SenparcWeixinSetting.WxOpenAppId,
                    timeStamp,
                    nonceStr,
                    package = packageStr,
                    //signType = "MD5",
                    paySign = TenPayV3.GetJsPaySign(WxOpenAppId, timeStamp, nonceStr, packageStr, Config.SenparcWeixinSetting.TenPayV3_Key)
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    msg = ex.Message
                });
            }

        }
    }
}