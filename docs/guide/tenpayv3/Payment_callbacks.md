# Payment callbacks

## Payment Callback

When a user completes a payment on the WeChat side, the WeChat server will actively push a notification to the application server. This message will only occur between the official WeChat server and the application server, without the user's involvement, and with additional signature checking so that it is trusted.

> **Note: Never trust the status of the mobile client completing the payment and using this to notify the app server that the user has completed the payment!**

Taking JsApi payment as an example, when initiating a unified payment, the callback address will be requested (`notifyUrl` parameter in `TenPayV3UnifiedorderRequestData`, see **JSAPI Payment** related note).

> Note: The settings of the callback address may be different for different payment methods, such as "Native Payment", which is set in the admin background of WeChat Payment.

## Define the callback entry

```cs
/// <summary
/// JS-SDK payment callback address (notify_url set in the order interface).
/// </summary>
/// <returns></returns>
public async Task<IActionResult> PayNotifyUrl()
{
    async Task<IActionResult> PayNotifyUrl()
    {
        /// Get the payment notification message sent asynchronously by the WeChat server.
        var resHandler = new TenPayNotifyHandler(HttpContext); var orderReturnJson = await resHandler.
        var orderReturnJson = await resHandler.AesGcmDecryptGetObjectAsync<OrderReturnJson>();

        //Get the payment status
        string trade_state = orderReturnJson.trade_state;

        //Verify that the request was sent from WeChat (secure)
        NotifyReturnData returnData = new();

        //Verify reliable payment state
        if (orderReturnJson.VerifySignSuccess == true && trade_state == "SUCCESS")
        {
            returnData.code = "SUCCESS"; // correct order processing
            /* Tip:
             * 1. Until here, the transaction can be considered truly successful and database operations can be performed, but don't forget to return a message in the specified format!
             * 2, the above judgement already has a relatively high level of security, but also to access the IP judgement to further strengthen the security.
             * 3, the following demonstration is to send a successful payment template message prompts, non-essential.
             */
        }
        else
        {
            returnData.code = "FAILD"; // incorrect order processing
            returnData.message = "Verification failed";

            // Here you can send the user payment failure alerts, etc.
        }

        return Json(returnData);
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
> /Controllers/**_TenPayApiV3Controller.cs_**
