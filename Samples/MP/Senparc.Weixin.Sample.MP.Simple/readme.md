# Senparc.Weixin.Sample.MP.Simple 项目说明

当先项目为公众号平台提供了简化的示例，相关功能已经可以满足最基本公众号的开发。

您可以直接运行 `Senparc.Weixin.Sample.MP.Simple.csproj` 文件单独运行公众号示例，或在 `/Samples/All/net8-mvc` 下的解决方案中找到此项目。

> 由于所有 Senparc.Weixin SDK 遵循统一的底层标准，因此当前公众号的所有演示方法同样可以举一反三运用到小程序、企业微信、微信支付、开放平台等其他平台。

## 关键代码

### 一站式引用类库

您可以单独引用公众号的 Nuget 包：Senparc.Weixin.MP，也可以一次性引用完整包，以使您的程序具备开发所有微信平台的能力：Senparc.Weixin.MP。

在 .csproj 文件中添加引用（或使用命令行添加:`dotnet add package Senparc.Weixin.MP`）：

``` xml
<PackageReference Include="Senparc.Weixin.All" Version="2024.6.29" />
```

### 添加注册代码

在 Program.cs 中对 SDK 及基础库进行注册：

``` C#
//Senparc.Weixin 注册（必须）
builder.Services.AddSenparcWeixin(builder.Configuration);
```

``` C#
//启用微信配置（必须）
var registerService = app.UseSenparcWeixin(app.Environment,
        senparcSetting: null /* 不为 null 则覆盖 appsettings  中的 SenpacSetting 配置*/,
        senparcWeixinSetting: null /* 不为 null 则覆盖 appsettings  中的 SenpacWeixinSetting 配置*/,
        globalRegisterConfigure: register => { /* CO2NET 全局配置 */ },
        weixinRegisterConfigure: (register, weixinSetting) => {/* 注册公众号或其他平台信息（可以执行多次，注册多个公众号）*/},
        autoRegisterAllPlatforms: true /* 自动注册所有平台 */
        );
```
> 如果不需要进行额外配置，则无需修改上述任何代码

只需两行代码，即可完成所有 Senparc.Weixin SDK 调用

### 如何使用高级接口？

您可以在程序任意位置，调用公众号（或其他平台）的高级接口。

例如，当您需要获取当前关注用的的 OpenId 时，这样进行调用：

``` C#
    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting.MpSetting;
    var users = await Senparc.Weixin.MP.AdvancedAPIs.UserApi.GetAsync(weixinSetting.WeixinAppId, null);
```

您也可以使用 MiniAPI 快速设置一个接口（请注意接口开放范围和安全性）：

``` C#
app.MapGroup("/").MapGet("/TryApi", async () =>
{
    //演示获取已关注用户的 OpenId（分批获取的第一批）

    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting.MpSetting;
    var users = await Senparc.Weixin.MP.AdvancedAPIs.UserApi.GetAsync(weixinSetting.WeixinAppId, null);
    return users.data.openid;
});
```

>提醒：使用时请注意接口开放范围和安全性

### 如何提供消息自动回复能力？

您可以使用 `hMessageHandler` 快速设置消息回复能力

#### 创建消息处理类

创建一个新的类，CustomMessageHander.cs：

``` C#
namespace Senparc.Weixin.Sample.MP
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<DefaultMpMessageContext>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider: serviceProvider)
        {
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"收到一条您发来的信息，类型为：{requestMessage.MsgType}";
            return responseMessage;
        }
    }
}
```

当我们需要处理特定类型信息的时候，只需要添加对应的重写方法，如当我们需要处理用户发过来的文字信息时，添加一个方法：

``` C#
public override async Task<IResponseMessageBase> OnTextOrEventRequestAsync(RequestMessageText requestMessage)
{
    var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
    responseMessage.Content = $"收到您发来的文字信息：{requestMessage.Content}";
    return responseMessage;
}
```

当我们需要将这个 MessageHandler 暴露给微信服务器时，可以将其放入 Controller 直接暴露 API 接口，或使用中间件的方式，只需在 Program.cs 中添加一句代码：

``` C#
app.UseMessageHandlerForMp("/WeixinAsync", CustomMessageHandler.GenerateMessageHandler, options =>
{
    //获取默认微信配置
    var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting;

    //[必须] 设置微信配置
    options.AccountSettingFunc = context => weixinSetting;

    //[可选] 设置最大文本长度回复限制（超长后会调用客服接口分批次回复）
    options.TextResponseLimitOptions = new TextResponseLimitOptions(2048, weixinSetting.WeixinAppId);
});
```

此时，我们就可以将 `https://domain/WeixinAsync` 这个路径提供给微信作为消息接口。

### 如何提供微信秘钥等信息？

当我们使用微信消息接口、高级接口的时候，需要提供 Token、AppId、AppSecret 等信息，此时我们只需在 `appsettings.json` 中添加相应的注册代码：

``` json
//CO2NET 设置
"SenparcSetting": {
  //以下为 CO2NET 的 SenparcSetting 全局配置，请勿修改 key，勿删除任何项

  "IsDebug": true,
  "DefaultCacheNamespace": "DefaultCache",

  //分布式缓存
  "Cache_Redis_Configuration": "#{Cache_Redis_Configuration}#", //Redis配置
  "Cache_Memcached_Configuration": "#{Cache_Memcached_Configuration}#", //Memcached配置
  "SenparcUnionAgentKey": "#{SenparcUnionAgentKey}#" //SenparcUnionAgentKey
},

//Senparc.Weixin SDK 设置
"SenparcWeixinSetting": {
  //以下为 Senparc.Weixin 的 SenparcWeixinSetting 微信配置
  //注意：所有的字符串值都可能被用于字典索引，因此请勿留空字符串（但可以根据需要，删除对应的整条设置）！

  //微信全局
  "IsDebug": true,

  //以下不使用的参数可以删除，key 修改后将会失效

  //公众号
  "Token": "#{Token}#", //说明：字符串内两侧#和{}符号为 Azure DevOps 默认的占位符格式，如果您有明文信息，请删除同占位符，修改整体字符串，不保留#和{}，如：{"Token": "MyFullToken"}
  "EncodingAESKey": "#{EncodingAESKey}#",
  "WeixinAppId": "#{WeixinAppId}#",
  "WeixinAppSecret": "#{WeixinAppSecret}#"
}
```

上述代码中，`SenparcSetting` 节点是基础的系统注册信息（如缓存等、调试状态等），建议保留。`SenparcWeixinSetting` 节点用于设置微信平台，
上述代码仅提供了公众号所必须的信息，如果您需要同时注册更多平台，可以参考 [/Samples/All/net8-mvc](/Samples/All/net8-mvc/Senparc.Weixin.Sample.Net8/appsettings.json) 下的完整示例，或 `/Samples/` 目录下的其他示例。

