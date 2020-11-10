## 如何使用 `Senparc.Weixin SDK` 进行小程序云开发

[云开发（CloudBase）](https://www.cloudbase.net/) 是基于Serverless架构构建的一站式后端云服务，涵盖函数、数据库、存储、CDN等服务，免后端运维，支持小程序、Web和APP开发。 其中，小程序·云开发是微信和腾讯云联合推出的云端一体化解决方案，基于云开发可以免鉴权调用微信所有开放能力，在微信开发者工具中即可开通使用。

### 第一步：引用并配置 Senparc.Weixin SDK

在任意 Web/桌面/命令行项目中引入 [Senparc.Weixin.WxOpen（小程序包）](https://www.nuget.org/packages/Senparc.Weixin.WxOpen)。

在 appsettings.json 中配置小程序的信息，如：

``` json

  //CO2NET 设置
  "SenparcSetting": {
    "IsDebug": true,
    "DefaultCacheNamespace": "DefaultCache",

    //分布式缓存
    "Cache_Redis_Configuration": "#{Cache_Redis_Configuration}#", //Redis配置
    "Cache_Memcached_Configuration": "#{Cache_Memcached_Configuration}#", //Memcached配置
    "SenparcUnionAgentKey": "#{SenparcUnionAgentKey}#"
  },
  //Senparc.Weixin SDK 设置
  "SenparcWeixinSetting": {
    //微信全局
    "IsDebug": true,
    //追加小程序配置
    "WxOpenAppId": "#{WxOpenAppId}#",
    "WxOpenAppSecret": "#{WxOpenAppSecret}#",
    "WxOpenToken": "#{WxOpenToken}#",
    "WxOpenEncodingAESKey": "#{WxOpenEncodingAESKey}#"
    }

```

其中， WxOpenAppId 和 WxOpenAppSecret 的字符串值（包括#{}#占位符）替换为小程序后台的值，如，将`#{WxOpenAppId}#` 替换为：`wx12b4f63276b14d4c`。

### 第二步：程序中注册小程序

在启动代码或 Startup.cs 的 ConfigureServices() 方法中，添加代码，注册 Senparc.Weixin SDK：

``` C#
services.AddSenparcWeixinServices(Configuration); //Senparc.Weixin 注册
```

在 Configure() 方法中添加两个参数，自动引入 appsettings.json 中的配置：IOptionssenparcSetting, IOptionssenparcWeixinSetting，完整代码：

``` C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                IOptions<SenparcSetting> senparcSetting, 
                IOptions<SenparcWeixinSetting> senparcWeixinSetting)
```

在方法体末尾追加代码：
``` C#
app.UseSenparcGlobal(env, senparcSetting.Value, null, true)
.UseSenparcWeixin(senparcWeixinSetting.Value,
weixinRegister =>
{
    weixinRegister.RegisterWxOpenAccount(senparcWeixinSetting.Value, "【云函数】小程序");
});
```

其中第一行代码是配置启用 CO2NET（Senparc.Weixin 的一个基础库）全局配置，第二行代码开始配置 Senparc.Weixin SDK 及小程序参数。

至此，小程序的所有配置工作已经完成。

### 第三步：调用小程序云开发的云函数

高级接口可以在任意地方触发，例如网页、队列、独立线程、服务，或由 UI 触发的事件。

在小程序开发工具的客户端配置完后，直接在需要触发的代码位置，调用接口：

``` C#
var wxOpenSetting = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenSetting;
var envId= "senparc-robot-5f5128";
var result = Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
              .TcbApi.DatabaseCollectionGet(wxOpenSetting.WxOpenAppId, envId);
```
其中，wxOpenSetting 是通过 startup.cs 中代码自动进行了全局配置的全套小程序配置参数，evnId 是云函数的环境ID。

除此以外，还可以调用所有 Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb.TcbApi 下的所有云开发接口，及其他小程序接口。

### 更多

请参考《[如何在 C# 平台调用云开发？](https://mp.weixin.qq.com/s/6dKkdxoyF4x3mZkBuDZjyg)》。
