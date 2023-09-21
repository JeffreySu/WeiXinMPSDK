# Registration

## Global Registration

The registration process is similar for all Senparc.Weixin SDKs.

First, complete the overall registration code for all Senparc.Weixin SDKs. Add the following code to Program.cs:

![注册 Senparc.Weixin](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-register-01.png)

Instructions:

1. `builder.Services.AddMemoryCache()` Senparc.Weixin supports various caching strategies such as local cache, Redis, Memcached, etc. Local cache is used by default.
2. `builder.Services.AddSenparcWeixinServices(builder.Configuration)` is used to complete the registration of Senparc.
3. `app.UseSenparcWeixin()` method is used to configure and enable Senparc.

The above code is the same for all Senparc.Weixin descendant modules, only 3 lines of code are needed.

> Reference file for this project:
>
> /**_Program.cs_**

## Public Registration

Insert code into the delegate method in line 27 of the above code to complete the registration of the default public number as well as Weixin payment:

```cs
 // Register public number information (can be executed multiple times to register multiple public numbers)
    register.RegisterMpAccount(weixinSetting, "[Shengpai Networks Little Helper] Public No.");; //Register public number information (can be executed multiple times, register multiple public numbers)
    // Register Weixin Payment (can be executed multiple times, register multiple Weixin Payments)
    register.RegisterTenpayApiV3(weixinSetting, "[Shengpai Networks Little Helper] WeChat Payment (ApiV3)");;
```

![注册微信公众号](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-register-02.png)

> Attention:
>
> The above code registers both public number and WeChat payment (V2), because WeChat payment needs to provide the identification of WeChat user (OpenId), and OpenId must come from WeChat public number, applet and other modules. If you need to use WeChat Pay within the applet, you need to register the applet accordingly.

The value of `weixinSetting` comes from `appsettings.json` by default:

```json
 "SenparcWeixinSetting": {
    "IsDebug": true,
 }

//public
"Token": "#{Token}#",
"EncodingAESKey": "#{EncodingAESKey}#",
"WeixinAppId": "#{WeixinAppId}#",
"WeixinAppSecret": "#{WeixinAppSecret}#",

// Weixin Pay V3
"TenPayV3_AppId": "#{TenPayV3_AppId}#",
"TenPayV3_AppSecret": "#{TenPayV3_AppSecret}#",
"TenPayV3_SubAppId": "#{TenPayV3_SubAppId}#",
"TenPayV3_SubAppSecret": "#{TenPayV3_SubAppSecret}#",
"TenPayV3_MchId": "#{TenPayV3_MchId}#",
"TenPayV3_SubMchId": "#{TenPayV3_SubMchId}#", //Sub-merchant, leave blank if you don't have one.
"TenPayV3_Key": "#{TenPayV3_Key}#", //Sub-merchant, leave blank if you don't have one.
"TenPayV3_TenpayNotify": "#{TenPayV3_TenpayNotify}#", //http://YourDomainName/TenpayApiV3/PayNotifyUrl
/* Payment certificate private key
 * 1, support plaintext private key (no newline characters)
 * 2, private key file path (such as: ~/App_Data/cert/apiclient_key.pem), note: must be placed in App_Data and other protected directories, to avoid leaks
 */
"TenPayV3_PrivateKey": "#{TenPayV3_PrivateKey}#", //(new) certificate private key
"TenPayV3_SerialNumber": "#{TenPayV3_SerialNumber}#", //(new) certificate serial number
"TenPayV3_ApiV3Key": "#{TenPayV3_APIv3Key}#", //(new) APIv3 key

```

![配置参数](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-register-03.png)

Where `Token`, `EncodingAESKey`, `WeixinAppId` and `WeixinAppSecret` correspond to the configuration parameters of the WeChat public backend.

Special Note: `TenPayV3_PrivateKey` can use the already processed private key, or you can directly provide the private key file downloaded from the official website of WeChat Pay (extract the file apiclient_key.pem in the downloaded zip and copy it to the secure path, recommended `App_Data` directory), the virtual path starts from the root directory of the website. It must start with `~/`, such as `~/App_Data/cert/apiclient_key.pem`, SDK will process it fully automatically.

> Reference file for this project:
>
> /appsettings.json

Configuration complete.

> Tip: The auto-registration information can be obtained via `Senparc.Weixin.Config.SenparcWeixinSetting`.
