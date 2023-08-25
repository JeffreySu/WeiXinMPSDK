# Registration

## Global Registration

The registration process is similar for all Senparc.Weixin SDKs.

First, complete the overall registration code for all Senparc.Weixin SDKs. Add the following code to Program.cs:

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-dev-register-01.png)

Instructions:

1. `builder.Services.AddMemoryCache()` Senparc.Weixin supports various caching strategies such as local cache, Redis, Memcached, etc. Local cache is used by default.
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` is used to complete the registration of Senparc.
3. `app.UseSenparcWeixin()` method is used to configure and enable Senparc.

The above code is the same for all Senparc.Weixin descendant modules, only 3 lines of code are needed.

> Reference file for this project:
>
> /**_Program.cs_**

## Public Registration

Insert code into the delegate method in line 17 of the above code to complete the registration of the default public number:

```cs
register.RegisterWxOpenAccount(weixinSetting, "[Shengpai Networks Little Helper] applet");
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-dev-register-02.png)

where the value of `weixinSetting` comes from `appsettings.json` by default:

```js

  "SenparcWeixinSetting": {
    "IsDebug": true,

	// Applet.
	"WxOpenAppId": "#{WxOpenAppId}#",
	"WxOpenAppSecret": "#{WxOpenAppSecret}#",
	"WxOpenToken": "#{WxOpenToken}#",
	"WxOpenEncodingAESKey": "#{WxOpenEncodingAESKey}#"
  }
```

![配置参数](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-dev-register-03.png)

Where `WxOpenToken`, `WxOpenEncodingAESKey`, `WxOpenAppId` and `WxOpenAppSecret` correspond to the configuration parameters of the WeChat public backend.

> Reference document for this project:
>
> /**_appsettings.json_**

Configuration is complete.

> Tip: The information of auto-registration can be obtained by `Senparc.Weixin.Config.SenparcWeixinSetting`.
