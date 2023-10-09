# 退款

## 退款

退款方法核心代码如下：

```cs
/// <summary>
/// 退款申请接口
/// </summary>
/// <returns></returns>
public async Task<IActionResult> Refund()
{
    try
    {
        string nonceStr = TenPayV3Util.GetNoncestr();

        string outTradeNo = HttpContext.Session.GetString("BillNo");
        if (!TradeNumberToTransactionId.TryGetValue(outTradeNo, out string transactionId))
        {
            return Content("transactionId 不正确，可能是服务器还没有收到微信回调确认通知，退款失败。请稍后刷新再试。");
        }

        string outRefundNo = "OutRefunNo-" + SystemTime.Now.Ticks;
        int totalFee = int.Parse(HttpContext.Session.GetString("BillFee"));
        int refundFee = totalFee;
        string opUserId = TenPayV3Info.MchId;
        var notifyUrl = "https://sdk.weixin.senparc.com/TenpayApiV3/RefundNotifyUrl";

        var dataInfo = new RefundRequsetData(transactionId, null, outRefundNo, "Senparc TenPayV3 demo退款测试", notifyUrl, null, new RefundRequsetData.Amount(refundFee, null, refundFee, "CNY"), null);

        var result = await _basePayApis.RefundAsync(dataInfo);

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
> /Controllers/**_TenPayApiV3Controller.cs_**

> 说明：上述代码为了方便演示，并限定在没有登录功能的情况下只能退款本人自己支付过的订单，因此将 BillNo（订单号）存在 Session 中，实际开发过程中可放入 URL 或 Post 参数中进行请求，并注意做好权限验证！

## 退款回调

在退款接口调用过程中，有一个 `notifyUrl` 的参数，此地址用于接收微信服务器发送的退款信息回调信息。代码如下：

```cs
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
```

> 本项目参考文件：
>
> /Controllers/**_TenPayApiV3Controller.cs_**
