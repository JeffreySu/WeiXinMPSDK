# Advanced Interface

After completing the usual registration in the `Program.cs` file, you can use the Advanced Interface anywhere in your programme.

> Notes:
>
> 1. The configuration of advanced interfaces is not related to `MessageHandler`, they can be used independently or in conjunction with each other.
>
> 2. The first parameter of almost all advanced interfaces in the SDK supports passing either AppId or AccessToken, usually named `appIdOrAccessToken`, the SDK will automatically recognise whether the input is an AppId or an AccessToken based on the characteristics of the parameter, and make a distinction between them.

## Calling interfaces with AppId (recommended)

For example, we can call a high-level interface in any method:

```cs
using Senparc.Weixin.WxOpen.AdvancedAPIs;

var appId = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenAppId;
var openId = "xxx"; var content = "This is a customer service message.
var content = "This is a customer service message.
var result = await CustomApi.SendTextAsync(appId, openId, content);// send customer service message
```

> The appId parameter must be registered, so that even if the AccessToken is expired, the SDK will process it automatically. If the appId is unregistered, you need to get the AccessToken first, and then call the interface.

## Calling an interface with an AccessToken (not recommended)

```cs
var accessToken = Senparc.Weixin.MP.CommonApi.GetTokenAsync(appId, appSecret);//get AccessToken
var openId = "xxx";
var content = "This is a customer service message";
var result = await CustomApi.SendTextAsync(accessToken, openId, content);//Send the customer service message
```

> Notes:
>
> 1. Using AccessToken to invoke an interface does not guarantee the validity of the current AccessToken, so it is recommended to check the validity before using it, and use `try-catch` to catch the exception that the AccessToken is not available, and then retry. Therefore, it is not recommended to use AccessToken to call the interface in regular cases.
> 2. Applets and public numbers use the same AccessToken fetching interface, so here we call the same method of public numbers in `Senparc.Weixin.MP` class library. Because `Senparc.Weixin.WxOpen` module already depends on `Senparc.Weixin.MP` by default, so you don't need to install MP module manually.
