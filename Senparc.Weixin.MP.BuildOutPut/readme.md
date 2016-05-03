DLL说明
----------

dll包含在此目录中的.NET版本文件夹内（如.net4.5或.net4.0），对应不同.NET版本的类库，如果需要下载其他版本的，只需要切换到其他对应分支即可。

| # | 模块功能 | DLL |
|--------|--------|--------|
| 1 | 基础库 | Senparc.Weixin.dll |
| 2 | 微信公众号 / 微信支付 / JSSDK / 摇周边 / 等等 | Senparc.Weixin.MP.dll  |
| 3 | ASP.NET MVC 扩展 | Senparc.Weixin.MP.MVC.dll |
| 4 | 微信企业号 | Senparc.Weixin.QY.dl |
| 5 | 微信开放平台 | Senparc.Weixin.Open.dll |
| 6 | Redis 分布式缓存 | Senparc.Weixin.Cache.Redis.dll |
| 7 | Memcached 分布式缓存 | Senparc.Weixin.Cache.Memcached.dll |


Senparc.Weixin.MP.MvcExtension更新日志见：https://github.com/JeffreySu/WeiXinMPSDK/blob/master/Senparc.Weixin.MP.MvcExtension/readme.md


Senparc.Weixin.MP.dll升级记录
----------

v6.0 之后的升级记录见：http://www.weiweihi.com/ApiDocuments/WeixinMpSdk/48

v5.10.0 /2014-5-16

添加对外请求超时设置

v5.9.0 /2014-5-10

添加多客服接口

v5.8.0 /2014-4-19

添加模板信息接口

添加群发接口


v5.7.0 /2014-4-14

添加百度地图有关帮助类


v5.6.2 /2014-3-15

添加MessageHendler中的OnTextOrEventRequest方法

v5.5.0 /2014-3-14

添加view事件；添加单个消息列队自定义过期时间

v5.4.0 /2014-03-09

souidea全面升级到weiweihi

v5.1.0 /2014-02-14

优化图片等消息的响应，并添加图片等新回复格式的DEMO

v5.0.0 /2014-02-13

添加图片、语音、视频返回类型

v4.13.0 /2014-02-09

添加高级接口中的语种选择


v4.12.0 /2014-02-04

添加更多AccessTokenContainer下的方法


v4.11.0 /2014-01-28

添加媒体文件上传、下载高级接口

添加AccessTokenContainer管理器

v4.9.0 /2013-01-27

添加获取用户分组ID的高级接口

v4.8.0 /2013-12-25

丰富了JSON通讯的消息返回类型

v4.7.3 /2013-12-12

开通OAuth2.0接口，并优化部分代码。

v4.6.0 /2013-12-2

跟据官方2012.12.2更新添加新的接口：https://mp.weixin.qq.com/cgi-bin/readtemplate?t=news/note-20131202_tmpl&lang=zh_CN

v4.5.0 /2013-11-25

升级到v4.5.0 补充之前遗漏的视屏类型消息。

v4.4.6 /2013-11-24

完善HttpUitlity中Get和Post的方法。

v4.4.2 /2013-11-23

添加消息上下文删除事件，Demo见/Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample.CommonService/CustomMessageHandler/CustomMessageContext.cs

v4.3.0 /2013-11-20

为MessageAgent添加了使用SouideaKey的方法：MessageAgent.RequestSouideaXml()。用于更加方便和安全地对接www.souidea.com平台的微信营销工具。有关SouideaKey的说明请见：http://www.souidea.com/ApiDocuments/Item/25#51

v4.2.2 /2013-11-18

优化升级MessageContext和WeixinContext

将最大记录数量设置添加到MessageHandler构造函数中

添加VS2010的解决方案文件

v4.2.0 /2013-11-18

优化新接口

优化菜单操作

添加MessageAgent代理功能

升级MessageContext，创建MessageContainer容器，添加记录条数限制（防止恶意发送消息导致内存大量消耗）

v3.x /2013-10-31

根据官方2013年10月29日更新，对SDK做相应升级。

相关功能的更新下面（OK结尾的表示已完成）：

语音识别            - v3.0 OK

客服接口            - v3.0 OK

OAuth2.0网页授权    - 目测用的人不多，等有人需要用的时候再开发

生成带参数二维码    - v3.5 OK

获取用户地理位置    - v3.1 OK

获取用户基本信息    - v3.2 OK

获取关注者列表      - v3.3 OK

用户分组接口        - v3.4 OK

上传下载多媒体文件  - 目测用的人不多，等有人需要用的时候再开发


v2.4 /2013-8-9

优化菜单操作的逻辑代码，添加WeixinMenuException异常类型

v2.3 /2013-8-7

完善菜单类型，添加在线菜单编辑器

v2.2 /2013-8-5

优化菜单事件处理，解决菜单编码问题

v2.1 /2013-8-5

升级自定义菜单类型

v2.0 /2013-8-5

完成微信5.0 自定义菜单升级


v1.5 /2013-8-4

这是一个重要更新。

为MessageHandler提供了一个DefaultResponseMessage的抽象方法，
DefaultResponseMessage必须在子类中重写，用于返回没有处理过的消息类型（也可以用于默认消息，如帮助信息等）；
其中所有原OnXX的抽象方法已经都改为虚方法，可以不必每个都重写。若不重写，默认返回DefaultResponseMessage方法中的结果。

v1.4 /2013-7-23

为HttpUtility下方法提供Encoding选项。

v1.3 /2013-7-9

封装System.Web.HttpUtility下HTML及Url的Encode及Decode方法

v1.2 /2013-7-8

独立封装RequestUtility.GetQueryString方法，将Dictionary<string,string>类型数据转为QueryString格式。

v1.1 /2013-7-6

添加HttpPost提交方法，支持更多数据提交格式，为实现P2P更多扩展做准备。


v1.0 /2013-6-25

微信4.5 API正式稳定版。修复ResponseMessage生成的XML节点顺序问题。

v0.9 /2013-6-23

开始添加自定义菜单操作；

去掉ResponseMessageNews中的Content属性。


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
