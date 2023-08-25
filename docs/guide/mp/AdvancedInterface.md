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
var appId = Senparc.Weixin.Config.SenparcWeixinSetting.AppId;
var result = await Senparc.Weixin.MP.AdvancedAPIs.UserApi.GetAsync(appId);//Get follower OpenId information
```

> The appId parameter must be registered, so that even if the AccessToken has expired, the SDK will handle it automatically. If the appId is not registered, you need to get the AccessToken first, and then call the interface.

## Calling an interface with an AccessToken (not recommended)

```cs
var accessToken = Senparc.Weixin.MP.CommonApi.GetTokenAsync(appId, appSecret);//get AccessToken
var result = await Senparc.Weixin.MP.AdvancedAPIs.UserApi.GetAsync(accessToken);//get the follower OpenId information
```

> Note: Using AccessToken to call an interface does not guarantee the validity of the current AccessToken, so it is recommended to check the validity before using it, and use `try-catch` to catch an exception if the AccessToken is not available, and then retry. Therefore, calling an interface directly with an AccessToken is not recommended for general use.
