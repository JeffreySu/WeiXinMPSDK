# 目录
* [微信C# SDK](#微信c-sdk--)
* [资源](#资源)
* [贡献代码](#贡献代码)
* [如何使用.net core开发](#如何使用net-core开发)
* [关注测试账号（SenparcRobot）](#关注测试账号senparcrobot)
* [项目文件夹说明（src文件夹下）](#项目文件夹说明src文件夹下)
* [Senparc.Weixin.MP.Sample中的关键代码说明](#senparcweixinmpsample中的关键代码说明)
    * [/Controllers/WeixinController.cs](#controllersweixincontrollercs)
    * [如何处理微信公众账号请求？](#如何处理微信公众账号请求)
* [使用Nuget安装到项目中](#使用nuget安装到项目中)
    * [如何处理微信公众号请求？](#如何处理微信公众号请求)
    * [如何处理微信小程序请求？](#如何处理微信小程序请求)
    * [如何增强 ASP.NET MVC 项目的功能？](#如何增强-aspnet-mvc-项目的功能)
    * [如何处理微信企业号请求？](#如何处理微信企业号请求)
    * [如何处理微信开放平台请求？](#如何处理微信开放平台请求)
    * [如何使用分布式缓存？](#如何使用分布式缓存)
* [如何开发小程序](#如何开发小程序)
* [已实现功能](#已实现功能)
* [捐助](#捐助)
* [图书众筹](#图书众筹)
* [License](#license)



微信C# SDK  [![Build Status](https://travis-ci.org/JeffreySu/WeiXinMPSDK.svg?branch=master)](https://travis-ci.org/JeffreySu/WeiXinMPSDK)
=================

| # | 模块功能                                            | DLL                               | Nuget                                       |
|---|----------------------------------------------------|-----------------------------------|---------------------------------------------|
| 1| 基础库                                               |Senparc.Weixin.dll                 | [![Senparc.Weixin][1.1]][1.2]               |
| 2| 微信公众号 / 微信支付 / JSSDK / 摇周边 / 等等 |Senparc.Weixin.MP.dll                       | [![Senparc.Weixin.MP][2.1]][2.2]            |
| 3| ASP.NET MVC 扩展                                     |Senparc.Weixin.MP.MVC.dll          | [![Senparc.Weixin.MP.MVC][3.1]][3.2]        |
| 4| 微信企业号                                           |Senparc.Weixin.QY.dll              | [![Senparc.Weixin.QY][4.1]][4.2]            |
| 5| 企业微信（准备中）                                    |Senparc.Weixin.Work.dll            | [![Senparc.Weixin.Work][5.1]][5.2]            |
| 6| 微信开放平台                                         |Senparc.Weixin.Open.dll            | [![Senparc.Weixin.Open][6.1]][6.2]          |
| 7| Redis 分布式缓存                                     |Senparc.Weixin.Cache.Redis.dll     | [![Senparc.Weixin.Cache.Redis][7.1]][7.2]   |
| 8| Memcached 分布式缓存                                 |Senparc.Weixin.Cache.Memcached.dll |[![Senparc.Weixin.Cache.Memcached][8.1]][8.2]| 
| 9| [微信小程序（独立项目）](https://github.com/JeffreySu/WxOpen)    |Senparc.Weixin.WxOpen.dll		  |[![Senparc.Weixin.WxOpen][9.1]][9.2]         | 
|10| [WebSocket（独立项目）](https://github.com/JeffreySu/Senparc.WebSocket)    |Senparc.WebSocket.dll		  |[![Senparc.WebSocket][10.1]][10.2]         | 

[1.1]: https://img.shields.io/nuget/v/Senparc.Weixin.svg?style=flat
[1.2]: https://www.nuget.org/packages/Senparc.Weixin
[2.1]: https://img.shields.io/nuget/v/Senparc.Weixin.MP.svg?style=flat
[2.2]: https://www.nuget.org/packages/Senparc.Weixin.MP
[3.1]: https://img.shields.io/nuget/v/Senparc.Weixin.MP.MVC.svg?style=flat
[3.2]: https://www.nuget.org/packages/Senparc.Weixin.MP.MVC
[4.1]: https://img.shields.io/nuget/v/Senparc.Weixin.QY.svg?style=flat
[4.2]: https://www.nuget.org/packages/Senparc.Weixin.QY
[5.1]: https://img.shields.io/nuget/v/Senparc.Weixin.Work.svg?style=flat
[5.2]: https://www.nuget.org/packages/Senparc.Weixin.Work
[6.1]: https://img.shields.io/nuget/v/Senparc.Weixin.Open.svg?style=flat
[6.2]: https://www.nuget.org/packages/Senparc.Weixin.Open
[7.1]: https://img.shields.io/nuget/v/Senparc.Weixin.Cache.Redis.svg?style=flat
[7.2]: https://www.nuget.org/packages/Senparc.Weixin.Cache.Redis
[8.1]: https://img.shields.io/nuget/v/Senparc.Weixin.Cache.Memcached.svg?style=flat
[8.2]: https://www.nuget.org/packages/Senparc.Weixin.Cache.Memcached
[9.1]: https://img.shields.io/nuget/v/Senparc.Weixin.WxOpen.svg?style=flat
[9.2]: https://www.nuget.org/packages/Senparc.Weixin.WxOpen
[10.1]: https://img.shields.io/nuget/v/Senparc.WebSocket.svg?style=flat
[10.2]: https://www.nuget.org/packages/Senparc.WebSocket


本库为.NET Core，其他.NET版本请看各自分支：master(.NET 4.5)、.NET 4.0等。

* 已经支持所有微信6 API，包括自定义菜单/个性化菜单、模板信息接口、素材上传接口、群发接口、多客服接口、支付接口、微小店接口、卡券接口等等。
* 已经支持用户会话上下文（解决服务器无法使用Session处理用户信息的问题）。
* 已经全面支持微信公众号、企业号、开放平台的最新API。
* 已经支持分布式缓存及缓存策略扩展。

目前官方的API都已完美集成，除非有特殊说明，所有升级都会尽量确保向下兼容，所以已经发布的版本请放心使用或直接升级（覆盖）最新的[DLLs](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut)。

## 如何使用.NET Core开发

> .NET Framework 版本及 .NET Core 版本代码分别位于 
[master](https://github.com/JeffreySu/WeiXinMPSDK) 
和 [DotNET-Core](https://github.com/JeffreySu/WeiXinMPSDK/tree/DotNET-Core) 分支下，
结构保持了高度一致。

> Senparc.Weixin SDK已经针对.NET Core进行了优化，.NET Core 的开发过程和.NET Framework几乎是一样的，
所有的接口、方法、命名规则和架构设计也都保持了高度的一致。

> 由于.NET Core对某些特性支持正在完善中，目前.NET Core版本暂未提供分布式缓存有关的功能以及Senpar.Weixin.Open.dll。
除此以外的所有库都已在Nuget包中支持，可以直接使用（同一个Nuget包同时支持.NET 4.0/4.5/Core，安装后程序会自动根据项目环境适配）。

> 以下所有介绍以 .NET Framework 版本为例。

## 贡献代码

> 如果需要使用或修改此项目的源代码，建议先Fork。也欢迎将您修改的通用版本Pull Request过来。

1. Fork
2. 创建您的特性分支 (`git checkout -b my-new-feature`)
3. 提交您的改动 (`git commit -am 'Added some feature'`)
4. 将您的修改记录提交到远程 `git` 仓库 (`git push origin my-new-feature`)
5. 然后到 github 网站的该 `git` 远程仓库的 `my-new-feature` 分支下发起 Pull Request

资源
----------------
1. 官网地址：http://weixin.senparc.com/
2. Demo 地址：http://sdk.weixin.senparc.com/
3. 微信开发系列教程：http://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html
4. 微信技术交流社区：http://weixin.senparc.com/QA
5. 自定义菜单在线编辑工具：http://sdk.weixin.senparc.com/Menu
6. 在线消息测试工具：http://sdk.weixin.senparc.com/SimulateTool
7. 缓存测试工具：http://sdk.weixin.senparc.com/Cache/Test
8. chm帮助文档下载：http://sdk.weixin.senparc.com/Document
9. 源代码及最新更新：https://github.com/JeffreySu/WeiXinMPSDK
10. 微信开发资源集合：https://github.com/JeffreySu/WeixinResource

* 技术交流QQ群：

> 1群：300313885（已满），3群：342319110（已满），4群：372212092（已满）

> 5群：377815480（已满），6群：425898825（已满），7群：482942254（已满）

> 8群：106230270（已满），9群：539061281（已满），11群：553198593（已满）

> *`以下群未满，可加：`*

> `2群（公众号）：293958349`

> `10群（分布式缓存群）：246860933`

> `12群（微信小程序）：108830388`

> `13群（开放平台）：183424136`

* 业务联系QQ：498977166

* 新浪微博：[@苏震巍](http://weibo.com/jeffreysu1984)

如果这个项目对您有用，我们欢迎各方任何形式的捐助，也包括参与到项目代码更新或意见反馈中来。谢谢！


资金捐助：[进入](http://sdk.weixin.senparc.com#donate)


### 关注测试账号（SenparcRobot）：
[![qrcode](http://sdk.weixin.senparc.com/Images/qrcode.jpg)](http://weixin.senparc.com/)


## 如何使用.NET Core开发

> .NET Framework 版本及 .NET Core 版本代码分别位于 
[master](https://github.com/JeffreySu/WeiXinMPSDK) 
和 [DotNET-Core](https://github.com/JeffreySu/WeiXinMPSDK/tree/DotNET-Core) 分支下，
结构保持了高度一致。

> Senparc.Weixin SDK已经针对.NET Core进行了优化，.NET Core 的开发过程和.NET Framework几乎是一样的，
所有的接口、方法、命名规则和架构设计也都保持了高度的一致。

> 由于.NET Core对某些特性支持正在完善中，目前.NET Core版本暂未提供分布式缓存有关的功能以及Senpar.Weixin.Open.dll。
除此以外的所有库都已在Nuget包中支持，可以直接使用（同一个Nuget包同时支持.NET 4.0/4.5/Core，安装后程序会自动根据项目环境适配）。

> 以下所有介绍以 .NET Framework 版本为例。

## 贡献代码

> 如果需要使用或修改此项目的源代码，建议先Fork。也欢迎将您修改的通用版本Pull Request过来。

1. Fork
2. 创建您的特性分支 (`git checkout -b my-new-feature`)
3. 提交您的改动 (`git commit -am 'Added some feature'`)
4. 将您的修改记录提交到远程 `git` 仓库 (`git push origin my-new-feature`)
5. 然后到 github 网站的该 `git` 远程仓库的 `my-new-feature` 分支下发起 Pull Request
（请提交到 `Developer` 分支，不要直接提交到 `master` 分支）


项目文件夹说明（src文件夹下）
--------------

| 文件夹 | 说明 |
|--------|--------|
|Senparc.WebSocket|WebSocket 模块|
|Senparc.Weixin.Cache|Senparc.Weixin.Cache.Memcached.dll 、 Senparc.Weixin.Cache.Redis.dll 等分布式缓存扩展方案|
|Senparc.Weixin.MP.BuildOutPut|所有最新版本DLL发布文件夹|
|Senparc.Weixin.MP.MvcExtension|Senparc.Weixin.MP.MvcExtension.dll源码，为MVC4.0项目提供的扩展包。|
|Senparc.Weixin.MP.Sample|可以直接发布使用的Demo（ASP.NET MVC 4.5）|
|Senparc.Weixin.MP.Sample.WebForms|可以直接发布使用的Demo（ASP.NET WebForms）|
|Senparc.Weixin.MP|Senparc.Weixin.MP.dll 微信公众账号SDK源代码|
|Senparc.Weixin.Open|Senparc.Weixin.Open.dll 第三方开放平台SDK源代码|
|Senparc.Weixin.QY|Senparc.Weixin.QY.dll 微信企业号SDK源代码|
|Senparc.Weixin.Work|Senparc.Weixin.Work.dll 企业微信SDK源代码|
|Senparc.Weixin.WxOpen|Senparc.Weixin.WxOpen.dll 微信小程序SDK源代码|
|Senparc.Wiexin|所有Senparc.Weixin.[x].dll 基础类库源代码|

Senparc.Weixin.MP.Sample中的关键代码说明
--------------
>注：这是MVC项目，WebForms项目见对应Demo中的Weixin.aspx。

### /Controllers/WeixinController.cs

下面的Token需要和微信公众平台后台设置的Token同步，如果经常更换建议写入Web.config等配置文件（实际使用过程中两列建议使用数字+英文大小写改写Token，Token一旦被破解，微信请求将很容易被伪造！）：
```C#
public readonly string Token = "weixin";
```
下面这个Action（Get）用于接收并返回微信后台Url的验证结果，无需改动。地址如：http://domain/Weixin或http://domain/Weixin/Index
```C#
/// <summary>
/// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
/// </summary>
[HttpGet]
[ActionName("Index")]
public ActionResult Get(PostModel postModel, string echostr)
{
    if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content(echostr); //返回随机字符串则表示验证通过
    }
    else
    {
        return Content("failed:" + postModel.Signature + "," 
            + MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
            "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
    }
}
```
上述方法中的PostModel是一个包括了了Signature、Timestamp、Nonce（由微信服务器通过请求时的Url参数传入），以及AppId、Token、EncodingAESKey等一系列内部敏感的信息（需要自行传入）的实体类，同时也会在后面用到。


下面这个Action（Post）用于接收来自微信服务器的Post请求（通常由用户发起），这里的if必不可少，之前的Get只提供微信后台保存Url时的验证，每次Post必须重新验证，否则很容易伪造请求。
```C#
/// <summary>
/// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
/// </summary>
[HttpPost]
[ActionName("Index")]
public ActionResult Post(PostModel postModel)
{
    if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content("参数错误！");
    }
    ...
}
```
### 如何处理微信公众账号请求？

Senparc.Weixin.MP提供了2中处理请求的方式，[传统方法](https://github.com/JeffreySu/WeiXinMPSDK/wiki/处理微信信息的常规方法)及使用[MessageHandler](https://github.com/JeffreySu/WeiXinMPSDK/wiki/%E5%A6%82%E4%BD%95%E4%BD%BF%E7%94%A8MessageHandler%E7%AE%80%E5%8C%96%E6%B6%88%E6%81%AF%E5%A4%84%E7%90%86%E6%B5%81%E7%A8%8B)处理方法（推荐）。上面两个方法在wiki中已经有比较详细的说明，这里简单举例MessageHandler的处理方法。

MessageHandler的处理流程非常简单：
``` C#
[HttpPost]
[ActionName("Index")]
public ActionResult Post(PostModel postModel)
{
    if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content("参数错误！");
    }

    postModel.Token = Token;
    postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
    postModel.AppId = AppId;//根据自己后台的设置保持一致

    var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//接收消息（第一步）

    messageHandler.Execute();//执行微信处理过程（第二步）

    return new FixWeixinBugWeixinResult(messageHandler);//返回（第三步）
}
```
整个消息除了postModel的赋值以外，接收（第一步）、处理（第二步）、返回（第三步）分别只需要一行代码。

上述代码中的CustomMessageHandler是一个自定义的类，继承自Senparc.Weixin.MP.MessageHandler.cs。MessageHandler是一个抽象类，包含了执行各种不同请求类型的抽象方法（如文字，语音，位置、图片等等），我们只需要在自己创建的CustomMessageHandler中逐个实现这些方法就可以了。刚建好的CustomMessageHandler.cs如下：

```C#
using System;
using System.IO;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Sample.CustomerMessageHandler
{
    public class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {

        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            //ResponseMessageText也可以是News等其他类型
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //...
        }

        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            //...
        }

        //更多没有重写的OnXX方法，将默认返回DefaultResponseMessage中的结果。
        ....
    }
}
```

其中OnTextRequest、OnVoiceRequest等分别对应了接收文字、语音等不同的请求类型。

比如我们需要对文字类型请求做出回应，只需要完善OnTextRequest方法：
```C#
      public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
      {
          //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs
          var responseMessage = CreateResponseMessage<ResponseMessageText>();
          responseMessage.Content =
              string.Format(
                  "您刚才发送了文字信息：{0}",
                  requestMessage.Content);
          return responseMessage;
      }
```
这样CustomMessageHandler在执行messageHandler.Execute()的时候，如果发现请求信息的类型是文本，会自动调用以上代码，并返回代码中的responseMessage作为返回信息。responseMessage可以是IResponseMessageBase接口下的任何类型（包括文字、新闻、多媒体等格式）。

从v0.4.0开始，MessageHandler增加了对用户会话上下文的支持，用于解决服务器上无法使用Session管理用户会话的缺陷。详见：[用户上下文WeixinContext和MessageContext](https://github.com/JeffreySu/WeiXinMPSDK/wiki/%E7%94%A8%E6%88%B7%E4%B8%8A%E4%B8%8B%E6%96%87WeixinContext%E5%92%8CMessageContext)


使用Nuget安装到项目中
--------------
### 如何处理微信公众号请求？

* Nuget 地址：https://www.nuget.org/packages/Senparc.Weixin.MP

* 命令：
```
PM> Install-Package Senparc.Weixin.MP
```


### 如何处理微信小程序请求？

Senparc.Weixin.WxOpen对微信小程序的消息、API进行了封装，保持了公众号处理请求一致的开发过程。

* Nuget 地址：https://www.nuget.org/packages/Senparc.Weixin.WxOpen

* 命令：
```
PM> Install-Package Senparc.Weixin.WxOpen
```

### 如何增强 ASP.NET MVC 项目的功能？

Senparc.Weixin.MP.MVC 针对 ASP.NET MVC 项目做了更多的优化，包括便捷的浏览器环境判断、官方 bug 修复等。
* Nuget 地址：https://www.nuget.org/packages/Senparc.Weixin.MP.MVC

* 命令：
```
PM> Install-Package Senparc.Weixin.MP.MVC
```

### 如何处理微信企业号请求？

Senparc.Weixin.QY.dll对企业号相关功能进行了封装，操作过程和微信公众账号SDK（Senparc.Weixin.MP）保持了一致。

* Nuget 地址：https://www.nuget.org/packages/Senparc.Weixin.QY

* 命令：
```
PM> Install-Package Senparc.Weixin.QY
```

### 如何处理微信开放平台请求？

Senparc.Weixin.Open.dll对目前所有的开放平台API进行了封装，消息处理过程和微信公众账号SDK（Senparc.Weixin.MP）保持了一致，其他一些特殊的消息流程请先阅读官方的文档，然后对照Senparc.Weixin.MP.Sample中有关Open的Demo进行开发。

* Nuget 地址为https://www.nuget.org/packages/Senparc.Weixin.Open

* 命令：
```
PM> Install-Package Senparc.Weixin.Open
```


### 如何使用分布式缓存？

Senparc.Weixin SDK 提供了完善的缓存策略接口，默认使用本机缓存实现，同时也提供了 Redis 和 Memcached 两个扩展方案，您也可以根据相同的规则添加自己的缓存策略。

* Redis 缓存扩展包 Nuget 地址：https://www.nuget.org/packages/Senparc.Weixin.Cache.Redis
* 命令：
```
PM> Install-Package Senparc.Weixin.Senparc.Weixin.Cache.Redis
```

* Memcached 缓存扩展包 Nuget 地址：https://www.nuget.org/packages/Senparc.Weixin.Cache.Memcached
* 命令：
```
PM> Install-Package Senparc.Weixin.Senparc.Weixin.Cache.Memcached
```


如何开发小程序
--------------
小程序的后端架构和公众号保持了高度一致，
只需要使用Nuget安装[Senparc.Weixin.WxOpen](https://www.nuget.org/packages/Senparc.Weixin.WxOpen)库即可开始使用小程序。
Senparc.Weixin.WxOpen目前包含了所有小程序需要用到的消息处理、AccessToken管理、模板消息、二维码生成等全套功能。


已实现功能
-------------
* 微信公众号
>   - [x] 接收/发送消息（事件）
>   - [x] 自定义菜单 & 个性化菜单
>   - [x] 消息管理
>   - [x] OAuth授权
>   - [x] JSSDK
>   - [x] 微信支付
>   - [x] 用户管理
>   - [x] 素材管理
>   - [x] 账号管理
>       - [x] 带参数二维码
>       - [X] 长链接转短链接接口
>       - [ ] 微信认证事件推送
>   - [x] 数据统计
>   - [x] 微信小店
>   - [x] 微信卡券
>       - [x] 卡券事件推送
>           - [ ] 买单事件推送
>           - [ ] 会员卡内容更新事件推送
>           - [ ] 库存报警事件推送
>           - [ ] 券点流水详情事件推送
>   - [x] 微信门店
>   - [x] 微信智能
>   - [x] 微信设备功能
>   - [x] 多客服功能
>   - [x] 微信摇一摇周边
>   - [x] 微信连WI-FI（未完整）
>   - [x] 微信扫一扫（商家）
>       - [ ] 扫一扫事件推送
>           - [ ] 打开商品主页事件推送
>           - [ ] 关注公众号事件推送
>           - [ ] 进入公众号事件推送
>           - [ ] 地理位置信息异步推送
>           - [ ] 商品审核结果推送

* 微信开放平台
>   - [x] 网站应用
>   - [x] 公众号第三方平台


* 微信企业号
>	- [x] 管理通讯录
>	- [x] 管理素材文件
>	- [x] 管理企业号应用
>	- [x] 接收消息与事件
>	- [x] 发送消息
>	- [x] 自定义菜单
>	- [x] 身份验证接口
>	- [x] JSSDK
>	- [x] 第三方应用授权
>	    - [x] 第三方回调协议
>	        - [ ] 授权成功推送auth_code事件
>	        - [ ] 通讯录变更通知
> 	- [x] 企业号授权登陆
>	- [x] 企业号微信支付
>	- [x] 企业回话服务
>	    - [ ] 企业会话回调
>	- [x] 企业摇一摇周边
>	- [ ] 企业卡券服务
>	    - [ ] 卡券事件推送
>	- [x] 企业客服服务
>	    - [ ] 客服回复消息回调
	    


* 缓存策略
>   - [x] 策略扩展接口
>   - [x] 本地缓存
>   - [x] Redis 扩展包
>   - [x] Memcached 扩展包

 欢迎开发者对未完成或需要补充的模块进行 Pull Request！

捐助
--------------
如果这个项目对您有用，我们欢迎各方任何形式的捐助，也包括参与到项目代码更新或意见反馈中来。谢谢！

资金捐助：

[![donate](http://sdk.weixin.senparc.com/Images/T1nAXdXb0jXXXXXXXX_s.png)](http://sdk.weixin.senparc.com#donate)


图书众筹
--------------
扫描下方二维码参与《微信公众平台快速开发》图书众筹

[![CrowdFunding](http://sdk.weixin.senparc.com/images/crowdfunding-qrcode.png)](http://www.weiweihi.com:8080/CrowdFunding/Home)  

License
--------------
Apache License Version 2.0

```
Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the 
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
either express or implied. See the License for the specific language governing permissions 
and limitations under the License.
```
Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md
