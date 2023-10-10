# 高级接口

完成 `Program.cs` 文件中的常规注册后，即可在程序的任意地方使用高级接口。

> 注意：
> 1、高级接口的配置和 `MessageHandler` 没有关联，两者可以独立或配合使用。
> 2、由于企业微信使用 CorpId + Secret 来定位（区别）每个应用的授权信息，因此，SDK 中将两者合并成一个唯一参数，命名为 **AppKey**，可通过 `AccessTokenContainer.BuildingKey(string corpId, string corpSecret)` 方法获取合成后的 **AppKey**。
> 3、企业微信 SDK 内几乎所有高级接口的第一个参数同时支持传入 AppKey 或 AccessToken，通常名称为 `accessTokenOrAppKey`，SDK 会根据参数特征自动识别输入的是 AppKey 还是 AccessToken，并做区分处理。

## 使用 AppKey 调用接口（推荐）

例如，我们可以在任意一个方法中调用一个高级接口：

```cs
public async Task<IActionResult> TryApiTryApiByAppKey()
{
    // 获取注册信息
    var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
    // 获取 AppKey
    var appKey = AccessTokenContainer.BuildingKey(workWeixinSetting);
    //发送请求
    try
    {
        //发送文字提醒
        var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(appKey, "001", "这是一条来企业微信的消息");
        return Content("OK");
    }
    catch (ErrorJsonResultException ex)
    {
        return Content($"出错啦：{ex.Message}");
    }
}
```

> `workWeixinSetting` 参数，必须是已经经过注册的企业微信信息（内部包括 CorpId 和 Secret），这样即使 AccessToken 过期，SDK 也会全自动处理。如果是未经过注册的 CorpId 和 Secret，则需要先获取 AccessToken，然后调用接口。

> 本项目参考文件：
>
> /Controllers/**_AdvancedApiController.cs_**

## 使用 AccessToken 调用接口（不推荐）

```cs
public async Task<IActionResult> TryApiByAccessToken()
{
    // 获取注册信息
    var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
    // 获取 AccessToken
    var accessToken = await AccessTokenContainer.GetTokenAsync(workWeixinSetting.WeixinCorpId, workWeixinSetting.WeixinCorpSecret);
    //发送请求
    try
    {
        //发送文字提醒
        var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(accessToken, "001", "这是一条来企业微信的消息");
        return Content("OK");
    }
    catch (ErrorJsonResultException ex)
    {
        return Content($"出错啦：{ex.Message}");
    }
}
```

> 注意：使用 AccessToken 方式调用接口，无法保证当前 AccessToken 的有效性，因此建议使用前进行有效性校验，并使用 `try-catch` 方式捕获 AccessToken 不可用的异常，然后进行重试。因此直接使用 AccessToken 调用接口的方式并不推荐在常规情况下使用。

> 本项目参考文件：
>
> /Controllers/**_AdvancedApiController.cs_**
