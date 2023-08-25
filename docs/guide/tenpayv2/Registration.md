# Registration

## Global Registration

The registration process is similar for all Senparc.Weixin SDKs.

First, complete the overall registration code for all Senparc.Weixin SDKs. Add the following code to Program.cs:

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-register-01.png)

Instructions:

1. `builder.Services.AddMemoryCache()` Senparc.Weixin supports various caching strategies such as local cache, Redis, Memcached, etc. Local cache is used by default, and you need to activate local cache at this time.
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` is used to complete the registration of Senparc.
3. `app.UseSenparcWeixin()` method is used to configure and enable Senparc.

The above code is the same for all Senparc.Weixin descendant modules, only 3 lines of code are needed.

> Reference file for this project:
>
> /**_Program.cs_**

## Public number registration

Inserting code into the delegate method in line 27 of the above code will complete the registration of the default public number as well as WeChat Pay:

```cs
    // Register public number information (can be executed multiple times to register multiple public numbers)
    register.RegisterMpAccount(weixinSetting, "[Shengpai Networks Little Helper] public number");;
    // Register Weixin Payment (can be executed multiple times, register multiple Weixin Payments)
    register.RegisterTenpayOld(weixinSetting, "[Shengpai Networks Little Helper] WeChat Payment (V2)");;
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-register-02.png)

> Attention:
>
> The above code registers both public number and WeChat payment (V2), because WeChat payment needs to provide the identification of WeChat user (OpenId), and the OpenId must come from WeChat public number, applet and other modules. If you need to use WeChat Pay within the applet, you need to register the applet accordingly.

The value of `weixinSetting` comes from `appsettings.json` by default:

```json
"SenparcWeixinSetting": {
    "IsDebug": true,

    //public
    "Token": "#{Token}#",
    "EncodingAESKey": "#{EncodingAESKey}#",
    "WeixinAppId": "#{WeixinAppId}#",
    "WeixinAppSecret": "#{WeixinAppSecret}#",

    //Weixin Pay
    //Weixin Pay V3 (old document V3)
    "TenPayV3_AppId": "#{TenPayV3_AppId}#",
    "TenPayV3_AppSecret": "#{TenPayV3_AppSecret}#",
    "TenPayV3_SubAppId": "#{TenPayV3_SubAppId}#",
    "TenPayV3_SubAppSecret": "#{TenPayV3_SubAppSecret}#",
    "TenPayV3_MchId": "#{TenPayV3_MchId}#",
    "TenPayV3_SubMchId": "#{TenPayV3_SubMchId}#", //Sub-merchant, leave blank if you don't have one.
    "TenPayV3_Key": "#{TenPayV3_Key}#", /* Sub-merchant, leave blank if you don't have one.
    /* Certificate path (APIv3 can not be used)
     * 1, physical path, such as: D:\\cert\\\apiclient_cert.p12
     * 2, relative path, such as: ~ / App_Data / cert / apiclient_cert.p12, note: must be placed in App_Data and other protected directories, to avoid leaks
     * Note: Certificate download address: https://pay.weixin.qq.com/index.php/account/api_cert
     */
    "TenPayV3_CertPath": "#{TenPayV3_CertPath}#", //(V3 API can not be used) Certificate Path
    "TenPayV3_CertSecret": "#{TenPayV3_CertSecret}#", //(not used by V3 API) Payment certificate password (original password is the same as MchId)
    "TenPayV3_TenpayNotify": "#{TenPayV3_TenpayNotify}#", //http://YourDomainName/TenpayV3/PayNotifyUrl
    //If you don't set TenPayV3_WxOpenTenpayNotify, the default is to add "WxOpen" at the end of the value of TenPayV3_TenpayNotify
    "TenPayV3_WxOpenTenpayNotify": "#{TenPayV3_WxOpenTenpayNotify}#" //http://YourDomainName/TenpayV3/PayNotifyUrlWxOpen
  }
```

Among them, `Token`, `EncodingAESKey`, `WeixinAppId` and `WeixinAppSecret` correspond to the configuration parameters of WeChat Public Number backend.

Special Note: The `TenPayV3_CertPath` parameter supports both full physical path and relative path (starting with `~/` relative to the application root directory). The certificate file must be placed in a protected directory such as App_Data to avoid leakage.

![配置参数](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-register-03.png)

> Reference file for this project:
>
> /**_appsettings.json_**

Configuration complete.

> Tip: Information for auto-registration can be obtained from `Senparc.Weixin.Config.SenparcWeixinSetting`.
