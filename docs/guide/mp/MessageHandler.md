# MessageHandler

The `MessageHandler` is used to handle messages from the WeChat public number dialogue window.

The SDK has all the basic functionality needed for the developer, the developer just needs to create a custom subclass to supplement the business logic that needs to be customised.

## Custom MessageHandler

In the current example, we named our custom MessageHandler `CustomMessageHandler`.

> **CustomMessageHandler.cs Reference file for this project:**
>
> /MessageHandlers/ directory
> **_CustomMessageHandler.cs_** MessageHandler main file + General Message Handling
> **_CustomMessageHandler_Events.cs_** MessageHandler Event Message Handler
> **_CustomMessageContext.cs_** Custom rewrite of DefaultMpMessageContext context (optional)

Of all the `override` methods demonstrated in `CustomMessageHandler*.cs`, only the `DefaultResponseMessage()` method is required to be overridden, and all the other `OnXxxRequest()` methods are optional, so that when a message is sent by a user, and the corresponding override method cannot be found, then the `DefaultMpMessage()` method is called. When the user sends a message and can't find a corresponding override method, the `DefaultResponseMessage()` method will be called.

The MessageHandler has two ways of carrying the message so that it can be accessed externally (by the WeChat server) via a URL. These are the **Middleware method** (recommended) and the **Controller method**. The `CustomMessageHandler` used by both methods is common, so it can be switched and co-exist at any time.

## Middleware way to host MessageHandler

The middleware approach is the recommended and simplest way to introduce middleware without creating any new files, just below the `Program.cs` file after all Senparc.Weixin registration code has been executed:

```cs
app.UseMessageHandlerForMp(
  "/WeixinAsync",
  CustomMessageHandler.GenerateMessageHandler,
  (options) => {
    options.AccountSettingFunc = (context) =>
      Senparc.Weixin.Config.SenparcWeixinSetting;
  }
);
```

Once done, the MessageHandler can be accessed via Url **`Domain/WeixinAsync`**, set to the message URL of the public backend.

[test /WeixinAsync](https://sdk.weixin.senparc.com/WeixinAsync)

> Reference document for this project:
>
> /**_Program.cs_**

For more middleware approaches, please refer to: ["Using MessageHandler Middleware in .NET Core 2.0/3.0"](https://www.cnblogs.com/szw/p/Wechat-MessageHandler-Middleware.html) (also applicable to . 6.0 and above).

## Controller approach to hosting MessageHandler

When the middleware approach doesn't meet your needs, you can use a Controller to "unfold" the execution process and control or intervene more precisely at each step of the execution.

To use Controller, you need to create 2 Actions, which correspond to WeChat background authentication (Get request) and real message push (Post request). The project example is located in `WeixinController.cs`.

> WeixinController.cs Reference file for this project:
>
> /**_Controllers/WeixinController.cs_**

Once done, you can access MessageHandler via Url **`Domain/Weixin`**, set as the message URL of public backend.

[Test /Weixin](https://sdk.weixin.senparc.com/Weixin)

For more Controller methods, please refer to: ["Understanding MessageHandler"](https://www.cnblogs.com/szw/p/3414862.html) (Recommended to use full set of asynchronous methods)
