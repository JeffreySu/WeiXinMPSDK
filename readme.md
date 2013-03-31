微信公众平台C# SDK：Senparc.Weixin.MP.dll
=================


已经支持所有微信4.5 API，支持语音接收及返回音乐格式。 已经支持关注（订阅）事件推送，尚未发布的消息推送功能可以通过项目中的单元测试进行开发。

目前官方的API都已完美集成，更多方便开发的扩展功能还在陆续添加中。除非有特殊说明，所有升级都会尽量确保向下兼容，所以已经发布的版本请放心使用或直接升级（覆盖）最新的[Senparc.Weixin.MP.dll](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.BuildOutPut)。

微信公众平台SDK：Senparc.Weixin.MP beta

资源
----------------
官网地址：http://weixin.senparc.com/

详细说明：http://www.cnblogs.com/szw/archive/2013/01/13/senparc-weixin-mp-sdk.html

源代码及最新更新：https://github.com/JeffreySu/WeiXinMPSDK

SDK技术交流QQ群：300313885

新浪微博：[@苏震巍](http://weibo.com/jeffreysu1984)

###关注测试账号：
[![image]](http://weixin.senparc.com/)  
[image]: http://weixin.senparc.com/Images/qrcode.jpg

项目文件夹说明
--------------
> Senparc.Weixin.MP.BuildOutPut：Senparc.Weixin.MP.dll发布文件夹

> Senparc.Weixin.MP.Sample：可以直接发布使用的Demo（ASP.NET MVC 4.0）

> Senparc.Weixin.MP：Senparc.Weixin.MP.dll源代码

Senparc.Weixin.MP.Sample中的关键代码说明
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

###如何处理微信POST请求？
###
只需要在Action中使用RequestMessageFactory.GetRequestEntity(doc)，就能得到微信发来的所有请求：
```C#
XDocument doc = XDocument.Load(Request.InputStream);
var requestMessage = RequestMessageFactory.GetRequestEntity(doc);
```
如果你不需要得到XML这个“原始数据”，那么只需一行：
```C#
var requestMessage = RequestMessageFactory.GetRequestEntity(Request.InputStream);
```
###如何响应不同类型的请求？
通过requestMessage.MsgType分析请求的类型，并作出不同回应，如：
```C#
switch (requestMessage.MsgType)
{
    case RequestMsgType.Text://文字类型
        {
            //TODO:交给Service处理具体信息，参考/Service/EventSercice.cs 及 /Service/LocationSercice.cs
            var strongRequestMessage = requestMessage as RequestMessageText;
            var strongresponseMessage =
                     ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Text) as
                     ResponseMessageText;
            strongresponseMessage.Content =
                string.Format(
                    "您刚才发送了文字信息：{0}\r\n您还可以发送【位置】【图片】【语音】信息，查看不同格式的回复。\r\nSDK官方地址：http://weixin.senparc.com",
                    strongRequestMessage.Content);
            responseMessage = strongresponseMessage;
            break;
        }
    case RequestMsgType.Location://地理位置
        ...
        break;
    case RequestMsgType.Image://图片
        ...
        break;
        case RequestMsgType.Voice://语音
        ...
        break;
    default:
        throw new ArgumentOutOfRangeException();
}
```
上述代码中，当确定requestMessage.MsgType为RequestMsgType.Text时，可以大胆使用转换（由RequestMessageFactory负责自动完成）：
```C#
var strongRequestMessage = requestMessage as RequestMessageText;
```
其他类型以此类推。
###如何生成要返回的数据？
当需要返回数据时，只需要这样做：
```C#
var strongresponseMessage = ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Text) as ResponseMessageText;
```
ResponseMessageBase.CreateFromRequestMessage()方法负责生成对应ResponseMsgType类型的响应类型实例，其中经过了对换收/发方地址（OpenID）、定义创建时间等一系列自动操作。

其中，requestMessage是上一步中获取到的微信服务器请求数据，ResponseMsgType.Text是返回数据类型，可以是文字、新闻（图片）、语音、音乐等。
ResponseMessageText类型和ResponseMsgType.Text对应，其他类型以此类推（由ResponseMessageFactory负责自动完成）。


###如何把结果返回给微信服务器？
第一步：把ResponseMessage生成XML（由于微信的个别特殊机制，不能简单序列化）：
```C#
var responseDoc = MP.Helpers.EntityHelper.ConvertEntityToXml(responseMessage);
```
第二步：在Action中直接返回responseDoc（XDocument类型）的XML字符串。
```C#
return Content(responseDoc.ToString());
```
如果你不需要responseDoc这个XML“中间数据”，那么以上两步只需要换做一行（加上using Senparc.Weixin.MP.Helpers）：
```C#
return Content(responseMessage.ConvertEntityToXmlString());
```
    
至此整个响应过程结束。
