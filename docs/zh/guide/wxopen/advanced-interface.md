# 高级接口

完成 `Program.cs` 文件中的常规注册后，即可在程序的任意地方使用高级接口。

> 注意：
> 1、高级接口的配置和 `MessageHandler` 没有关联，两者可以独立或配合使用。
> 2、SDK 内几乎所有高级接口的第一个参数同时支持传入 AppId 或 AccessToken，通常名称为 `appIdOrAccessToken`，SDK 会根据参数特征自动识别输入的是 AppId 还是 AccessToken，并做区分处理。

## 使用 AppId 调用接口（推荐）

例如，我们可以在任意一个方法中调用一个高级接口：

```cs
using Senparc.Weixin.WxOpen.AdvancedAPIs;

var appId = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenAppId;
var openId = "xxx";
var content = "这是一条客服消息";
var result = await CustomApi.SendTextAsync(appId, openId, content);//发送客服消息
```

> appId 参数，必须是已经经过注册的，这样即使 AccessToken 过期，SDK 也会全自动处理。如果是未经过注册的 appId，则需要先获取 AccessToken，然后调用接口。

## 使用 AccessToken 调用接口（不推荐）

```cs
var accessToken = Senparc.Weixin.MP.CommonApi.GetTokenAsync(appId, appSecret);//获取 AccessToken
var openId = "xxx";
var content = "这是一条客服消息";
var result = await CustomApi.SendTextAsync(accessToken, openId, content);//发送客服消息
```

> 注意：
> 1、使用 AccessToken 方式调用接口，无法保证当前 AccessToken 的有效性，因此建议使用前进行有效性校验，并使用 `try-catch` 方式捕获 AccessToken 不可用的异常，然后进行重试。因此直接使用 AccessToken 调用接口的方式并不推荐在常规情况下使用。
> 2、小程序和公众号使用相同的 AccessToken 获取接口，因此，此处调用了 `Senparc.Weixin.MP` 类库中公众号的相同方法。因为 `Senparc.Weixin.WxOpen` 模块默认已经依赖了 `Senparc.Weixin.MP`，所以不必再手动安装 MP 模块。
