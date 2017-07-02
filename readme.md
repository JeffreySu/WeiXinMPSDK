Senparc.Weixin —— 微信 .NET SDK [![Build Status](https://travis-ci.org/JeffreySu/WeiXinMPSDK.svg?branch=master)](https://travis-ci.org/JeffreySu/WeiXinMPSDK)
=================

Senparc.Weixin SDK 是目前使用率最高的微信 .NET SDK，也是国内最受欢迎的 .NET 开源项目之一。

目前 Senparc.Weixin 已经支持几乎所有微信平台模块和接口，同时支持 
.NET [4.0](https://github.com/JeffreySu/WeiXinMPSDK/tree/NET4.0) / 
[4.5](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer) / 
[.NET Core](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer-FullDotNet)。

立项四年多来我们一直持续更新，并会坚持做下去，感谢亲们对我们的信任和各种支持！


> 由 Jeffrey Su 亲笔撰写的微信开发图书将于 7 月中旬出版，<br>
书名：《微信开发深度解析：公众号、小程序高效开发秘籍》，可以使用微信扫描下方二维码查看最新进展：<br>

> [![CrowdFunding](http://sdk.weixin.senparc.com/images/crowdfunding-qrcode.png)](https://www.weiweihi.com:8080/CrowdFunding/Home)  
> <img src="http://sdk.weixin.senparc.com/images/book-cover-front-small-3d.png" width="300" /> <br >
> 图书出版时的代码版本快照见分支 [BookVersion1](https://github.com/JeffreySu/WeiXinMPSDK/tree/BookVersion1)。

目录
----------------
* [各模块类库](#各模块类库)
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
    * [如何处理企业微信请求？](#如何处理企业微信请求)
    * [如何处理微信开放平台请求？](#如何处理微信开放平台请求)
    * [如何使用分布式缓存？](#如何使用分布式缓存)
* [如何开发小程序](#如何开发小程序)
* [已实现功能](#已实现功能)
* [捐助](#捐助)
* [图书众筹](#图书众筹)
* [License](#license)

本库为.NET 4.5，其他.NET版本请看各自分支：

* [.NET Core + .NET 4.6.1 + .NET 4.5](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer-FullDotNet)
* [.NET 4.0](https://github.com/JeffreySu/WeiXinMPSDK/tree/NET4.0) 



各模块类库
----------------

| # | 模块功能                                            | DLL                               | Nuget                                             | .NET 4.0 			   | .NET 4.5		       | .NET Core
|---|----------------------------------------------------|-----------------------------------|----------------------------------------------------|----------------------|-----------------------|------------------------
| 1| 基础库                                               |Senparc.Weixin.dll                 | [![Senparc.Weixin][1.1]][1.2]                     | ![.NET 4.0][net40Y]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY]
| 2| 微信公众号 /<br> 微信支付 /<br> JSSDK / 摇周边<br> 等等 |Senparc.Weixin.MP.dll               | [![Senparc.Weixin.MP][2.1]][2.2]                  | ![.NET 4.0][net40Y]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY]
| 3| ASP.NET MVC 扩展<br>（.NET Framework）|Senparc.Weixin.MP.MVC.dll          | [![Senparc.Weixin.MP.MVC][3.1]][3.2]| ![.NET 4.0][net40Y]  | ![.NET 4.5][net45Y]  | ![.NET Core][coreN]
| 4| ASP.NET Core MVC <br> 扩展<br>（.NET Core）|Senparc.Weixin.MP.CoreMVC.dll      | [![Senparc.Weixin.MP.CoreMVC][11.1]][11.2]     | ![.NET 4.0][net40N]  | ![.NET 4.5][net45N] | ![.NET Core][coreY]
| 5| 微信企业号                                           |Senparc.Weixin.QY.dll              | [![Senparc.Weixin.QY][4.1]][4.2]                  | ![.NET 4.0][net40Y]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY]
| 6| 企业微信                                |Senparc.Weixin.Work.dll            |  [![Senparc.Weixin.WorkQY][12.1]][12.2]               | ![.NET 4.0][net40N]					   | ![.NET 4.5][net45Y] | ![.NET Core][coreY]		 	  
| 7| 微信开放平台                                         |Senparc.Weixin.Open.dll            | [![Senparc.Weixin.Open][6.1]][6.2]                | ![.NET 4.0][net40Y]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY]
| 8| Redis 分布式缓存                                     |Senparc.Weixin.Cache.Redis.dll     | [![Senparc.Weixin.Cache.Redis][7.1]][7.2]         | ![.NET 4.0][net40N]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY]
| 9| Memcached <br> 分布式缓存                            |Senparc.Weixin.Cache.Memcached.dll |[![Senparc.Weixin.Cache.Memcached][8.1]][8.2] | ![.NET 4.0][net40N]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY]
|10| [微信小程序 <br>（独立项目）](https://github.com/JeffreySu/WxOpen)    |Senparc.Weixin.WxOpen.dll |[![Senparc.Weixin.WxOpen][9.1]][9.2]       | ![.NET 4.0][net40N]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY] 
|11| [WebSocket <br>（独立项目）](https://github.com/JeffreySu/Senparc.WebSocket)    |Senparc.WebSocket.dll |[![Senparc.WebSocket][10.1]][10.2]   | ![.NET 4.0][net40N]  | ![.NET 4.5][net45Y]   | ![.NET Core][coreY] 

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
[11.1]: https://img.shields.io/nuget/v/Senparc.Weixin.MP.CoreMVC.svg?style=flat
[11.2]: https://www.nuget.org/packages/Senparc.Weixin.MP.CoreMVC
[12.1]: https://img.shields.io/nuget/v/Senparc.Weixin.Work.svg?style=flat
[12.2]: https://www.nuget.org/packages/Senparc.Weixin.Work

[net40Y]: https://img.shields.io/badge/4.0-Y-brightgreen.svg
[net40N]: https://img.shields.io/badge/4.0-N-lightgrey.svg
[net45Y]: https://img.shields.io/badge/4.5-Y-brightgreen.svg
[net45N]: https://img.shields.io/badge/4.5-N-lightgrey.svg
[net461Y]: https://img.shields.io/badge/4.6.1-Y-brightgreen.svg
[net461N]: https://img.shields.io/badge/4.6.1-N-lightgrey.svg
[coreY]: https://img.shields.io/badge/core-Y-brightgreen.svg
[coreN]: https://img.shields.io/badge/core-N-lightgrey.svg


* 已经支持所有微信6 API，包括自定义菜单/个性化菜单、模板信息接口、素材上传接口、群发接口、多客服接口、支付接口、微小店接口、卡券接口等等。
* 已经支持用户会话上下文（解决服务器无法使用Session处理用户信息的问题）。
* 已经全面支持微信公众号、企业号、开放平台的最新API。
* 已经支持分布式缓存及缓存策略扩展。

目前官方的API都已完美集成，除非有特殊说明，所有升级都会尽量确保向下兼容，所以已经发布的版本请放心使用或直接升级（覆盖）最新的[DLLs](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut)。


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
上述方法中的PostModel是一个包括了了Signature、Timestamp、Nonce（由微信服务器通过请求时的Url参数传入），以及AppId、Token、EncodingAESKey等一系列内部敏感的信息（需要自行传入）的实体类，同时也会在后面用
