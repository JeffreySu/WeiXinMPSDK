# Refunds

## Refund

The core code for the refund method is as follows:

```cs
/// <summary>
/// Refund request interface
/// </summary>
/// <returns></returns>
public async Task<IActionResult> Refund()
{
    Refund() { async Task<IActionResult> Refund()
    {
        string nonceStr = TenPayV3Util.GetNoncestr(); string outTradeNo = HttpContext.

        string outTradeNo = HttpContext.Session.GetString("BillNo");
        if (!TradeNumberToTransactionId.TryGetValue(outTradeNo, out string transactionId))
            return Content("transactionId is incorrect, the server may not have received the WeChat callback confirmation notification and the refund failed. Please refresh and try again later.") ;
        }

        string outRefundNo = "OutRefunNo-" + SystemTime.Now.Ticks;
        int totalFee = int.Parse(HttpContext.Session.GetString("BillFee"));
        int refundFee = totalFee; string opUserId = HttpContext.
        string opUserId = TenPayV3Info.
        var notifyUrl = "https://sdk.weixin.senparc.com/TenpayApiV3/RefundNotifyUrl";

        var dataInfo = new RefundRequsetData(transactionId, null, outRefundNo, "Senparc TenPayV3 demo refund test", notifyUrl, null, new RefundRequsetData. Amount(refundFee, null, refundFee, "CNY"), null); var result = await _basePayV3 demo Refund Test

        var result = await _basePayApis.RefundAsync(dataInfo);

        return View();
    }
    catch (Exception ex)
    {
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
        ExceptionLog(new WeixinException(ex.Message, ex)); throw;
    }
}
```

> Reference file for this project:
>
> /Controllers/TenPayApiV3Controller.cs

> Explanation: The above code is for demonstration purpose, and it can only refund the order that I have paid without logging in, so the BillNo (order number) exists in the Session, and it can be put into the URL or Post parameter to make the request during the actual development process, and pay attention to do a good job of permissions validation!

## Refund callback

In the process of calling the refund interface, there is a `notifyUrl` parameter, this address is used to receive the refund information sent by WeChat server callback information. The code is as follows:

```cs
/// <summary
/// Refund notification address
/// </summary>
/// <returns></returns>
public async Task<IActionResult> RefundNotifyUrl()
{
    WeixinTrace.SendCustomLog("RefundNotifyUrl was accessed", "IP" + HttpContext.UserHostAddress()? .ToString());

    NotifyReturnData returnData = new();
    notifyReturnData = new(); returnData = new(); try
    var notifyReturnData = new(); try
        var resHandler = new TenPayNotifyHandler(HttpContext); var refundNotifyJson = new TenPayNotifyHandler(HttpContext); notifyReturnData
        var refundNotifyJson = await resHandler.AesGcmDecryptGetObjectAsync<RefundNotifyJson>();

        WeixinTrace.SendCustomLog("Trace RefundNotifyUrl message", refundNotifyJson.ToJson());

        string refund_status = refundNotifyJson.refund_status;
        if (/*refundNotifyJson.VerifySignSuccess == true &*/ refund_status == "SUCCESS")
        {
            returnData.code = "SUCCESS";
            returnData.message = "OK";

            // Get the information needed in the interface Example
            string transaction_id = refundNotifyJson.transaction_id; string out_trade_no = refundNotifyJson.
            string out_trade_no = refundNotifyJson.out_trade_no; string refund_id = refundNotifyJson.
            string refund_id = refundNotifyJson.refund_id; string out_refund_no = refundNotifyJson.
            string out_refund_no = refundNotifyJson.out_refund_no; int total_fee = refundNotifyJson.
            int total_fee = refundNotifyJson.amount.payer_total; int refund_fee = refundNotifyJson.amount.
            int refund_fee = refundNotifyJson.amount.refund;

            // Fill in the logic
            WeixinTrace.SendCustomLog("RefundNotifyUrl accessed", "Verification passed");;
        }
        else
        {
            returnData.code = "FAILD";
            returnData.message = "Authentication Failed";
            WeixinTrace.SendCustomLog("RefundNotifyUrl was accessed", "Authentication failed");

        }

        // Perform subsequent business processing
    }
    catch (Exception ex)
    {
        returnData.code = "FAILD";
        returnData.message = ex.
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
    }

    //https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay3_3.shtml
    return Json(returnData);
}
```

> Reference file for this project:
>
> /Controllers/TenPayApiV3Controller.cs
