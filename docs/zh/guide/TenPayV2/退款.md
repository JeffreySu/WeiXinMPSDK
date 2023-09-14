# 退款

## 退款

退款方法核心代码如下：

```cs
/// <summary>
/// 退款申请接口
/// </summary>
/// <returns></returns>
public ActionResult Refund()
{
    try
    {
        string nonceStr = TenPayV3Util.GetNoncestr();

        string outTradeNo = HttpContext.Session.GetString("BillNo");

        string outRefundNo = "OutRefunNo-" + SystemTime.Now.Ticks;
        int totalFee = int.Parse(HttpContext.Session.GetString("BillFee"));
        int refundFee = totalFee;
        string opUserId = TenPayV3Info.MchId;
        var notifyUrl = "https://sdk.weixin.senparc.com/TenPayV3/RefundNotifyUrl";
        var dataInfo = new TenPayV3RefundRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, TenPayV3Info.Key,
            null, nonceStr, null, outTradeNo, outRefundNo, totalFee, refundFee, opUserId, null, notifyUrl: notifyUrl);

        var result = TenPayOldV3.Refund(_serviceProvider, dataInfo);//证书地址、密码，在配置文件中设置，并在注册微信支付信息时自动记录

        ViewData["Message"] = $"退款结果：{result.result_code} {result.err_code_des}。您可以刷新当前页面查看最新结果。";
        return View();
    }
    catch (Exception ex)
    {
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));

        throw;
    }
}
```

> 本项目参考文件：
>
> /Controllers/**_TenPayV3Controller.cs_**

> 说明：上述代码为了方便演示，并限定在没有登录功能的情况下只能退款本人自己支付过的订单，因此将 BillNo（订单号）存在 Session 中，实际开发过程中可放入 URL 或 Post 参数中进行请求，并注意做好权限验证！

## 退款回调

在退款接口调用过程中，有一个 `notifyUrl` 的参数，此地址用于接收微信服务器发送的退款信息回调信息。代码如下：

```cs
/// <summary>
/// 退款通知地址
/// </summary>
/// <returns></returns>
public ActionResult RefundNotifyUrl()
{
    string responseCode = "FAIL";
    string responseMsg = "FAIL";
    try
    {
        ResponseHandler resHandler = new ResponseHandler(HttpContext);

        string return_code = resHandler.GetParameter("return_code");
        string return_msg = resHandler.GetParameter("return_msg");

        WeixinTrace.SendCustomLog("跟踪RefundNotifyUrl信息", resHandler.ParseXML());

        if (return_code == "SUCCESS")
        {
            responseCode = "SUCCESS";
            responseMsg = "OK";

            string appId = resHandler.GetParameter("appid");
            string mch_id = resHandler.GetParameter("mch_id");
            string nonce_str = resHandler.GetParameter("nonce_str");
            string req_info = resHandler.GetParameter("req_info");

            if (!appId.Equals(Senparc.Weixin.Config.SenparcWeixinSetting.TenPayV3_AppId))
            {
                /*
                    * 注意：
                    * 这里添加过滤只是因为盛派Demo经常有其他公众号错误地设置了我们的地址，
                    * 导致无法正常解密，平常使用不需要过滤！
                    */
                SenparcTrace.SendCustomLog("RefundNotifyUrl 的 AppId 不正确",
                    $"appId:{appId}\r\nmch_id:{mch_id}\r\nreq_info:{req_info}");
                return Content("faild");
            }

            var decodeReqInfo = TenPayV3Util.DecodeRefundReqInfo(req_info, TenPayV3Info.Key);//解密
            var decodeDoc = XDocument.Parse(decodeReqInfo);

            //获取接口中需要用到的信息
            string transaction_id = decodeDoc.Root.Element("transaction_id").Value;
            string out_trade_no = decodeDoc.Root.Element("out_trade_no").Value;
            string refund_id = decodeDoc.Root.Element("refund_id").Value;
            string out_refund_no = decodeDoc.Root.Element("out_refund_no").Value;
            int total_fee = int.Parse(decodeDoc.Root.Element("total_fee").Value);
            int? settlement_total_fee = decodeDoc.Root.Element("settlement_total_fee") != null
                    ? int.Parse(decodeDoc.Root.Element("settlement_total_fee").Value)
                    : null as int?;
            int refund_fee = int.Parse(decodeDoc.Root.Element("refund_fee").Value);
            int tosettlement_refund_feetal_fee = int.Parse(decodeDoc.Root.Element("settlement_refund_fee").Value);
            string refund_status = decodeDoc.Root.Element("refund_status").Value;
            string success_time = decodeDoc.Root.Element("success_time").Value;
            string refund_recv_accout = decodeDoc.Root.Element("refund_recv_accout").Value;
            string refund_account = decodeDoc.Root.Element("refund_account").Value;
            string refund_request_source = decodeDoc.Root.Element("refund_request_source").Value;

            //验证通过，进行后续业务处理
        }
    }
    catch (Exception ex)
    {
        responseMsg = ex.Message;
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
    }

    string xml = string.Format(@"<xml>
<return_code><![CDATA[{0}]]></return_code>
<return_msg><![CDATA[{1}]]></return_msg>
</xml>", responseCode, responseMsg);
    return Content(xml, "text/xml");
}
```

> 本项目参考文件：
>
> /Controllers/**_TenPayV3Controller.cs_**
