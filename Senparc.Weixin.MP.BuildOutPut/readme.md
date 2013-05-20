Senparc.Weixin.MP.MvcExtension更新日志见：https://github.com/JeffreySu/WeiXinMPSDK/blob/master/Senparc.Weixin.MP.MvcExtension/readme.md


Senparc.Weixin.MP升级记录
----------

v0.8 /2013-5-21

添加IMessageHandler接口。


v0.7 /2013-5-14

完善通用接口http://mp.weixin.qq.com/wiki/index.php?title=通用接口文档 ，增加内置模拟Post及文件上传功能。


v0.6 /2013-5-7

优化MessageHandler：

废弃UserName，改为WeixinOpenId；

添加CancelExcute属性，以便在执行过程中及时中断处理程序；

添加`CreateResponseMessage<TR>()`方法，用于快捷生成以当前RequestMessage为基础的ResponseMessage。

v0.5 /2013-5-2

GpsHelper中添加了根据实际距离（KM）计算经度和纬度差的方法。

v0.4 /2013-5-1

EntityHelper中添加CreateResponseMessage<T>静态方法。

添加Senparc.Weixin.MP.HttpUtility.RequestUtility.IsWeixinClientRequest()方法，用于判断请求是否发起自微信客户端的浏览器。

v0.4.* /2013-5-1

版本号中的生成号和修订号开始使用.net自动编号方式。主版本和次版本决定版本功能比较大的差异。
也就是说从现在起只需要关注如0.4这两位主、次版本号，后面的2位生成号和修订号只是针对功能改进及记录编译次数，功能及方法上不会有太大变化，多数情况下可以不用同步更新。

v0.4.2 /2013-4-26

完善用户信息上下文。

MessageHandler中加入了OnExecuting和OnExecuted两个方法，分别在Execute()触发前/后运行。


v0.4.0 /2013-4-26


添加用户信息上下文，WeixinContext，可以很方便地跟踪某个用户的会话，并可以临时储存信息。

原先实现MessageHandler的类，如：

```C#
    public class MyMessageHandler : MessageHandler
```

现在需要在MessageHandler后加上“微信上下文”的泛型，如：
```C#
    public class MyMessageHandler : MessageHandler<MessageContext>
```
其中MessageContext可以是继承IMessageContext的任何子类，这里的MessageContext是SDK中的一个默认的简单实现。

v0.3.5 /2013-4-17

添加RequestMessageLink用于接收处理link类型的信息。同时MessageHandler也增加了对应的OnLinkRequest处理方法。

v0.3.4.2 /2013-4-17

修改GoogleMapHelper.GetGoogleStaticMap()方法，将List<Markers> markersList类型改为IList<Markers>。


v0.3.4 /2013-4-8

将IRequestMessageBase及IResponseMessageBase下的MsgType设为只读，这样所有子类的MsgType都会在开发的时候被确定下来，不用初始化之后再重复设置。

v0.3 /2013-4-5

添加MessageHandler处理类，简化二次开发时对信息的处理流程。

v0.3.1 /2013-03-19


新增自定义菜单等相关类型。


为access_token验证做好准备，提供简便的Http请求和Json转换方法。
