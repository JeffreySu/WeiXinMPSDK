# 注册

## 全局注册

所有的 Senparc.Weixin SDK 注册过程都是类似的。

首先，完成所有 Senparc.Weixin SDK 的整体注册代码。在 Program.cs 中加入以下代码：

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-register-01.png)

说明：

1. `builder.Services.AddMemoryCache()` Senparc.Weixin 支持本机缓存、Redis、Memcached 等多种缓存策略，默认使用本机缓存，此时需要激活本地缓存。
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` 用于完成 Senparc.Weixin 的注册。
3. `app.UseSenparcWeixin()` 方法用于配置和启用 Senparc.Weixin。

以上代码对于所有的 Senparc.Weixin 下级模块都是相同的，只需要 3 句代码。

> 本项目参考文件：
>
> /Program.cs

## 公众号注册

在上述代码中的第 27 行委托方法中插入代码，即可完成默认公众号以及微信支付的注册：

```cs
    //注册公众号信息（可以执行多次，注册多个公众号）
    register.RegisterMpAccount(weixinSetting, "【盛派网络小助手】公众号");
    //注册微信支付（可以执行多次，注册多个微信支付）
    register.RegisterTenpayOld(weixinSetting, "【盛派网络小助手】微信支付（V2）");
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-register-02.png)注册微信公众号和微信新支付

> 注意：
>
> 上述代码同时注册了公众号和微信支付（V2），因为微信支付需要提供微信用户的标识（OpenId），OpenId 必须来自微信公众号、小程序等模块。如果需要在小程序内使用微信支付，则需要对应注册小程序。

其中，`weixinSetting` 的值默认来自于 `appsettings.json`：

```JSON
  "SenparcWeixinSetting": {
    "IsDebug": true,

    //公众号
    "Token": "#{Token}#",
    "EncodingAESKey": "#{EncodingAESKey}#",
    "WeixinAppId": "#{WeixinAppId}#",
    "WeixinAppSecret": "#{WeixinAppSecret}#",

    //微信支付
    //微信支付V3（旧版文档V3）
    "TenPayV3_AppId": "#{TenPayV3_AppId}#",
    "TenPayV3_AppSecret": "#{TenPayV3_AppSecret}#",
    "TenPayV3_SubAppId": "#{TenPayV3_SubAppId}#",
    "TenPayV3_SubAppSecret": "#{TenPayV3_SubAppSecret}#",
    "TenPayV3_MchId": "#{TenPayV3_MchId}#",
    "TenPayV3_SubMchId": "#{TenPayV3_SubMchId}#", //子商户，没有可留空
    "TenPayV3_Key": "#{TenPayV3_Key}#",
    /* 证书路径（APIv3 可不使用）
     * 1、物理路径如：D:\\cert\\apiclient_cert.p12
     * 2、相对路径，如：~/App_Data/cert/apiclient_cert.p12，注意：必须放在 App_Data 等受保护的目录下，避免泄露
     * 备注：证书下载地址：https://pay.weixin.qq.com/index.php/account/api_cert
     */
    "TenPayV3_CertPath": "#{TenPayV3_CertPath}#", //（V3 API 可不使用）证书路径
    "TenPayV3_CertSecret": "#{TenPayV3_CertSecret}#", //（V3 API 可不使用）支付证书密码（原始密码和 MchId 相同）
    "TenPayV3_TenpayNotify": "#{TenPayV3_TenpayNotify}#", //http://YourDomainName/TenpayV3/PayNotifyUrl
    //如果不设置TenPayV3_WxOpenTenpayNotify，默认在 TenPayV3_TenpayNotify 的值最后加上 "WxOpen"
    "TenPayV3_WxOpenTenpayNotify": "#{TenPayV3_WxOpenTenpayNotify}#" //http://YourDomainName/TenpayV3/PayNotifyUrlWxOpen
  }
```

其中，`Token`、`EncodingAESKey`、`WeixinAppId` 和 `WeixinAppSecret` 对应了微信公众号后台的配置参数。

特别说明：`TenPayV3_CertPath` 参数同时支持完整的物理路径或相对路径（相对于程序根目录以 `~/` 开头）。证书文件必须放在 App_Data 等受保护的目录下，避免泄露。

![配置参数](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-register-03.png)

> 本项目参考文件：
>
> /appsettings.json

配置完成。

> 提示：自动注册的信息可通过 `Senparc.Weixin.Config.SenparcWeixinSetting` 获取。
