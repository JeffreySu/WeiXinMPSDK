/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：TenpayApiV3Controller.cs
    文件功能描述：微信支付V3 Controller
    
    
    创建标识：Senparc - 20210820

    修改标识：Senparc - 20210821
    修改描述：加入账单查询/关闭账单Demo

----------------------------------------------------------------*/

/* 注意：TenpayApiV3Controller 为真正微信支付 API V3 的示例 */

//DPBMARK_FILE TenPay
using System.Collections.Concurrent;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
//DPBMARK MP
using Senparc.Weixin.Sample.TenPayV3.Filters;
using Senparc.Weixin.Sample.TenPayV3.Models;
using Senparc.Weixin.Sample.TenPayV3.Utilities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
//DPBMARK_END

namespace Senparc.Weixin.Sample.TenPayV3.Controllers
{
    public class TenPayApiV3Controller : BaseController
    {
        private static TenPayV3Info _tenPayV3Info;

        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[key];
                }
                return _tenPayV3Info;
            }
        }

        /// <summary>
        /// 用于初始化BasePayApis
        /// </summary>
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        private readonly BasePayApis _basePayApis;
        private readonly SenparcHttpClient _httpClient;

        /// <summary>
        /// trade_no 和 transaction_id 对照表
        /// TODO：可以放入缓存，设置有效时间
        /// </summary>
        public static ConcurrentDictionary<string, string> TradeNumberToTransactionId = new ConcurrentDictionary<string, string>();


        public TenPayApiV3Controller(SenparcHttpClient httpClient)
        {
            _tenpayV3Setting = Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
            _basePayApis = new BasePayApis(_tenpayV3Setting);
            this._httpClient = httpClient;
        }

        /// <summary>
        /// 获取用户的OpenId
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int productId = 0, int hc = 0)
        {
            if (productId == 0 && hc == 0)
            {
                return RedirectToAction("ProductList");
            }

            var returnUrl = string.Format("https://sdk.weixin.senparc.com/TenpayApiV3/JsApi");
            var state = string.Format("{0}|{1}", productId, hc);
            string url = null;

            url = OAuthApi.GetAuthorizeUrl(TenPayV3Info.AppId, returnUrl, state, OAuthScope.snsapi_userinfo);//   -- DPBMARK MP DPBMARK_END

            if (url.IsNullOrEmpty())
            {
                throw new Exception("此功能需要使用微信公众号，但未获取到 OAuth URL，如果此项目为自动僧城项目，请确保已经引用“公众号”");
            }

            return Redirect(url);
        }


        #region 产品展示
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        public IActionResult ProductList()
        {
            var products = ProductModel.GetFakeProductList();
            return View(products);
        }
        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="hc"></param>
        /// <returns></returns>
        public ActionResult ProductItem(int productId, int hc)
        {
            var products = ProductModel.GetFakeProductList();
            var product = products.FirstOrDefault(z => z.Id == productId);
            if (product == null || product.GetHashCode() != hc)
            {
                return Content("商品信息不存在，或非法进入！2003");
            }

            //判断是否正在微信端
            if (Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser(HttpContext))
            {
                //正在微信端，直接跳转到微信支付页面
                return RedirectToAction("JsApi", new { productId = productId, hc = hc });
            }
            else
            {
                //在PC端打开，提供二维码扫描进行支付
                return View(product);
            }
        }

        /// <summary>
        /// 使用 Native 支付
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="hc"></param>
        /// <returns></returns>
        public async Task<IActionResult> NativePayCode(int productId, int hc)
        {
            var products = ProductModel.GetFakeProductList();
            var product = products.FirstOrDefault(z => z.Id == productId);
            if (product == null || product.GetHashCode() != hc)
            {
                return Content("商品信息不存在，或非法进入！2004");
            }

            //使用 Native 支付，输出二维码并展示
            MemoryStream fileStream = null;//输出图片的URL
            var price = (int)(product.Price * 100);
            var name = product.Name + " - 微信支付 V3 - Native 支付";
            var sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                            TenPayV3Util.BuildRandomStr(6));

            var notifyUrl = TenPayV3Info.TenPayV3Notify.Replace("/TenpayApiV3/", "/TenpayApiV3/");

            TransactionsRequestData requestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, name, sp_billno, new TenpayDateTime(DateTime.Now.AddHours(1)), null, notifyUrl, null, new() { currency = "CNY", total = price }, null, null, null, null);

            BasePayApis basePayApis = new BasePayApis();
            var result = await basePayApis.NativeAsync(requestData);
            //进行安全签名验证
            if (result.VerifySignSuccess == true)
            {
                fileStream = QrCodeHelper.GerQrCodeStream(result.code_url);
            }
            else
            {
                fileStream = QrCodeHelper.GetTextImageStream("Native Pay 未能通过签名验证，无法显示二维码");
            }
            return File(fileStream, "image/png");
        }

        /// <summary>
        /// 显示支付二维码，跳转至手机详情页再进行支付
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="hc"></param>
        /// <returns></returns>
        public IActionResult ProductPayCode(int productId, int hc)
        {
            var products = ProductModel.GetFakeProductList();
            var product = products.FirstOrDefault(z => z.Id == productId);
            if (product == null || product.GetHashCode() != hc)
            {
                return Content("商品信息不存在，或非法进入！2004");
            }

            //使用 JsApi 方式支付，引导到常规的商品详情页面
            string url = $"http://sdk.weixin.senparc.com/TenpayApiV3/JsApi?productId={productId}&hc={product.GetHashCode()}&t={SystemTime.Now.Ticks}";
            var fileStream = QrCodeHelper.GerQrCodeStream(url);

            return File(fileStream, "image/png");
        }


        #endregion

        //DPBMARK MP
        #region OAuth授权
        public ActionResult OAuthCallback(string code, string state, string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return Content("您拒绝了授权！");
                }

                if (!state.Contains("|"))
                {
                    //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                    //实际上可以存任何想传递的数据，比如用户ID
                    return Content("验证失败！请从正规途径进入！1001");
                }

                //通过，用code换取access_token
                var openIdResult = OAuthApi.GetAccessToken(TenPayV3Info.AppId, TenPayV3Info.AppSecret, code);
                if (openIdResult.errcode != ReturnCode.请求成功)
                {
                    return Content("错误：" + openIdResult.errmsg);
                }

                HttpContext.Session.SetString("OpenId", openIdResult.openid);//进行登录

                //也可以使用FormsAuthentication等其他方法记录登录信息，如：
                //FormsAuthentication.SetAuthCookie(openIdResult.openid,false);

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
        #endregion

        #region JsApi支付

        //需要OAuth登录
        [CustomOAuth(null, "/TenpayApiV3/OAuthCallback")]
        public async Task<IActionResult> JsApi(int productId, int hc)
        {
            try
            {
                //获取产品信息
                var products = ProductModel.GetFakeProductList();
                var product = products.FirstOrDefault(z => z.Id == productId);
                if (product == null || product.GetHashCode() != hc)
                {
                    return Content("商品信息不存在，或非法进入！1002");
                }
                ViewData["product"] = product;

                var openId = HttpContext.Session.GetString("OpenId");

                string sp_billno = Request.Query["order_no"];//out_trade_no
                if (string.IsNullOrEmpty(sp_billno))
                {
                    //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                    sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                        TenPayV3Util.BuildRandomStr(6));

                    //注意：以上订单号仅作为演示使用，如果访问量比较大，建议增加订单流水号的去重检查。
                }
                else
                {
                    sp_billno = Request.Query["order_no"];
                }

                //调用下单接口下单
                var name = product == null ? "test" : product.Name;
                var price = product == null ? 100 : (int)(product.Price * 100);//单位：分
                var notifyUrl = TenPayV3Info.TenPayV3Notify;

                //请求信息
                TransactionsRequestData jsApiRequestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, name + " - 微信支付 V3", sp_billno, new TenpayDateTime(DateTime.Now.AddHours(1), false), null, notifyUrl, null, new() { currency = "CNY", total = price }, new(openId), null, null, null);

                //请求接口
                var basePayApis2 = new Senparc.Weixin.TenPayV3.TenPayHttpClient.BasePayApis2(_httpClient, _tenpayV3Setting);
                var result = await basePayApis2.JsApiAsync(jsApiRequestData);

                if (result.VerifySignSuccess != true)
                {
                    throw new WeixinException("获取 prepay_id 结果校验出错！");
                }

                //获取 UI 信息包
                var jsApiUiPackage = TenPaySignHelper.GetJsApiUiPackage(TenPayV3Info.AppId, result.prepay_id);
                ViewData["jsApiUiPackage"] = jsApiUiPackage;

                //临时记录订单信息，留给退款申请接口测试使用（分布式情况下请注意数据同步）
                HttpContext.Session.SetString("BillNo", sp_billno);
                HttpContext.Session.SetString("BillFee", price.ToString());

                return View();
            }
            catch (Exception ex)
            {
                Senparc.Weixin.WeixinTrace.BaseExceptionLog(ex);
                throw;
            }
        }


        /// <summary>
        /// JS-SDK支付回调地址（在下单接口中设置的 notify_url）
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PayNotifyUrl()
        {
            try
            {
                //获取微信服务器异步发送的支付通知信息
                var resHandler = new TenPayNotifyHandler(HttpContext);
                var orderReturnJson = await resHandler.AesGcmDecryptGetObjectAsync<OrderReturnJson>();

                //记录日志
                Senparc.Weixin.WeixinTrace.SendCustomLog("PayNotifyUrl 接收到消息", orderReturnJson.ToJson(true));

                //演示记录 transaction_id，实际开发中需要记录到数据库，以便退款和后续跟踪
                TradeNumberToTransactionId[orderReturnJson.out_trade_no] = orderReturnJson.transaction_id;

                //获取支付状态
                string trade_state = orderReturnJson.trade_state;

                //验证请求是否从微信发过来（安全）
                NotifyReturnData returnData = new();

                //验证可靠的支付状态
                if (orderReturnJson.VerifySignSuccess == true && trade_state == "SUCCESS")
                {
                    returnData.code = "SUCCESS";//正确的订单处理
                    /* 提示：
                        * 1、直到这里，才能认为交易真正成功了，可以进行数据库操作，但是别忘了返回规定格式的消息！
                        * 2、上述判断已经具有比较高的安全性以外，还可以对访问 IP 进行判断进一步加强安全性。
                        * 3、下面演示的是发送支付成功的模板消息提示，非必须。
                        */
                }
                else
                {
                    returnData.code = "FAILD";//错误的订单处理
                    returnData.message = "验证失败";

                    //此处可以给用户发送支付失败提示等
                }

                #region 记录日志（也可以记录到数据库审计日志中）

                var logDir = ServerUtility.ContentRootMapPath(string.Format("~/App_Data/TenPayNotify/{0}", SystemTime.Now.ToString("yyyyMMdd")));
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                var logPath = Path.Combine(logDir, string.Format("{0}-{1}-{2}.txt", SystemTime.Now.ToString("yyyyMMdd"), SystemTime.Now.ToString("HHmmss"), Guid.NewGuid().ToString("n").Substring(0, 8)));

                using (var fileStream = System.IO.File.OpenWrite(logPath))
                {
                    var notifyJson = orderReturnJson.ToString();
                    await fileStream.WriteAsync(Encoding.Default.GetBytes(notifyJson), 0, Encoding.Default.GetByteCount(notifyJson));
                    fileStream.Close();

                }
                #endregion

                //https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_5.shtml
                return Json(returnData);
            }
            catch (Exception ex)
            {
                WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
                throw;
            }
        }
        #endregion  

        #region H5支付
        /// <summary>
        /// H5支付
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="hc"></param>
        /// <returns></returns>
        public async Task<IActionResult> H5Pay(int productId, int hc)
        {
            try
            {
                //获取产品信息
                var products = ProductModel.GetFakeProductList();
                var product = products.FirstOrDefault(z => z.Id == productId);
                if (product == null || product.GetHashCode() != hc)
                {
                    return Content("商品信息不存在，或非法进入！1002");
                }

                string openId = null;//此时在外部浏览器，无法或得到OpenId

                string sp_billno = Request.Query["order_no"];
                if (string.IsNullOrEmpty(sp_billno))
                {
                    //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                    sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                        TenPayV3Util.BuildRandomStr(6));
                }
                else
                {
                    sp_billno = Request.Query["order_no"];
                }

                var timeStamp = TenPayV3Util.GetTimestamp();
                var nonceStr = TenPayV3Util.GetNoncestr();

                var body = product == null ? "test" : product.Name;
                var price = product == null ? 100 : (int)(product.Price * 100);

                var notifyUrl = TenPayV3Info.TenPayV3Notify.Replace("/TenpayApiV3/", "/TenpayApiV3/");

                TransactionsRequestData.Scene_Info sence_info = new(HttpContext.UserHostAddress()?.ToString(), null, null, new("Wap", null, null, null, null));

                TransactionsRequestData requestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, body, sp_billno, new TenpayDateTime(DateTime.Now.AddHours(1), false), null, notifyUrl, null, new() { currency = "CNY", total = price }, new(openId), null, null, sence_info);

                WeixinTrace.SendCustomLog("H5Pay接口请求", requestData.ToJson());

                var result = await _basePayApis.H5Async(requestData);

                WeixinTrace.SendCustomLog("H5Pay接口返回", result.ToJson());

                if (result.VerifySignSuccess != true)
                {
                    return Content("未通过验证，请检查数据有效性！");
                }

                //直接跳转
                return Redirect(result.h5_url);
            }
            catch (Exception ex)
            {
                WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
                throw;
            }
        }

        public ActionResult H5PaySuccess(int productId, int hc)
        {
            try
            {
                //TODO：这里可以校验支付是否真的已经成功

                //获取产品信息
                var products = ProductModel.GetFakeProductList();
                var product = products.FirstOrDefault(z => z.Id == productId);
                if (product == null || product.GetHashCode() != hc)
                {
                    return Content("商品信息不存在，或非法进入！1002");
                }
                ViewData["product"] = product;

                return View();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                msg += "<br>" + ex.StackTrace;
                msg += "<br>==Source==<br>" + ex.Source;

                if (ex.InnerException != null)
                {
                    msg += "<br>===InnerException===<br>" + ex.InnerException.Message;
                }
                return Content(msg);
            }
        }

        #endregion
        //DPBMARK_END


        #region 订单及退款

        /// <summary>
        /// 退款申请接口
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Refund()
        {
            try
            {
                WeixinTrace.SendCustomLog("进入退款流程", "1");

                string nonceStr = TenPayV3Util.GetNoncestr();

                string outTradeNo = HttpContext.Session.GetString("BillNo");
                if (!TradeNumberToTransactionId.TryGetValue(outTradeNo, out string transactionId))
                {
                    return Content("transactionId 不正确，可能是服务器还没有收到微信回调确认通知，退款失败。请稍后刷新再试。");
                }

                WeixinTrace.SendCustomLog("进入退款流程", "2 outTradeNo：" + outTradeNo + ",transactionId：" + transactionId);

                string outRefundNo = "OutRefunNo-" + SystemTime.Now.Ticks;
                int totalFee = int.Parse(HttpContext.Session.GetString("BillFee"));
                int refundFee = totalFee;
                string opUserId = TenPayV3Info.MchId;
                var notifyUrl = "https://sdk.weixin.senparc.com/TenpayApiV3/RefundNotifyUrl";
                //var dataInfo = new TenPayV3RefundRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, TenPayV3Info.Key,
                //    null, nonceStr, null, outTradeNo, outRefundNo, totalFee, refundFee, opUserId, null, notifyUrl: notifyUrl);
                //TODO:该接口参数二选一传入
                //var dataInfo = new RefundRequsetData(transactionId, null, outRefundNo, "Senparc TenPayV3 demo退款测试", notifyUrl, null, new RefundRequsetData.Amount(refundFee, null, refundFee, "CNY"), null);
                var dataInfo = new RefundRequestData(transactionId, null, outRefundNo, "Senparc TenPayV3 demo退款测试", notifyUrl, null, new RefundRequestData.Amount(refundFee, null, refundFee, "CNY"), null);

                //#region 新方法（Senparc.Weixin v6.4.4+）
                //var result = TenPayOldV3.Refund(_serviceProvider, dataInfo);//证书地址、密码，在配置文件中设置，并在注册微信支付信息时自动记录
                //#endregion
                //var result = await _basePayApis.RefundAsync(dataInfo);

                var result = await _basePayApis.RefundAsync(dataInfo);

                WeixinTrace.SendCustomLog("进入退款流程", "3 Result：" + result.ToJson());
                ViewData["Message"] = $"退款结果：{result.status} {result.ResultCode}。您可以刷新当前页面查看最新结果。";
                return View();
                //return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));

                throw;
            }
        }

        /// <summary>
        /// 退款通知地址
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RefundNotifyUrl()
        {
            WeixinTrace.SendCustomLog("RefundNotifyUrl被访问", "IP" + HttpContext.UserHostAddress()?.ToString());

            NotifyReturnData returnData = new();
            try
            {
                var resHandler = new TenPayNotifyHandler(HttpContext);
                var refundNotifyJson = await resHandler.AesGcmDecryptGetObjectAsync<RefundNotifyJson>();

                WeixinTrace.SendCustomLog("跟踪RefundNotifyUrl信息", refundNotifyJson.ToJson());

                string refund_status = refundNotifyJson.refund_status;
                if (/*refundNotifyJson.VerifySignSuccess == true &*/ refund_status == "SUCCESS")
                {
                    returnData.code = "SUCCESS";
                    returnData.message = "OK";

                    //获取接口中需要用到的信息 例
                    string transaction_id = refundNotifyJson.transaction_id;
                    string out_trade_no = refundNotifyJson.out_trade_no;
                    string refund_id = refundNotifyJson.refund_id;
                    string out_refund_no = refundNotifyJson.out_refund_no;
                    int total_fee = refundNotifyJson.amount.payer_total;
                    int refund_fee = refundNotifyJson.amount.refund;

                    //填写逻辑
                    WeixinTrace.SendCustomLog("RefundNotifyUrl被访问", "验证通过");
                }
                else
                {
                    returnData.code = "FAILD";
                    returnData.message = "验证失败";
                    WeixinTrace.SendCustomLog("RefundNotifyUrl被访问", "验证失败");

                }

                //进行后续业务处理
            }
            catch (Exception ex)
            {
                returnData.code = "FAILD";
                returnData.message = ex.Message;
                WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
            }

            //https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay3_3.shtml
            return Json(returnData);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OrderQuery(string out_trade_no = null, string transaction_id = null)
        {
            //out_trade_no transaction_id 两个参数不能都为空
            if (out_trade_no is null && transaction_id is null)
            {
                throw new ArgumentNullException(nameof(out_trade_no) + " or " + nameof(transaction_id));
            }

            OrderReturnJson result = null;

            //选择方式查询订单
            if (out_trade_no is not null)
            {
                //result = await _basePayApis.OrderQueryByOutTradeNoAsync(out_trade_no, TenPayV3Info.MchId);

                var dataInfo = new QueryRequestData(TenPayV3Info.MchId, out_trade_no);
                result = await _basePayApis.OrderQueryByOutTradeNoAsync(dataInfo);
            }
            if (transaction_id is not null)
            {
                //result = await _basePayApis.OrderQueryByTransactionIdAsync(transaction_id, TenPayV3Info.MchId);

                var dataInfo = new QueryRequestData(TenPayV3Info.MchId, transaction_id);
                result = await _basePayApis.OrderQueryByTransactionIdAsync(dataInfo);
            }

            return Json(result);
        }

        /// <summary>
        /// 关闭订单接口
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CloseOrder(string out_trade_no)
        {

            //out_trade_no transaction_id 两个参数不能都为空
            if (out_trade_no is null)
            {
                throw new ArgumentNullException(nameof(out_trade_no));
            }

            ReturnJsonBase result = null;
            //result = await _basePayApis.CloseOrderAsync(out_trade_no, TenPayV3Info.MchId);

            var dataInfo = new CloseRequestData(TenPayV3Info.MchId, out_trade_no);
            result = await _basePayApis.CloseOrderAsync(dataInfo);

            return Json(result);
        }
        #endregion

        #region 交易账单

        /// <summary>
        /// 申请交易账单
        /// </summary>
        /// <param name="date">日期，格式如：2021-08-27</param>
        /// <returns></returns>
        public async Task<IActionResult> TradeBill(string date)
        {
            if (!Request.IsLocal())
            {
                return Content("出于安全考虑，此操作限定在本机上操作（实际项目可以添加登录权限限制后远程操作）！");
            }


            var filePath = $"{date}-TradeBill.csv";
            Console.WriteLine("FilePath:" + filePath);
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BasePayApis basePayApis = new BasePayApis();

                //var result = basePayApis.TradeBillQueryAsync(date, fileStream: fs).GetAwaiter().GetResult();

                var dataInfo = new TradeBillQueryRequestData(date);
                var result = await _basePayApis.TradeBillQueryAsync(dataInfo, fs);

                fs.Flush();

            }
            return Content("已经下载倒指定目录，文件名：" + filePath);
        }

        /// <summary>
        /// 申请资金账单接口
        /// </summary>
        /// <param name="date">日期，格式如：2021-08-27</param>
        /// <returns></returns>
        public async Task<IActionResult> FundflowBill(string date)
        {
            if (!Request.IsLocal())
            {
                return Content("出于安全考虑，此操作限定在本机上操作（实际项目可以添加登录权限限制后远程操作）！");
            }

            var filePath = $"{date}-FundflowBill.csv";
            Console.WriteLine("FilePath:" + filePath);
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BasePayApis basePayApis = new BasePayApis();

                //var result = await _basePayApis.FundflowBillQueryAsync(date, fs);

                var dataInfo = new FundflowBillQueryRequestData(date);
                var result = await _basePayApis.FundflowBillQueryAsync(dataInfo, fs);

                fs.Flush();
            }
            return Content("已经下载倒指定目录，文件名：" + filePath);
        }

        #endregion

        #region 分账
        /// <summary>
        /// 添加分账接收方接口
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddProfitsharingReceiver()
        {
            var profitsharingApis = new ProfitsharingApis(_tenpayV3Setting);
            await profitsharingApis.AddProfitsharingReceiverAsync(new Weixin.TenPayV3.Apis.Profitsharing.AddProfitsharingReceiverRequestData()
            {
                account = "1572122941",
                appid = _tenpayV3Setting.TenPayV3_AppId,
                name = "杭州掌流信息技术有限公司",
                sub_mchid = "1632369346",
                relation_type = "PARTNER",
                type = "MERCHANT_ID"
            });
            return Content("执行成功");
        }
        #endregion
    }
}
