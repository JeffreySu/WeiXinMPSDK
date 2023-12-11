# MessageHandler

The `MessageHandler` is used to handle messages from the app's customer service dialogue window and other push messages from WeChat servers.

The SDK has prepared all the basic functionality needed for the developer, the developer only needs to create a custom subclass to supplement the business logic that needs to be customised.

## Custom MessageHandler

In the current example, we named our custom MessageHandler `CustomWxOpenMessageHandler`.

> CustomWxOpenMessageHandler.cs Reference file for this project:
>
> /MessageHandlers/ directory
> **_CustomWxOpenMessageHandler.cs_** MessageHandler Message Handler
> **_CustomWxOpenMessageContext.cs_** Custom rewrite of DefaultMpMessageContext context (optional)

Of all the `override` methods demonstrated in `CustomWxOpenMessageHandler.cs`, only the `DefaultResponseMessageAsync()` method is mandatory, and all the other `OnXxxRequestAsync()` methods are optional, which is useful for handling messages sent by the user when the corresponding message cannot be found. All other `OnXxxRequestAsync()` methods are optional, when the user sends a message and can't find the corresponding overridden method, the `DefaultResponseMessageAsync()` method will be called.

The MessageHandler has two ways of carrying the message so that it can be accessed externally (WeChat server) via URL. These are the **Middleware method** (recommended) and the **Controller method**. The `CustomWxOpenMessageHandler` used in both ways is common, so it can be switched and coexisted at any time.

## Middleware way to host MessageHandler

The middleware approach is the recommended and simplest way, no need to create any new files, just introduce the middleware in the `Program.cs` file underneath all Senparc.Weixin registration code after execution:

```cs
app.UseMessageHandlerForWxOpen("/WxOpenAsync", CustomWxOpenMessageHandler.GenerateMessageHandler, options =>
{
    options.AccountSettingFunc = context => Senparc.Weixin.Config.SenparcWeixinSetting;
});
```

Once done, the MessageHandler can be accessed via Url **`Domain/WxOpenAsync`**, set to the message URL of the public backend.

[test /WxOpenAsync](https://sdk.weixin.senparc.com/WxOpenAsync)

> Reference document for this project:
>
> /**_Program.cs_**

For more middleware approaches, please refer to: ["Using MessageHandler Middleware in .NET Core 2.0/3.0"](https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html) (also available for . 6.0 and above, same usage as public).

## Controller approach to hosting MessageHandler

When the middleware approach can't satisfy your needs, you can use a Controller to "expand" the execution process and control or intervene more precisely at each step of the execution.

To use Controller, you need to create 2 Actions (both with ActionName `Index`), which correspond to WeChat background authentication (Get request) and real message push (Post request). The project example is located in WxOpenController.cs.

> WxOpenController.cs Reference file for this project:
>
> /**_Controllers/WxOpenController.cs_**

Once done, the MessageHandler can be accessed via Url **`Domain/WxOpen`**, set to the message URL of the public backend.

[Test /WxOpen](https://sdk.weixin.senparc.com/WxOpen)

For more Controller methods, please refer to: ["Understanding MessageHandler"](https://www.cnblogs.com/szw/p/3414862.html) (recommended to use the full set of asynchronous methods, the basic usage is the same as that of the public number)
