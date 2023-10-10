# MessageHandler

`MessageHandler` 用于处理微信公众号对话窗口的消息。

SDK 已经为开发者准备好了所有需要的基础功能，开发者只需要创建一个自定义的子类，补充需要自定义的业务逻辑。

## 自定义 MessageHandler

当前示例中，我们给自定义的 MessageHandler 取名 `CustomMessageHandler`。

> **CustomMessageHandler.cs 本项目参考文件：**
>
> /MessageHandlers/ 目录
> **_CustomMessageHandler.cs_** MessageHandler 主文件 + 普通消息处理
> **_CustomMessageHandler_Events.cs_** MessageHandler 事件消息处理
> **_CustomMessageContext.cs_** 自定义重写 DefaultMpMessageContext 上下文（可选）

`CustomMessageHandler*.cs` 中所有演示的重写（`override`）方法中，只有 `DefaultResponseMessage()` 方法是必须重写的，其他所有 `OnXxxRequest()` 方法都为可选，当用户发送的消息，找不到对应重写方法时，则调用 `DefaultResponseMessage()` 方法。

MessageHandler 有两种承载方式，使其可以被外部（微信服务器）通过 URL 访问到。分别是**中间件方式**（推荐）和 **Controller 方式**。两种方式所使用的 `CustomMessageHandler` 是通用的，因此可以随时切换和共存。

## 中间件方式承载 MessageHandler

中间件方式是推荐的方式，也是最简化的方式，无需创建任何新文件，只需在 `Program.cs` 文件所有 Senparc.Weixin 注册代码执行后的下方，引入中间件：

```cs
app.UseMessageHandlerForMp("/WeixinAsync", CustomMessageHandler.GenerateMessageHandler, options =>
{
    options.AccountSettingFunc = context =>  Senparc.Weixin.Config.SenparcWeixinSetting;
});

```

完成后，即可通过 Url **`域名/WeixinAsync`** 访问 MessageHandler，设置为公众号后台的消息 URL。

[测试 /WeixinAsync](https://sdk.weixin.senparc.com/WeixinAsync)

> 本项目参考文件：
>
> /**_Program.cs_**

更多中间件方式请参考：[《在 .NET Core 2.0/3.0 中使用 MessageHandler 中间件》](https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html)（同样适用于 .NET 6.0 及以上）。

## Controller 方式承载 MessageHandler

当中间件的方式满足不了需求时，可以使用 Controller 将执行过程“展开”，对每一步执行进行更加精确的控制或干预。

使用 Controller 方式，需要创建 2 个 Action，分别对应微信后台验证（Get 请求），以及真实消息推送（Post 请求）。本项目示例位于 `WeixinController.cs`中。

> WeixinController.cs 本项目参考文件：
>
> /**_Controllers/WeixinController.cs_**

完成后，即可通过 Url **`域名/Weixin`** 访问 MessageHandler，设置为公众号后台的消息 URL。

[测试 /Weixin](https://sdk.weixin.senparc.com/Weixin)

更多 Controller 方式请参考：[《了解 MessageHandler》](https://www.cnblogs.com/szw/p/3414862.html)（推荐使用全套异步方法）
