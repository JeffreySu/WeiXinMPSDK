
/*
     重要提示
     
  1. 当前 Controller 展示了有特殊自定义需求的 MessageHandler 处理方案，
     可以高度控制消息处理过程的每一个细节，
     如果仅常规项目使用，可以直接使用中间件方式，参考 startup.cs：
     app.UseMessageHandlerForWxOpen("/WxOpenAsync", CustomWxOpenMessageHandler.GenerateMessageHandler, options => ...);

  2. 目前 Senparc.Weixin SDK 已经全面转向异步方法驱动，
     因此建议使用异步方法（如：messageHandler.ExecuteAsync()），不再推荐同步方法。

 */

//DPBMARK_FILE MiniProgram
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.AspNet.HttpUtility;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.AspNet.MvcExtension;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Helpers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Template;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.WxOpen.Helpers;

namespace Senparc.Weixin.Sample.WxOpen.Controllers
{
    /// <summary>
    /// 微信小程序Controller
    /// </summary>
    public partial class WxOpenController : Controller
    {
        public static readonly string Token = Config.SenparcWeixinSetting.WxOpenToken;//与微信小程序后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = Config.SenparcWeixinSetting.WxOpenEncodingAESKey;//与微信小程序后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string WxOpenAppId = Config.SenparcWeixinSetting.WxOpenAppId;//与微信小程序后台的AppId设置保持一致，区分大小写。
        public static readonly string WxOpenAppSecret = Config.SenparcWeixinSetting.WxOpenAppSecret;//与微信小程序账号后台的AppId设置保持一致，区分大小写。


        readonly Func<string> _getRandomFileName = () => SystemTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);


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
            postModel.AppId = WxOpenAppId;//根据自己后台的设置保持一致（必须提供）

