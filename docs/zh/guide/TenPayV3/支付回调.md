# 支付回调

## 支付回调

当用户在微信端完成支付后，微信服务器会主动推送一条通知到应用服务器。这条信息只会在微信官方服务器和应用服务器之间发生，不会有用户的参与，并且附加签名校验，因此才是可信的。

> **注意：千万不能信任手机客户端完成支付的状态，并以此通知应用服务器用户已完成支付！**

以 JsApi 支付为例，当发起统一支付时，会要求填写回调地址（`TenPayV3UnifiedorderRequestData` 中的 `notifyUrl` 参数，见 **JSAPI 支付** 相关说明）。

> 注意：不同的支付方式提供回调地址的设置可能不同，如“Native 支付”，则是在微信支付的管理后台设置。

## 定义回调入口

```cs
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

        return Json(returnData);
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
