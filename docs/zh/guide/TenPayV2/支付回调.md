# 支付回调

## 支付回调

当用户在微信端完成支付后，微信服务器会主动推送一条通知到应用服务器。这条信息只会在微信官方服务器和应用服务器之间发生，不会有用户的参与，并且附加签名校验，因此才是可信的。

> **注意：千万不能信任手机客户端完成支付的状态，并以此通知应用服务器用户已完成支付！**

以 JsApi 支付为例，当发起统一支付时，会要求填写回调地址（`TenPayV3UnifiedorderRequestData` 中的 `notifyUrl` 参数，见 **JSAPI 支付** 相关说明）。

> 注意：不同的支付方式提供回调地址的设置可能不同，如“Native 支付”，则是在微信支付的管理后台设置。

## 定义回调入口

```cs
/// <summary>
/// JS-SDK支付回调地址（在统一下单接口中设置notify_url）
/// </summary>
/// <returns></returns>
public ActionResult PayNotifyUrl()
{
    try
    {
        ResponseHandler resHandler = new ResponseHandler(HttpContext);

        string return_code = resHandler.GetParameter("return_code");
        string return_msg = resHandler.GetParameter("return_msg");

        string res = null;

        resHandler.SetKey(TenPayV3Info.Key);
        //验证请求是否从微信发过来（安全）
        if (resHandler.IsTenpaySign() && return_code.ToUpper() == "SUCCESS")
        {
            res = "success";//正确的订单处理
            //直到这里，才能认为交易真正成功了，可以进行数据库操作，但是别忘了返回规定格式的消息！
        }
        else
        {
            res = "wrong";//错误的订单处理
        }

        /* 这里可以进行订单处理的逻辑 */

        string xml = string.Format(@"<xml>
<return_code><![CDATA[{0}]]></return_code>
<return_msg><![CDATA[{1}]]></return_msg>
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

> 本项目参考文件：
>
> /Controllers/TenPayV3Controller.cs
