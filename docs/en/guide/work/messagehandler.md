# MessageHandler

The `MessageHandler` is used to handle messages from the enterprise WeChat dialogue window.

The SDK has all the basic functionality needed for the developer, the developer just needs to create a custom subclass to supplement the business logic that needs to be customised.

## Custom MessageHandler

In the current example, we named our custom MessageHandler `WorkCustomMessageHandler`.

> WorkCustomMessageHandler.cs Reference file for this project:
>
> /MessageHandlers/ directory
> **_WorkCustomMessageHandler.cs_** MessageHandler main file, responsible for general message handling.
> **_WorkCustomMessageContext.cs_** Custom rewrite of DefaultMpMessageContext context (optional)

Of all the demonstrated override methods in `WorkCustomMessageHandler.cs`, only the `DefaultResponseMessage()` method is required to be overridden, and all the other `OnXxxRequest()` methods are optional, so that when the user sends a message, and the corresponding override method cannot be found, then the message will be handled by the `DefaultMpMessageContext.cs`. When the user sends a message and can't find a corresponding overridden method, the `DefaultResponseMessage()` method will be called.

The MessageHandler has two ways of carrying the message so that it can be accessed externally (by the WeChat server) via a URL. These are the **Middleware method** (recommended) and the **Controller method**. The `WorkCustomMessageHandler` used in both ways is common, so it can be switched and coexisted at any time.

## Middleware approach to hosting MessageHandler

The middleware approach is the recommended and most simplified way, there is no need to create any new files, just introduce the middleware in the `Program.cs` file underneath all the Senparc.Weixin registration code after it has been executed:

```cs
app.UseMessageHandlerForWork("/WorkAsync", WorkCustomMessageHandler.GenerateMessageHandler, options =>
{
    options.AccountSettingFunc = context => Senparc.Weixin.Config.SenparcWeixinSetting;
});
```

Once done, the MessageHandler can be accessed via Url **`Domain/WorkAsync`**, set to the message URL of the public backend.

[test /WorkAsync](https://sdk.weixin.senparc.com/WorkAsync)

> Reference document for this project:
>
> /**_Program.cs_**

[Test /Work](https://sdk.weixin.senparc.com/Work)

For more middleware approaches, please refer to (the same way as public use): ["Using MessageHandler Middleware in .NET Core 2.0/3.0"](https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html) (also for .NET 6.0 and above).

## Controller approach to hosting MessageHandler

When the middleware approach is not enough, you can use a Controller to "expand" the execution process and control or intervene more precisely at each step.

To use Controller, you need to create 2 Actions, which correspond to WeChat background authentication (Get request) and real message push (Post request). The project example is located in WorkController.cs.

> WorkController.cs Reference file for this project:
>
> /Controllers/**_WorkController.cs_**

Once done, you can access the MessageHandler via Url **`Domain/Work`**, set to the message URL of the public backend.

[Test /Work](https://sdk.weixin.senparc.com/Work)

For more Controller methods, please refer to [Understanding MessageHandler](https://www.cnblogs.com/szw/p/3414862.html) (recommended to use the full set of asynchronous methods).
