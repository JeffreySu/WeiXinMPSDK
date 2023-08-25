# Registration

## Global Registration

The registration process is similar for all Senparc.Weixin SDKs.

First, complete the overall registration code for all Senparc.Weixin SDKs. Add the following code to Program.cs:

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/Work/images/home-dev-register-01.png)

Instructions:

1. `builder.Services.AddMemoryCache()` Senparc.Weixin supports various caching strategies such as local cache, Redis, Memcached, etc. Local cache is used by default.
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` is used to complete the registration of Senparc.
3. `app.UseSenparcWeixin()` method is used to configure and enable Senparc.

The above code is the same for all Senparc.Weixin descendant modules, only 3 lines of code are needed.

> Reference file for this project:
>
> /**_Program.cs_**

## Enterprise Weixin

Insert code into the delegate method in line 17 of the above code to complete the registration of the default public number:

```cs
register.RegisterWorkAccount(weixinSetting, "[Shengpai Networks] Enterprise WeChat");
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/Work/images/home-dev-register-02.png)

where the value of `weixinSetting` comes from `appsettings.json` by default:`json`.

```json
"SenparcWeixinSetting": {
  // The following is the SenparcWeixinSetting microsoft configuration for Senparc.Weixin
  //Note: All string values may be used for dictionary indexing, so please do not leave empty strings (but you can remove the corresponding whole setting as needed)!

  // WeChat Global
  "IsDebug": true,

  //The following unused parameters can be deleted, and the key will be invalidated after modification

  //Corporate Weixin
  "WeixinCorpId": "#{WeixinCorpId}#",
  "WeixinCorpAgentId": "#{WeixinCorpAgentId}#",
  "WeixinCorpSecret": "#{WeixinCorpSecret}#",
  "WeixinCorpToken": "#{WeixinCorpToken}#",
  "WeixinCorpEncodingAESKey": "#{WeixinCorpEncodingAESKey}#"

  // More configuration information for other platforms can be appended
}

```

Among them, `WeixinCorpId` is the **corpId** unique to each enterprise WeChat account, `WeixinCorpAgentId` and `WeixinCorpSecret` correspond to the **agentId** and **secret** of each different application, `WeixinCorpToken` and ` WeixinCorpEncodingAESKey` correspond to the configuration parameters of the backend of the current application's messaging interface.

> Reference document for this project:
>
> /**_appsettings.json_**

Configuration complete.

> Tip: The auto-registration information can be obtained via `Senparc.Weixin.Config.SenparcWeixinSetting`.
