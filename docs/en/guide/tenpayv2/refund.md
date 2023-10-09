# Refunds

## Refund

The core code for the refund method is as follows:

```cs
/// <summary>
/// Refund request interface
/// </summary>
/// <returns></returns>
public ActionResult Refund()
{
    try
    {
        string nonceStr = TenPayV3Util.GetNoncestr(); string outTradeNo = HttpContext.

        string outTradeNo = HttpContext.Session.GetString("BillNo");

        string outRefundNo = "OutRefunNo-" + SystemTime.Now.Ticks; int totalFee = int.
        int totalFee = int.Parse(HttpContext.Session.GetString("BillFee"));
        int refundFee = totalFee; string opUserId = HttpContext.
        string opUserId = TenPayV3Info.
        var notifyUrl = "https://sdk.weixin.senparc.com/TenPayV3/RefundNotifyUrl";
        var dataInfo = new TenPayV3RefundRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, TenPayV3Info.Key,
        null, nonceStr, null, outTradeNo, outRefundNo, totalFee, refundFee, opUserId, null, notifyUrl: notifyUrl);

        var result = TenPayOldV3.Refund(_serviceProvider, dataInfo);//certificate address, password, set in configuration file and automatically recorded when registering WeChat payment information

        ViewData["Message"] = $"Refund result: {result.result_code} {result.err_code_des}. You can refresh the current page to see the latest results." ;
        return View();
    }
    catch (Exception ex)
    {
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));; } catch (Exception ex) { WeixinTrace.

        ExceptionLog(new WeixinException(ex.Message, ex)); throw;
    }
}
```

> Reference file for this project:
>
> /Controllers/**_TenPayV3Controller.cs_**

> Explanation: The above code is for demonstration purpose, and it can only refund the order that I have paid without login, so BillNo (order number) is stored in the Session, which can be put into the URL or Post parameter to make request during actual development, and pay attention to do the authority verification!

## Refund callback

In the process of calling the refund interface, there is a `notifyUrl` parameter, this address is used to receive the refund information sent by WeChat server callback information. The code is as follows:

```cs
/// <summary
/// Refund notification address
/// </summary>
/// <returns></returns>
public ActionResult RefundNotifyUrl()
{
    string responseCode = "FAIL";
    string responseMsg = "FAIL";
    try
    {
        ResponseHandler resHandler = new ResponseHandler(HttpContext);

        string return_code = resHandler.GetParameter("return_code"); string return_msg = resHandler.
        string return_msg = resHandler.GetParameter("return_msg");

        WeixinTrace.SendCustomLog("Trace RefundNotifyUrl Message", resHandler.ParseXML());

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
                    * Caution:
                    * The filter is added here only because ShengPai Demo often has other public numbers wrongly set our address, which
                    * causing it to not be able to decrypt properly, there is no need to filter for normal use!
                    */
                SenparcTrace.SendCustomLog("AppId of RefundNotifyUrl is incorrect",
                    $"appId:{appId}\r\nmch_id:{mch_id}\r\nreq_info:{req_info}");

                return Content("faild");
            }

            var decodeReqInfo = TenPayV3Util.DecodeRefundReqInfo(req_info, TenPayV3Info.Key);// decode
            var decodeDoc = XDocument.Parse(decodeReqInfo);

            //Get the information needed in the interface.
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

            // Validation passed, proceed to subsequent business processing
        }
    }
    catch (Exception ex)
    {
        responseMsg = ex.Message;
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
    }

    string xml = string.Format(@"<xml>
<return_code><! [CDATA[{0}]]></return_code>
<return_msg><! [CDATA[{1}]]></return_msg>
</xml>", responseCode, responseMsg);
    return Content(xml, "text/xml");
}
```

> Reference file for this project:
>
> /Controllers/**_TenPayV3Controller.cs_**
