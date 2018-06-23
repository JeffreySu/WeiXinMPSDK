<img src="https://sdk.weixin.senparc.com/images/senparc-logo-500.jpg" />

Senparc.Weixin —— Wechat .NET SDK
=================
[![Build status](https://ci.appveyor.com/api/projects/status/eshwtou0h6xfwa1q/branch/master?svg=true)](https://ci.appveyor.com/project/JeffreySu/weixinmpsdk/branch/master)
[![Build Status](https://travis-ci.org/JeffreySu/WeiXinMPSDK.svg?branch=master)](https://travis-ci.org/JeffreySu/WeiXinMPSDK)
[![NuGet](https://img.shields.io/nuget/dt/Senparc.Weixin.MP.svg)](https://www.nuget.org/packages/Senparc.Weixin.MP)
[![GitHub commit activity the past week, 4 weeks, year](https://img.shields.io/github/commit-activity/4w/JeffreySu/WeiXinMPSDK.svg)](https://github.com/JeffreySu/WeiXinMPSDK/commits/master)
[![license](https://img.shields.io/github/license/JeffreySu/WeiXinMPSDK.svg)](http://www.apache.org/licenses/LICENSE-2.0)

[[中文]](readme.md)

> Wechat is the most famous IM APP in China which has more than 1 billion active users and more than ten million Official Accounts.

By using Senparc.Weixin SDK, you can develop all wechat platform applications, including Official Account, Mini Programm, Mini Game,  Enterprise Account, Open Platform, Wechat Pay, JS-SDK, Wechat IoT/Bluetooth, etc. 

Now, Senparc.Weixin has been supported almost all of the API for Wechat's all modules. It supports mutipule .Net targets [.NET 3.5 / 4.0 / 4.5 / .NET Core 2.0 / .NET Core 2.1](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer).

Senparc.Weixin SDK is the most widly used .NET Wechat SDK. Also it is one of the most popular .NET open source project in China.

For more than five years, we have been keeping the project constantly updated, share the complete source code and design ideas without reservation. Hopefully more people will benefit from it, understand and disseminate the spirit of open source. Grateful to the friends who helped us along the way!

If you like and hope us to continue to optimize this project, please give us a Star:)

<img src="https://sdk.weixin.senparc.com/images/SenparcRobotsnapshoot.jpg" width="350" align="right">


Index
----------------

* [SDK Modules](#sdk-modules)
* [Resources](#resources)
* [:book: Senparc official tutorials](#senparc-official-tutorials)
* [Contribute Code](#contribute-code)
* [Develop with .net Core](#develop-with-net-core)
* [Follow Demo Official Account(SenparcRobot)](#follow-demo-official-accountsenparcrobot)
* [Project folder description (under src folder)](#project-folder-description-under-src-folder)
* [Demo folder description (under Samples folder)](#demo-folder-description-under-samples-folder)
* [Senparc.Weixin.MP.Sample Key Code](#senparcweixinmpsample-key-code)
    * [/Controllers/WeixinController.cs](#controllersweixincontrollercs)
    * [How to handle WeChat Official Account request?](#how-to-handle-wechat-official-account)
* [Use Nuget to install the project](#use-nuget-to-install-the-project)
    * [How to handle WeChat Official Account?](#how-to-handle-wechat-official-account)
    * [How to handle WeChat Mini Program (include Mini Game)?](#how-to-handle-wechat-mini-program-include-mini-game)
    * [How to enhance the functionality of ASP.NET MVC project?](#how-to-enhance-the-functionality-of-aspnet-mvc-project)
    * [How to handle WeChat Corporate Account?](#how-to-handle-wechat-corporate-account)
    * [How to handle Corporate Wechat?](#how-to-handle-corporate-wechat)
    * [How to handle Wechat Open Platform?](#how-to-handle-wechat-open-platform)
    * [How to use distributed cache?](#how-to-use-distributed-cache)
* [How to develop Mini Program?](#how-to-develop-mini-program)
<!--* [已实现功能](#已实现功能)-->
* [Branch Description](#branch-description)
* [Thanks for Contributors](#thanks-for-contributors)
* [Donate](#donate)
* [License](#license)

The library contains the source code (the Core logic is exactly the same) that includes .Net 3.5/4.0/4.5/.NET Core 2.0/2.1.

* Use Visual Studio 2017 to open the Demo (support all versions)
: [Senparc.Weixin.MP.Sample.vs2017.sln](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer/src/Senparc.Weixin.MP.Sample.vs2017)
* Use other versions of Visual Studio to open the Demo (support .net 4.5 only) :
[Senparc.Weixin.MP.Sample.sln](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer/src/Senparc.Weixin.MP.Sample)
* Open Demo with Visual Studio 2010 SP1 (support.net 4.5 only) :
[Senparc.Weixin.MP.Sample.vs2010sp1.sln](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer/src/Senparc.Weixin.MP.Sample)


SDK Modules
----------------

| # | Module Libraries                                         | DLL                                             | Nuget & Support .NET Versions 
|---|----------------------------------------------------|-------------------------------------------------|--------------------------------------
| 1| Base Library                                               |Senparc.Weixin.dll                               | [![Senparc.Weixin][1.1]][1.2]  ![.NET 3.5][net35Y]    ![.NET 4.0][net40Y]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]
| 2| Official Account /<br> TenPay /<br> JSSDK <br> ect. |Senparc.Weixin.MP.dll                         | [![MP][2.1]][2.2]  ![.NET 3.5][net35Y]    ![.NET 4.0][net40Y]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]
| 3| ASP.NET MVC Extension<br>.NET Framework +<br> Core               |Senparc.Weixin.MP.MVC.dll                        | [![MP.MVC][3.1]][3.2]  ![.NET 3.5][net35N]    ![.NET 4.0][net40Y]   ![.NET 4.5][net45Y]      ![.NET Core 2.x][core20Y]
| 4| Corporate Account                                           |Senparc.Weixin.QY.dll                            | [![QY][4.1]][4.2]   ![.NET 3.5][net35Y] ![.NET 4.0][net40Y] ![.NET 4.5][net45Y] ![.NET Core 2.x][core20Y]
| 5| Corporate Wechat                                             |Senparc.Weixin.Work.dll                          | [![Work][5.1]][5.2]  ![.NET 3.5][net35Y]    ![.NET 4.0][net40Y]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]
| 6| Open Platform                                         |Senparc.Weixin.Open.dll                          | [![Open][6.1]][6.2]  ![.NET 3.5][net35Y]    ![.NET 4.0][net40Y]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]
| 7| Redis Distributed Cache                                     |Senparc.Weixin.Cache.<br>Redis.dll               | [![Cache.Redis][7.1]][7.2]   ![.NET 3.5][net35N]    ![.NET 4.0][net40N]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]
| 8| Memcached <br> Distributed Cache                            |Senparc.Weixin.Cache.<br>Memcached.dll           | [![Cache.Memcached][8.1]][8.2]   ![.NET 3.5][net35N]    ![.NET 4.0][net40N]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]
| 9| [Mini Program <br>(s.p. Mini Game)<br>(indep. proj.)](https://github.com/JeffreySu/WxOpen)    |Senparc.Weixin.WxOpen.dll       | [![WxOpen][9.1]][9.2]  ![.NET 3.5][net35N]    ![.NET 4.0][net40Y]  ![.NET 4.5][net45Y] ![.NET Core 2.x][core20Y] 
|10| [WebSocket <br>(indep. proj.)](https://github.com/JeffreySu/Senparc.WebSocket)    |Senparc.WebSocket.dll | [![Senparc.WebSocket][10.1]][10.2]   ![.NET 3.5][net35N]    ![.NET 4.0][net40N]   ![.NET 4.5][net45Y]    ![.NET Core 2.x][core20Y]


| ![.NET 3.5][net35Y] | ![.NET 4.0][net40Y] | ![.NET 4.5][net45Y] | ![.NET Core 2.x][core20Y] |
|--|--|--|--|
| .NET 3.5            | .NET 4.0            | .NET 4.5            |  .NET Core 2.0 + 2.1       |


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

[net35Y]: https://img.shields.io/badge/3.5-Y-brightgreen.svg
[net35N]: https://img.shields.io/badge/3.5-N-lightgrey.svg
[net40Y]: https://img.shields.io/badge/4.0-Y-brightgreen.svg
[net40N]: https://img.shields.io/badge/4.0-N-lightgrey.svg
[net40N-]: https://img.shields.io/badge/4.0----lightgrey.svg
[net45Y]: https://img.shields.io/badge/4.5-Y-brightgreen.svg
[net45N]: https://img.shields.io/badge/4.5-N-lightgrey.svg
[net45N-]: https://img.shields.io/badge/4.5----lightgrey.svg
[net461Y]: https://img.shields.io/badge/4.6.1-Y-brightgreen.svg
[net461N]: https://img.shields.io/badge/4.6.1-N-lightgrey.svg
[coreY]: https://img.shields.io/badge/core-Y-brightgreen.svg
[coreN]: https://img.shields.io/badge/core-N-lightgrey.svg
[coreN-]: https://img.shields.io/badge/core----lightgrey.svg
[core20Y]: https://img.shields.io/badge/core2.x-Y-brightgreen.svg
[core20N]: https://img.shields.io/badge/core2.x-N-lightgrey.svg

* Supported all Wechat 6 APIs, includes customize menu / personalized  menu, template message, material APIs, group message, multi-customer service, TenPay, MerChant, cards APIs, ect.
* Supported user dialogue context, to solve application service can not use Session to handle users' dialogues.
* Fully supported latest APIs for Wechat Official Account, Corporate Account(Corporate Wechat), Open Platform.
* Supported Distributed Cache Strategy with high scalability.

> All updates will ensure downward compatibility unless otherwise specified. So you can cover new DLLs directly or use Nuget to manage packages(highly recommend).

resources
----------------
1. Senparc.Weixin SDK Official Site: http://weixin.senparc.com/
2. Demo Site：http://sdk.weixin.senparc.com/
3. Blog tutorial: http://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html
4. WeChat technology exchange community: http://weixin.senparc.com/QA
5. Online editing tool for Customize menu: http://sdk.weixin.senparc.com/Menu
6.  Online test tool for Messages: http://sdk.weixin.senparc.com/SimulateTool
7. Online test tool for Cache：http://sdk.weixin.senparc.com/Cache/Test
8. chm documentation download：http://sdk.weixin.senparc.com/Document
9. Source code and the latest updates
: https://github.com/JeffreySu/WeiXinMPSDK
10. WeChat development resources: https://github.com/JeffreySu/WeixinResource
11. *Depth Analysis of WeChat Development* reading system：https://book.weixin.senparc.com
12. Buy *Depth Analysis of WeChat Development*：[https://item.jd.com/12220004.html](https://book.weixin.senparc.com/book/link?code=github-homepage-resource-en)
13. Video Course：[https://github.com/JeffreySu/WechatVideoCourse](https://github.com/JeffreySu/WechatVideoCourse)


* Technical communication QQ group:

> `14th Group(Video Course Student Group)：588231256`<br>
> `15th Group(Official Account): 289181996`<br>
> `10th Group(Distributed Cache): 246860933`<br>
> `12th Group(Mini Program): 108830388`<br>
> `13th Group(Open Platform): 183424136`<br>
> *`the following group is full:`*<br>
> 1st group：300313885(full)，2nd group：293958349(full)，3rd group：342319110(full)<br>
> 4th group：372212092(full)，5th group: 377815480(full)，6th group：425898825(full)<br>
> 7th group：482942254(full)，8th group：106230270(full)，9th group：539061281(full)<br>
> 11th group：553198593(full)

* Business contact QQ：498977166

<!-- * 新浪微博：[@苏震巍](http://weibo.com/jeffreysu1984) -->

If this project is useful to you, we welcome any form of donations from all parties, including participation in project code updates or feedback. Thank you!



donate: [Enter](http://sdk.weixin.senparc.com#donate)


Senparc official tutorials
----------------

<img src="https://sdk.weixin.senparc.com/images/book-cover-front-small-3d.jpg" width="400" align="right">


> By Jeffrey Su and Senparc team took 2 years to complete the development of WeChat book have been published, the book's full name is: *Depth Analysis of WeChat Development: the efficient development of the Official Account and Mini Program*, the auxiliary reading system has been on the line: [BookHelper](http://book.weixin.senparc.com)。<br>
> Welcome to buy genuine books:[【Buy】](https://book.weixin.senparc.com/book/link?code=github-homepage)<br>
> The branch of code snapshot for the book published version [BookVersion1](https://github.com/JeffreySu/WeiXinMPSDK/tree/BookVersion1)。



### Follow Demo Official Account(SenparcRobot)：
|Senparc Helper Official Account|Senparc Helper Mini-Program| BookHelper |
|--|--|--|
| <img src="https://sdk.weixin.senparc.com/Images/qrcode.jpg" width="258" /> | <img src="https://sdk.weixin.senparc.com/Images/SenparcRobot_MiniProgram.jpg" width="258" /> | <img src="https://sdk.weixin.senparc.com/Images/qrcode-bookhelper.jpg" width="258" /> |


## Develop with .net Core

> Current branch including .NET Framework 4.5 / 4.6.1 及 .NET Core 2.0 / 2.1 full version codes.<br>
> .NET Framework 4.5 Demo under `/src/Senparc.Weixin.MP.Sample` directory, <br>
> .NET Core 2.0 Demo under `/src/Senparc.Weixin.MP.Sample.vs2017` directory.<br>
> Attention: the source code of the Senparc.Weixin SDK library referenced by the above two Demo is exactly the same, it will automatically select the output version according to the conditions when compiling and running.

> All of the following introduction use the example of the .NET Framework 4.5.

## Contribute Code

> If you need to use or modify the program source code, recommended to Fork. We also welcome you to modify the generic version of Pull Request.

1. Fork
2. Create your own branch (`git checkout -b my-new-feature`)
3. Submit your changes (`git commit -am'Added some feature'`)
4. Submit modify records to a remote warehouse (`git push origin `git` my-new-feature`)
5. And then go to the `my-new-feature` branch of the `git` on GitHub site, launch Pull Request
(Please refer to `Developer` branch, not directly submitted to the `master` branch)


## Project folder description (under src folder)

| Folder | Description |
|--------|--------|
|[Senparc.WebSocket](src/Senparc.WebSocket)									|WebSocket Module|
|[Senparc.Weixin.Cache](src/Senparc.Weixin.Cache)							|Senparc.Weixin.Cache.Memcached.dll 、 Senparc.Weixin.Cache.Redis.dll Distributed Cache extension solutions|
|[Senparc.Weixin.MP.BuildOutPut](src/Senparc.Weixin.MP.BuildOutPut	)		|DLLs output folder|
|[Senparc.Weixin.MP.MvcExtension](src/Senparc.Weixin.MP.MvcExtension)		|Senparc.Weixin.MP.MvcExtension.dll source code, extension for ASP.NET MVC |
|[Senparc.Weixin.MP](src/Senparc.Weixin.MP)									|Senparc.Weixin.MP, Official Account SDK source code|
|[Senparc.Weixin.Open](src/Senparc.Weixin.Open)								|Senparc.Weixin.Open.dll, 3rd Open Platform SDK source code|
|[Senparc.Weixin.QY](src/Senparc.Weixin.QY)									|Senparc.Weixin.QY.dll, Corporate Account SDK source code|
|[Senparc.Weixin.Work](src/Senparc.Weixin.Work)								|Senparc.Weixin.Work.dll Corporate Wechat SDk  source code|
|[Senparc.Weixin.WxOpen](src/Senparc.Weixin.WxOpen)							|Senparc.Weixin.WxOpen.dll Mini Program SDK source code. Include Mini Game.|
|[Senparc.Weixin](src/Senparc.Weixin)										|all Senparc.Weixin.[x].dll base library  source code|


## Demo folder description (under Samples folder)

| Folder | Description |
|--------|--------|
|[Senparc.Weixin.MP.Sample](Samples/Senparc.Weixin.MP.Sample)						  |Demo, can be released directly(.NET Framework 4.5 + ASP.NET MVC)|
|[Senparc.Weixin.MP.Sample.WebForms](Samples/Senparc.Weixin.MP.Sample.WebForms)		  |Demo, can be released directly(.NET Framework 4.5 + + ASP.NET WebForms)|
|[Senparc.Weixin.MP.Sample.vs2017](Samples/Senparc.Weixin.MP.Sample.vs2017)			  |Demo, can be released directly(.NET Core 2.0 + MVC)|


Senparc.Weixin.MP.Sample Key Code
--------------
> Note: This is MVC projct, you can learn corresponding file in  WebForm project.

### /Controllers/WeixinController.cs

The following Token needs to be synchronized with the Token synchronization in the background Settings of the WeChat Official Account Site(https://mp.weixin.qq.com). You can set the Token string in database or config file like `Web.cofing`. It's strongly recommend to use complex string, because the request is easy to forge while the Token is cracked.

```C#
public readonly string Token = "weixin";
```

The following Action (Get) is used to receive and return the validation results of the WeChat background Url without any changes. Address such as http://domain/Weixin  or http://domain/Weixin/Index
```C#
/// <summary>
/// WeChat background validation Url (Get request), such as http://weixin.senparc.com/weixin
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
            "If you see this message in browser, that means this url address can be used in Wechat Official Account background setting for validation Url. Please note that the Token is consistent.");
    }
}
```
In above methods, PostModel is an entity including Signature, Timestamp, Nonce (by WeChat server via the incoming request Url parameter), AppId and Token, EncodingAESKey and a series of internal sensitive information (need to pass it in) entity class. PostModel also used in the rear.


The following Action (Post) used to receive Post request from WeChat server (usually initiated by the user), if necessary, here before you Get only provide WeChat background save Url validation, every Post must revalidate, otherwise it's easy to forge the request.

```C#
/// <summary>
/// After the user sends the message, the WeChat platform automatically posts a request to this place and waits for the response XML
/// </summary>
[HttpPost]
[ActionName("Index")]
public ActionResult Post(PostModel postModel)
{
    if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content("Parameter error!");
    }
    ...
}
```
### How to handle WeChat Official Account request?

Senparc.Weixin.MP provides two ways to process requests, [traditional methos](https://github.com/JeffreySu/WeiXinMPSDK/wiki/处理微信信息的常规方法) and [MessageHandler](https://github.com/JeffreySu/WeiXinMPSDK/wiki/%E5%A6%82%E4%BD%95%E4%BD%BF%E7%94%A8MessageHandler%E7%AE%80%E5%8C%96%E6%B6%88%E6%81%AF%E5%A4%84%E7%90%86%E6%B5%81%E7%A8%8B) (recommended). 

The above two methods have been described in more detail in the wiki, which is a simple example of the MessageHandler method.


The MessageHandler process is very simple:
``` C#
[HttpPost]
[ActionName("Index")]
public ActionResult Post(PostModel postModel)
{
    if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
    {
        return Content("Parameter error!");
    }

    postModel.Token = Token;
    postModel.EncodingAESKey = EncodingAESKey;//Be consistent with your Settings in the background
    postModel.AppId = AppId;//Be consistent with your Settings in the background


    var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//Receive messages (first step)

    messageHandler.Execute();//Processing (step 2)

    return new FixWeixinBugWeixinResult(messageHandler);//Return (step 3)
}
```
In addition to the postModel assignment, the receipt (step 1), processing (step 2), and return (step 3) will only need one line of code.

CustomMessageHandler in the code above is a custom class that inherits from Senparc.Weixin.MP.MessageHandler.cs. MessageHandler is an abstract class that contains the request perform a variety of different types of abstract methods (such as text, voice, location, pictures, etc.), we only need to create your own CustomMessageHandler in each of these methods is implemented.The newly built CustomMessageHandler.cs is as follows:

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
            //ResponseMessageText can also be other types like News
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "This message is from DefaultResponseMessage。";
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
        
        //More OnXX methods that are not overridden will return the result in DefaultResponseMessage by default.
        ....
    }
}
```

OnTextRequest and OnVoiceRequest correspond to different request types, such as receiving text and voice.

For example, we need to respond to text type requests, just perfect the OnTextRequest method:
```C#
      public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
      {
          //TODO: the logic can be dealt with to the Service details, reference OnLocationRequest method or /Service/LocationSercice.cs
          var responseMessage = CreateResponseMessage<ResponseMessageText>();
          responseMessage.Content = string.Format("You just sent the message：{0}", requestMessage.Content);
          return responseMessage;
      }
```

In this way, when the CustomMessageHandler executes the Messagehandler.execute(), it will automatically call the above code and return the ResponseMessage in the code to return the information if it finds that the type of the request information is Text. ResponseMessage can be any type under the IResponseMessageBase interface (including text, news, multimedia, etc.).

Starting with v0.4.0, the MessageHandler adds support for user Session context, which can be used to resolve defects on the server that can't use Session to manage user sessions. Details：[User context WeixinContext and MessageContext](https://github.com/JeffreySu/WeiXinMPSDK/wiki/%E7%94%A8%E6%88%B7%E4%B8%8A%E4%B8%8B%E6%96%87WeixinContext%E5%92%8CMessageContext)

Use Nuget to install the project
--------------
### How to handle WeChat Official Account?

* Nuget Address: https://www.nuget.org/packages/Senparc.Weixin.MP

* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.MP
```


###  How to handle WeChat Mini Program (include Mini Game)?

Senparc.Weixin.WxOpen encapsulates the message and API of WeChat mini programs, keeping the development process of the Official Account request consistent. This module also support Mini Game.

* Nuget Address: https://www.nuget.org/packages/Senparc.Weixin.WxOpen

* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.WxOpen
```

### How to enhance the functionality of ASP.NET MVC Project?

Senparc.Weixin.MP.MVC has done more optimization for ASP.NET MVC project, including convenient browser environment judgment, official bug fix, etc.

* Nuget Address: https://www.nuget.org/packages/Senparc.Weixin.MP.MVC

* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.MP.MVC
```

### How to handle WeChat Corporate Account?

Senparc.Weixin.QY.dll for `Corporate Account` encapsulation were conducted for the relevant functions, operation process remain the same with WeChat Official Account SDK (Senparc.Weixin.MP) .

* Nuget Address: https://www.nuget.org/packages/Senparc.Weixin.QY

* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.QY
```

> Note: QY has been stopped updating with the WeChat Corporate Account and has been seamlessly ported to Work (Corporate WeChat).


### How to handle Corporate Wechat?

Senparc.Weixin.Work.dll for `Corporate Wechat` encapsulation were conducted for the relevant functions, operation process remain the same with WeChat Official Account SDK (Senparc.Weixin.MP) and WeChat Corporate Account (Senparc.Weixin.QY.dll).

* Nuget Address: https://www.nuget.org/packages/Senparc.Weixin.Work

* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.Work
```


### How to handle Wechat Open Platform?


Senparc.Weixin.Open.dll is encapsulatied all Open Platform APIs , message operation process remain the same with WeChat Official Account SDK (Senparc.Weixin.MP), some other special message process please read the official document, then compares Demo in the Senparc.Weixin.MP.Sample project.

* Nuget Address: https://www.nuget.org/packages/Senparc.Weixin.Open

* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.Open
```


### How to use distributed cache?

Senparc. Weixin SDK provides the perfect caching policy interface, use the default native cache implementation, it also provides a Redis and Memcached expansion plans, you can also add your own caching strategies according to the same rules.


* Redis Cache Extension package Nuget address：https://www.nuget.org/packages/Senparc.Weixin.Cache.Redis
* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.Senparc.Weixin.Cache.Redis
```

* Memcached Cache Extension package Nuget address：https://www.nuget.org/packages/Senparc.Weixin.Cache.Memcached
* Package Manager Command:
```
PM> Install-Package Senparc.Weixin.Senparc.Weixin.Cache.Memcached
```


How to develop Mini Program?
--------------
The back-end architecture of the mini program is highly consistent with the Official Account,
Only use the Nuget installation[Senparc.Weixin.WxOpen](https://www.nuget.org/packages/Senparc.Weixin.WxOpen) to start your Mini Program develop.

Senparc.Weixin.WxOpen currently contains all the information processing, AccessToken management, template message, QR code generation, etc.

<!--
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
>       - [x] 长链接转短链接接口
>       - [x] 微信认证事件推送
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
-->

 Welcome developers to Pull Request for modules that are not completed or need to be added.


Branch Description
--------------

|  Branch      |     Description         
|-----------|---------------
| master    | Officially published main branch, usually this branch is stable, can be used in production environment.
| Developer | 1, development branch, the branch for the Beta version, usually we submit the new version to this branch first, the stable version will push to the master branch. If you want to sneak peek in new function, you can use this branch.<br> 2, this branch is compatible with the.net 4.5 /.net core /.net core 2.0 version, and it is recommended that Pull Request to this branch, not master

| BookVersion1 | this branch is code snapshot for book *[Depth Analysis of WeChat Development](https://book.weixin.senparc.com/book/link?code=github-homepage2)* .
| DotNET-Core_MySQL | this branch shows the integration with [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql) in .NET Core environment.
| NET4.0     | Support for .Net 4.0 only, this branch has stopped updating in 2017. The latest code of .Net 4.0 is updated with the master/Developer branch

| NET3.5     | Support for.Net 3.5 only, which stopped updating in 2015. The latest code is updated with the master/Developer branch
| Developer-Senparc.SDK | This branch is used only for the Senparc team internal test, you can ignore this one.



Thanks for Contributors
--------------
Thanks to the developers who have contributed to this project, you have not only perfected this project, but also made a contribution to Open Source Enterprise. Thank you!
[Click Here](https://github.com/JeffreySu/WeiXinMPSDK/blob/master/Contributors.md) to see the list.

Donate
--------------
If this project is useful to you, we welcome any form of contributions, including participation in project code updates or feedback.Thank you!

Donate:

[![donate](http://sdk.weixin.senparc.com/Images/T1nAXdXb0jXXXXXXXX_s.png)](http://sdk.weixin.senparc.com#donate)



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
