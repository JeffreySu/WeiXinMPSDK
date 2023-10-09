# Payment callbacks

## Payment Callback

When a user completes a payment on the WeChat side, the WeChat server will actively push a notification to the application server. This message will only occur between the official WeChat server and the application server, without the user's involvement, and with additional signature checking so that it is trusted.

> **Note: Never trust the status of the mobile client completing the payment and using this to notify the app server that the user has completed the payment!**

Taking JsApi payment as an example, when initiating a unified payment, the callback address will be requested (`notifyUrl` parameter in `TenPayV3UnifiedorderRequestData`, see **JSAPI Payment** related note).

> Note: The settings of the callback address may be different for different payment methods, such as "Native Payment", which is set in WeChat Payment's admin background.

## Define the callback entry

```cs
/// <summary
/// JS-SDK payment callback address (set notify_url in Unified Order Interface)
/// </summary>
/// <returns></returns>
public ActionResult PayNotifyUrl()
{
    try
    {
        ResponseHandler resHandler = new ResponseHandler(HttpContext); string return_code = resHandler.

        string return_code = resHandler.GetParameter("return_code"); string return_msg = resHandler.
        string return_msg = resHandler.GetParameter("return_msg"); string return_msg = resHandler.

        GetParameter("return_msg"); string return_msg = resHandler.

        resHandler.SetKey(TenPayV3Info.Key);
        //Verify that the request was sent from WeChat (secure)
        if (resHandler.IsTenpaySign() && return_code.ToUpper() == "SUCCESS")
        {
            res = "SUCCESS"; //correct order processing
            // Until here, the transaction is not considered truly successful and database operations can be performed, but don't forget to return the message in the prescribed format!
        }
        else
        {
            res = "wrong";// incorrect order processing
        }

        /* Here you can do the order processing logic */

        string xml = string.Format(@"<xml>
<return_code><! [CDATA[{0}]]></return_code>
<return_msg><! [CDATA[{1}]]></return_msg>
</xml>", return_code, return_msg);
        return Content(xml, "text/xml");
    }
    catch (Exception ex)
    {
        WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
        throw;
    }
}
```

> Reference file for this project:
>
> /Controllers/**_TenPayV3Controller.cs_**