            //v4.2.2之后的版本，可以设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
            var maxRecordCount = 10;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomWxOpenMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);


            try
            {
                /* 如果需要添加消息去重功能，只需打开OmitRepeatedMessage功能，SDK会自动处理。
                 * 收到重复消息通常是因为微信服务器没有及时收到响应，会持续发送2-5条不等的相同内容的RequestMessage*/
                messageHandler.OmitRepeatedMessage = true;

                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

                messageHandler.Execute();//执行微信处理过程（关键）

                messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）

                //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
                return new FixWeixinBugWeixinResult(messageHandler);//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
                                                                    //return new WeixinResult(messageHandler);//v0.8+
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(ServerUtility.ContentRootMapPath("~/App_Data/Error_WxOpen_" + _getRandomFileName() + ".txt")))
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
                msg = string.Format("服务器时间：{0}，昵称：{1}", SystemTime.Now.LocalDateTime, nickName)
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
            try
            {
                var jsonResult = SnsApi.JsCode2Json(WxOpenAppId, WxOpenAppSecret, code);
                if (jsonResult.errcode == ReturnCode.请求成功)
                {
                    //Session["WxOpenUser"] = jsonResult;//使用Session保存登陆信息（不推荐）
                    //使用SessionContainer管理登录信息（推荐）
                    var unionId = "";
                    var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

                    //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
                    return Json(new { success = true, msg = "OK", sessionId = sessionBag.Key, sessionKey = sessionBag.SessionKey/* 此参数千万不能暴露给客户端！处仅作演示！ */ });
                }
                else
                {
                    return Json(new { success = false, msg = jsonResult.errmsg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 检查签名
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="rawData"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 数据解密并进行水印校验
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DecodeEncryptedData(string type, string sessionId, string encryptedData, string iv)
        {
            DecodeEntityBase decodedEntity = null;

            try
            {
                switch (type.ToUpper())
                {
                    case "USERINFO"://wx.getUserInfo()
                        decodedEntity = EncryptHelper.DecodeUserInfoBySessionId(
                            sessionId,
                            encryptedData, iv);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WeixinTrace.SendCustomLog("EncryptHelper.DecodeUserInfoBySessionId 方法出错",
                    $@"sessionId: {sessionId}
encryptedData: {encryptedData}
iv: {iv}
sessionKey: {(await SessionContainer.CheckRegisteredAsync(sessionId)
                ? (await SessionContainer.GetSessionAsync(sessionId)).SessionKey
                : "未保存sessionId")}

异常信息：
{ex.ToString()}
");
            }

            //检验水印
            var checkWatermark = false;
            if (decodedEntity != null)
            {
                checkWatermark = decodedEntity.CheckWatermark(WxOpenAppId);

                //保存用户信息（可选）
                if (checkWatermark && decodedEntity is DecodedUserInfo decodedUserInfo)
                {
                    var sessionBag = await SessionContainer.GetSessionAsync(sessionId);
                    if (sessionBag != null)
                    {
                        await SessionContainer.AddDecodedUserInfoAsync(sessionBag, decodedUserInfo);
                    }
                }
            }

            //注意：此处仅为演示，敏感信息请勿传递到客户端！
            return Json(new
            {
                success = checkWatermark,
                //decodedEntity = decodedEntity,
                msg = $"水印验证：{(checkWatermark ? "通过" : "不通过")}"
            });
        }

        /// <summary>
        /// 解密电话号码
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public ActionResult DecryptPhoneNumber(string sessionId, string encryptedData, string iv)
        {
            //var sessionBag = SessionContainer.GetSession(sessionId);
            try
            {
                var phoneNumber = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecryptPhoneNumber(sessionId, encryptedData, iv);

                //throw new WeixinException("解密PhoneNumber异常测试");//启用这一句，查看客户端返回的异常信息

                return Json(new { success = true, phoneNumber = phoneNumber });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 解密运动步数
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public ActionResult DecryptRunData(string sessionId, string encryptedData, string iv)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            try
            {
                var runData = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecryptRunData(sessionId, encryptedData, iv);

                //throw new WeixinException("解密PhoneNumber异常测试");//启用这一句，查看客户端返回的异常信息

                return Json(new { success = true, runData = runData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });

            }
        }

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetQrCode(string sessionId, string useBase64, string codeType = "1")
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                return Json(new { success = false, msg = "请先登录！" });
            }

            var ms = new MemoryStream();
            var openId = sessionBag.OpenId;
            var page = "pages/QrCode/QrCode";//此接口不可以带参数，如果需要加参数，必须加到scene中
            var scene = $"OpenIdSuffix:{openId.Substring(openId.Length - 10, 10)}#{codeType}";//储存OpenId后缀，以及codeType。scene最多允许32个字符
            LineColor lineColor = null;//线条颜色
            if (codeType == "2")
            {
                lineColor = new LineColor(221, 51, 238);
            }

            var result = await Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppApi
                .GetWxaCodeUnlimitAsync(WxOpenAppId, ms, scene, page, lineColor: lineColor);
            ms.Position = 0;

            if (!useBase64.IsNullOrEmpty())
            {
                //转base64
                var imgBase64 = Convert.ToBase64String(ms.GetBuffer());
                return Json(new { success = true, msg = imgBase64, page = page });
            }
            else
            {
                //返回文件流
                return File(ms, "image/jpeg");
            }
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public async Task<IActionResult> SubscribeMessage(string sessionId, string templateId = "xWclWkOqDrxEgWF4DExmb9yUe10pfmSSt2KM6pY7ZlU")
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                return Json(new { success = false, msg = "请先登录！" });
            }

            await Task.Delay(1000);//停1秒钟，实际开发过程中可以将权限存入数据库，任意时间发送。

            var templateMessageData = new TemplateMessageData();
            templateMessageData["thing1"] = new TemplateMessageDataValue("微信公众号+小程序快速开发");
            templateMessageData["time5"] = new TemplateMessageDataValue(SystemTime.Now.ToString("yyyy年MM月dd日 HH:mm"));
            templateMessageData["thing6"] = new TemplateMessageDataValue("盛派网络研究院");
            templateMessageData["thing7"] = new TemplateMessageDataValue("第二部分课程正在准备中，尽情期待");

            var page = "pages/index/index";
            //templateId也可以由后端指定

            try
            {
                var result = await Weixin.WxOpen.AdvancedAPIs.MessageApi.SendSubscribeAsync(WxOpenAppId, sessionBag.OpenId, templateId, templateMessageData, page);
                if (result.errcode == ReturnCode.请求成功)
                {
                    return Json(new { success = true, msg = "消息已发送，请注意查收" });
                }
                else
                {
                    return Json(new { success = false, msg = result.errmsg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        /// <summary>
        /// 下发小程序和公众号统一的服务消息
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<ActionResult> UniformSend(string sessionId)
        {
            var sessionBag = SessionContainer.GetSession(sessionId);
            if (sessionBag == null)
            {
                return Json(new { success = false, msg = "请先登录！" });
            }

            await Task.Delay(1000);//停1秒钟，实际开发过程中可以将权限存入数据库，任意时间发送。

            try
            {
                var mpAppId = Config.SenparcWeixinSetting.MpSetting.WeixinAppId;//公众号ID
                var mpTemplateId = "ur6TqESOo-32FEUk4qJxeWZZVt4KEOPjqbAFDGWw6gg";//公众号模板消息ID
                var pagePath = "pages/websocket_signalr/websocket_signalr";

                object templateData = null;

                //{"touser":"oeaTy0DgoGq-lyqvTauWVjbIVuP0","weapp_template_msg":null,"mp_template_msg":{"appid":"wx669ef95216eef885","template_id":null,"url":"https://dev.senparc.com","miniprogram":{"appid":"wx12b4f63276b14d4c","pagepath":"websocket/websocket"},"data":{"first":{"value":"小程序和公众号统一的服务消息","color":"#173177"},"keyword1":{"value":"2022/1/20 23:22:12","color":"#173177"},"keyword2":{"value":"dev.senparc.com","color":"#173177"},"keyword3":{"value":"小程序接口测试","color":"#173177"},"keyword4":{"value":"正常","color":"#173177"},"keyword5":{"value":"测试“小程序和公众号统一的服务消息”接口，服务正常","color":"#173177"},"remark":{"value":"您的 OpenId：oeaTy0DgoGq-lyqvTauWVjbIVuP0","color":"#173177"},"TemplateId":"ur6TqESOo-32FEUk4qJxeWZZVt4KEOPjqbAFDGWw6gg","Url":"https://dev.senparc.com","TemplateName":"系统异常告警通知"}}}


                #region 公众号模板消息信息       -- DPBMARK MP

                //可选参数（需要和公众号模板消息匹配）：
                templateData = templateData = new
                {
                    first = new MP.AdvancedAPIs.TemplateMessage.TemplateDataItem("小程序和公众号统一的服务消息"),
                    keyword1 = new MP.AdvancedAPIs.TemplateMessage.TemplateDataItem("dev.senparc.com"),
                    keyword2 = new MP.AdvancedAPIs.TemplateMessage.TemplateDataItem("小程序接口测试"),
                    keyword3 = new MP.AdvancedAPIs.TemplateMessage.TemplateDataItem("正常"),
                    keyword4 = new MP.AdvancedAPIs.TemplateMessage.TemplateDataItem("测试“小程序和公众号统一的服务消息”接口，服务正常"),
                    remark = new MP.AdvancedAPIs.TemplateMessage.TemplateDataItem("您的 OpenId：" + sessionBag.OpenId),
                };
                #endregion                      DPBMARK_END

                var miniprogram = new Miniprogram_PagePath(WxOpenAppId, pagePath);//使用 pagepath 参数
                //var miniprogram = new Miniprogram_Page(WxOpenAppId, pagePath);// 使用 page 参数
                //https://weixin.senparc.com/QA-17333

                UniformSendData msgData = new(
                    sessionBag.OpenId,
                    new Mp_Template_Msg(mpAppId,
                                        mpTemplateId,
                                        "https://dev.senparc.com",
                                        miniprogram,
                                        templateData)
                    );

                var result = await TemplateApi.UniformSendAsync(WxOpenAppId, msgData);

                if (result.errcode == ReturnCode.请求成功)
                {
                    return Json(new { success = true, title = "服务消息已发送，请注意查收", content = result.errmsg });
                }
                else
                {
                    string msg;

                    if (result.errmsg.Contains("require subscribe"))
                    {
                        msg = "您需要关注公众号【盛派网络小助手】才能收到公众号内的模板消息！";
                    }
                    else
                    {
                        msg = result.errmsg;
                    }

                    return Json(new { success = false, title = "出错啦！", msg = msg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }


        #region GetUrlScheme
        public async Task<IActionResult> GetUrlScheme(int tickid, string ntype = "gclub")
        {
            string message;
            ViewData["inWeChatBrowser"] = true;// Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser(HttpContext);
            try
            {
                if (!HttpContext.Request.IsLocal())
                {
                    throw new WeixinException("此接口为内部接口，请在服务器本地调用！");
                }

                var wxOpenAppId = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenSetting.WxOpenAppId;
                var jumpWxa = new Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme.GenerateSchemeJumpWxa("", "");
                var schmeResult = await Senparc.Weixin.WxOpen.AdvancedAPIs.UrlSchemeApi.GenerateSchemeAsync(wxOpenAppId, jumpWxa, false, null);
                message = schmeResult.openlink;
                ViewData["Success"] = true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            ViewData["Message"] = message;
            return View();
        }

        public IActionResult Page(string t = "E1EvMrNAEdi", string d = null)
        {
            if (d != null)
            {
                return Content($"<script>location.href = 'weixin://dl/business/?t={t}' </script>", "text/html");
            }

            ViewData["inWeChatBrowser"] = true;// Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser(HttpContext);
            return View("Page", t);
        }


        #endregion

        /// <summary>
        /// 获取用户手机号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetUserPhoneNumber(string code)
        {
            try
            {
                var result = await BusinessApi.GetUserPhoneNumberAsync(WxOpenAppId, code);
                return Json(new { success = true, phoneInfo = result.phone_info });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        public async Task<ActionResult> GetPrepayid(string sessionId)
        {
            try
            {
                var sessionBag = SessionContainer.GetSession(sessionId);
                var openId = sessionBag.OpenId;

                //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                var sp_billno = string.Format("{0}{1}{2}", Config.SenparcWeixinSetting.TenPayV3_MchId /*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                        TenPayV3Util.BuildRandomStr(6));

                var body = "小程序微信支付Demo";
                var price = 1;//单位：分
                var notifyUrl = Config.SenparcWeixinSetting.TenPayV3_WxOpenTenpayNotify;
                var basePayApis = new Senparc.Weixin.TenPayV3.Apis.BasePayApis(Config.SenparcWeixinSetting);

                var requestData = new TenPayV3.Apis.BasePay.TransactionsRequestData(WxOpenAppId, Config.SenparcWeixinSetting.TenPayV3_MchId, body, sp_billno, new TenPayV3.Entities.TenpayDateTime(SystemTime.Now.AddMinutes(120).DateTime, false), HttpContext.UserHostAddress().ToString(), notifyUrl, null, new() { currency = "CNY", total = price }, new(openId), null, null, null);
                var result = await basePayApis.JsApiAsync(requestData);

                var packageStr = "prepay_id=" + result.prepay_id;

                var jsApiUiPackage = TenPaySignHelper.GetJsApiUiPackage(WxOpenAppId, result.prepay_id);

                return Json(new
                {
                    success = true,
                    prepay_id = result.prepay_id,
                    appId = Config.SenparcWeixinSetting.WxOpenAppId,
                    timeStamp = jsApiUiPackage.Timestamp,
                    nonceStr = jsApiUiPackage.NonceStr,
                    package = packageStr,
                    signType = "RSA",
                    paySign = jsApiUiPackage.Signature
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