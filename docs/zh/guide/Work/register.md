# 注册

## 全局注册

所有的 Senparc.Weixin SDK 注册过程都是类似的。

首先，完成所有 Senparc.Weixin SDK 的整体注册代码。在 Program.cs 中加入以下代码：

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/Work/images/home-dev-register-01.png)

说明：

1. `builder.Services.AddMemoryCache()` Senparc.Weixin 支持本机缓存、Redis、Memcached 等多种缓存策略，默认使用本机缓存，此时需要激活本地缓存。
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` 用于完成 Senparc.Weixin 的注册。
3. `app.UseSenparcWeixin()` 方法用于配置和启用 Senparc.Weixin。

以上代码对于所有的 Senparc.Weixin 下级模块都是相同的，只需要 3 句代码。

> 本项目参考文件：
>
> /**_Program.cs_**

## 企业微信

在上述代码中的第 17 行委托方法中插入代码，即可完成默认公众号的注册：

```cs
register.RegisterWorkAccount(weixinSetting, "【盛派网络】企业微信");
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/Work/images/home-dev-register-02.png)

其中，`weixinSetting` 的值默认来自于 `appsettings.json`：

```JSON
"SenparcWeixinSetting": {
  //以下为 Senparc.Weixin 的 SenparcWeixinSetting 微信配置
  //注意：所有的字符串值都可能被用于字典索引，因此请勿留空字符串（但可以根据需要，删除对应的整条设置）！

  //微信全局
  "IsDebug": true,

  //以下不使用的参数可以删除，key 修改后将会失效

  //企业微信
  "WeixinCorpId": "#{WeixinCorpId}#",
  "WeixinCorpAgentId": "#{WeixinCorpAgentId}#",
  "WeixinCorpSecret": "#{WeixinCorpSecret}#",
  "WeixinCorpToken": "#{WeixinCorpToken}#",
  "WeixinCorpEncodingAESKey": "#{WeixinCorpEncodingAESKey}#"

  //可以追加更多其他平台的配置信息
}
```

其中，`WeixinCorpId` 是每个企业微信账号独有的 **corpId**，`WeixinCorpAgentId` 和 `WeixinCorpSecret` 对应每个不同应用的 **agentId** 和 **secret**， `WeixinCorpToken` 和 `WeixinCorpEncodingAESKey` 对应了当前应用消息接口的后台的配置参数。

> 本项目参考文件：
>
> /**_appsettings.json_**

配置完成。

> 提示：自动注册的信息可通过 `Senparc.Weixin.Config.SenparcWeixinSetting` 获取。
