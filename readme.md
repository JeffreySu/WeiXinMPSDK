微信公众平台C# SDK：Senparc.Weixin.MP.dll
=================


已经支持所有微信4.5 API，支持语音接收及返回音乐格式。 已经支持关注（订阅）事件推送，尚未发布的消息推送功能可以通过项目中的单元测试进行开发。

目前官方的API都已完美集成，更多方便开发的扩展功能还在陆续添加中。除非有特殊说明，所有升级都会尽量确保向下兼容，所以已经发布的版本请放心使用或直接升级（覆盖）最新的[Senparc.Weixin.MP.dll](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.BuildOutPut)。

微信公众平台SDK：Senparc.Weixin.MP beta

资源
----------------
官网地址：http://weixin.senparc.com/

源代码及最新更新：https://github.com/JeffreySu/WeiXinMPSDK

SDK技术交流QQ群：300313885

新浪微博：[@苏震巍](http://weibo.com/jeffreysu1984)

###关注测试账号：
[![image]](http://weixin.senparc.com/)  
[image]: http://weixin.senparc.com/Images/qrcode.jpg
PS：请大家努力关注！达不到一定关注人数无法申请开通自定义菜单的内侧功能。

项目文件夹说明
--------------
> Senparc.Weixin.MP.BuildOutPut：Senparc.Weixin.MP.dll发布文件夹

> Senparc.Weixin.MP.Sample：可以直接发布使用的Demo（ASP.NET MVC 4.0，需要.NET 4.0）

> Senparc.Weixin.MP.Sample.WebForms：可以直接发布使用的Demo（ASP.NET WebForms，需要.NET 3.5）

> Senparc.Weixin.MP：Senparc.Weixin.MP.dll源代码

Senparc.Weixin.MP.Sample中的关键代码说明（这是MVC项目，WebForms项目见Weixin.aspx）
--------------
###/Controllers/WeixinController.cs
下面的Token需要和微信公众平台后台设置的Token同步，如果经常更换建议写入Web.config等配置文件：
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
public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
{
    if (CheckSignature.Check(signature, timestamp, nonce, Token))
    {
        return Content(echostr);//返回随机字符串则表示验证通过
    }
    else
    {
        return Content("failed:" + signature + "," + MP.CheckSignature.GetSignature(timestamp, nonce, Token));
    }
}
```
下面这个Action（Post）用于接收来自微信服务器的Post请求（通常由用户发起），这里的if必不可少，之前的Get只提供微信后台保存Url时的验证，每次Post必须重新验证，否则很容易伪造请求。
```C#
/// <summary>
/// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
/// </summary>
[HttpPost]
[ActionName("Index")]
public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
{
    if (!CheckSignature.Check(signature, timestamp, nonce, Token))
    {
        return Content("参数错误！");
    }
    ...
}
```
###如何处理微信请求？
Senparc.Weixin.MP提供了2中处理请求的方式，[传统方法](https://github.com/JeffreySu/WeiXinMPSDK/wiki/处理微信信息的常规方法)及使用[MessageHandler](https://github.com/JeffreySu/WeiXinMPSDK/wiki/%E5%A6%82%E4%BD%95%E4%BD%BF%E7%94%A8MessageHandler%E7%AE%80%E5%8C%96%E6%B6%88%E6%81%AF%E5%A4%84%E7%90%86%E6%B5%81%E7%A8%8B)处理方法（推荐）。上面两个方法在wiki中已经有比较详细的说明，这里简单举例MessageHandler的处理方法。

MessageHandler的处理流程非常简单：
``` C#
[HttpPost]
[ActionName("Post")]
public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
{
    if (!CheckSignature.Check(signature, timestamp, nonce, Token))
    {
        return Content("参数错误！");
    }

    var messageHandler = new CustomerMessageHandler(Request.InputStream);//接收消息
    messageHandler.Execute();//执行微信处理过程
    return Content(messageHandler.ResponseDocument.ToString());//返回数据
}
```
整个消息的接收、处理、返回分别只需要一行代码。

上述代码中的CustomerMessageHandler是一个自定义的类，继承自Senparc.Weixin.MP.MessageHandler.cs。MessageHandler是一个抽象类，包含了执行各个请求的抽象方法，我们只需要在自己创建的CustomerMessageHandler中逐个实现这些方法就可以了。刚建好的CustomerMessageHandler.cs如下：
```C#
using System;
using System.IO;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Sample.CustomerMessageHandler
{
    public class MyMessageHandler:MessageHandler
    {
        public MyMessageHandler(Stream inputStream)
            : base(inputStream)
        {

        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            throw new NotImplementedException();
        }

        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            throw new NotImplementedException();
        }

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
          var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);//v0.3版本之前的非泛型方法仍然有效
          responseMessage.Content =
              string.Format(
                  "您刚才发送了文字信息：{0}",
                  requestMessage.Content);
          return responseMessage;
      }
```
这样CustomerMessageHandler在执行messageHandler.Execute()的时候，如果发现请求信息的类型是文本，会自动调用以上代码，并返回代码中的responseMessage作为返回信息。responseMessage可以是IResponseMessageBase接口下的任何类型（包括文字、新闻、多媒体等格式）。

开原协议
--------------
MIT
