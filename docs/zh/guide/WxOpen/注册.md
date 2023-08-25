# 注册

## 全局注册

所有的 Senparc.Weixin SDK 注册过程都是类似的。

首先，完成所有 Senparc.Weixin SDK 的整体注册代码。在 Program.cs 中加入以下代码：

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-dev-register-01.png)

说明：

1. `builder.Services.AddMemoryCache()` Senparc.Weixin 支持本机缓存、Redis、Memcached 等多种缓存策略，默认使用本机缓存，此时需要激活本地缓存。
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` 用于完成 Senparc.Weixin 的注册。
3. `app.UseSenparcWeixin()` 方法用于配置和启用 Senparc.Weixin。

以上代码对于所有的 Senparc.Weixin 下级模块都是相同的，只需要 3 句代码。

> 本项目参考文件：
>
> /**_Program.cs_**

## 公众号注册

在上述代码中的第 17 行委托方法中插入代码，即可完成默认公众号的注册：

```cs
register.RegisterWxOpenAccount(weixinSetting, "【盛派网络小助手】小程序");
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-dev-register-02.png)

其中，`weixinSetting` 的值默认来自于 `appsettings.json`：

```json
  "SenparcWeixinSetting": {
    "IsDebug": true,

    //小程序
    "WxOpenAppId": "#{WxOpenAppId}#",
    "WxOpenAppSecret": "#{WxOpenAppSecret}#",
    "WxOpenToken": "#{WxOpenToken}#",
    "WxOpenEncodingAESKey": "#{WxOpenEncodingAESKey}#"
  }
```

![配置参数](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-dev-register-03.png)

其中，`WxOpenToken`、`WxOpenEncodingAESKey`、`WxOpenAppId` 和 `WxOpenAppSecret` 对应了微信公众号后台的配置参数。

> 本项目参考文件：
>
> /**_appsettings.json_**

配置完成。

> 提示：自动注册的信息可通过 `Senparc.Weixin.Config.SenparcWeixinSetting` 获取。
