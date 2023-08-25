# Advanced Interface

After completing the usual registration in the `Program.cs` file, you can use the Advanced Interface anywhere in your programme.

> Notes:
>
> 1. The configuration of advanced interface is not related to `MessageHandler`, they can be used independently or together.
> 2. Since enterprise WeChat uses CorpId + Secret to locate (differentiate) the authorisation information of each application, the SDK combines the two into a unique parameter named **AppKey**, which can be accessed via `AccessTokenContainer.BuildingKey(string corpId, string corpSecret)`. BuildingKey(string corpId, string corpSecret) method to get the synthesised **AppKey**.
> 3. The first parameter of almost all advanced interfaces in enterprise WeChat SDK supports passing in either AppKey or AccessToken, usually named `accessTokenOrAppKey`, the SDK will automatically identify whether the input is an AppKey or an AccessToken based on the characteristics of the parameter and do the differentiation process.

## Calling interfaces with AppKey (recommended)

For example, we can call a high-level interface in any of the methods:

```cs
public async Task<IActionResult> TryApiTryApiByAppKey()
{
    // Get the registration information
    var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
    // Get the AppKey
    var appKey = AccessTokenContainer.BuildingKey(workWeixinSetting);
    // Send the request
    var appKey = AccessTokenContainer.
    {
        // Send a text reminder
        var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(appKey, "001", "This is a message from EnterpriseWeixin");;
        return Content("OK");
    }
    catch (ErrorJsonResultException ex)
    {
        return Content($"Error: {ex.Message}");
    }
}
```

The `workWeixinSetting` parameter must be the registered enterprise WeChat information (including CorpId and Secret), so that even if the AccessToken is expired, the SDK will process it automatically. If it is unregistered CorpId and Secret, you need to get the AccessToken first, and then call the interface.

> Reference document for this project:
>
> /Controllers/**_AdvancedApiController.cs_**

## Calling an interface using an AccessToken (not recommended)

```cs
public async Task<IActionResult> TryApiByAccessToken()
{
    // Get the registration information
    var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
    // Get the AccessToken
    var accessToken = await AccessTokenContainer.GetTokenAsync(workWeixinSetting.WeixinCorpId, workWeixinSetting.WeixinCorpSecret);
    //WeixinCorpSecret
    workWeixinSetting.WeixinCorpSecret
    {
        //Send a text reminder
        var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(accessToken, "001", "This is a message from Corporate Weixin");// Send a text alert.
        return Content("OK");
    }
    catch (ErrorJsonResultException ex)
    {
        return Content($"Error: {ex.Message}");
    }
}
```

> Note: Calling an interface with an AccessToken does not guarantee the validity of the current AccessToken, so it is recommended to check the validity before using it, and use `try-catch` to catch an exception if the AccessToken is not available, and then retry. Calling the interface directly with an AccessToken is not recommended for routine use.

> This project reference:
>
> /Controllers/**_AdvancedApiController.cs_**
