# Registration

## Global Registration

The registration process for all Senparc.Weixin SDKs is similar.

First, complete the overall registration code for all Senparc.Weixin SDKs. Add the following code in Program.cs:

![Register Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/MP/images/home-dev-register-01.png)

Notes:

1. `builder.Services.AddMemoryCache()` Senparc.Weixin supports multiple cache strategies like native cache, Redis, Memcached etc. By default native cache is used, so it needs to be enabled here.

2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` is used to complete the registration of Senparc.Weixin.

3. `app.UseSenparcWeixin()` method is used to configure and enable Senparc.Weixin.

The above code is the same for all Senparc.Weixin sub-modules, and only 3 lines are needed.

> Reference file for this project:
>
> /**_Program.cs_**

## Official Account Registration

Insert the following code in the delegate method on line 17 of the above code to complete the default official account registration:

```cs
register.RegisterMpAccount(weixinSetting, "[Senparc Network Assistant]Official Account");
```

![Register WeChat Official Account](https://sdk.weixin.senparc.com/Docs/MP/images/home-dev-register-02.png "Register WeChat Official Account")

The value of `weixinSetting` comes from `appsettings.json` by default:

```json
  "SenparcWeixinSetting": {
    "IsDebug": true,

    "Token": "#{Token}#",
    "EncodingAESKey": "#{EncodingAESKey}#",
    "WeixinAppId": "#{WeixinAppId}#",
    "WeixinAppSecret": "#{WeixinAppSecret}#"
  }
```

![Configuration Parameters](https://sdk.weixin.senparc.com/Docs/MP/images/home-dev-register-03.png "Configuration Parameters")

Where `Token`, `EncodingAESKey`, `WeixinAppId` and `WeixinAppSecret` correspond to the configuration parameters in the WeChat Official Account backend.

> Reference file for this project:
>
> /**_appsettings.json_**

Configuration is complete.

> Tip: The registered information can be obtained through `Senparc.Weixin.Config.SenparcWeixinSetting`.
