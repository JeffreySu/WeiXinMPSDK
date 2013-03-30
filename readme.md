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

> Senparc.Weixin.MP.Sample：可以直接发布使用的Demo（ASP.NET MVC4.0）

> Senparc.Weixin.MP：Senparc.Weixin.MP.dll源代码

Senparc.Weixin.MP.Sample中的关键代码说明
--------------
###/Controllers/WeixinController.cs
    public readonly string Token = "weixin";
这里的Token需要和微信公众平台后台设置的Token同步，如果经常更换建议写入Web.config等配置文件。

###
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
这个Action用于接收并返回微信后台Url的验证结果，无需改动。地址如：http://domain/Weixin或http://domain/Weixin/Index

###
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
这个Action用于接收来自微信服务器的Post请求（通常由用户发起），这里的if必不可少，之前的Get只提供微信后台保存Url时的验证，每次Post必须重新验证，否则很容易伪造请求。
