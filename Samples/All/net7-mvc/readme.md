# Senparc.Weixin.Sample.Net7 项目说明

> 推荐您使用当前 Sample（[在线示例](https://sdk.weixin.senparc.com/)）。<br>
> 如果您使用的是 Visual Studio，
请升级到 VS 2022（或将来更新版本），并安装 [.NET 7.0 SDK](https://docs.microsoft.com/zh-cn/aspnet/core/getting-started/?view=aspnetcore-7.0&WT.mc_id=DT-MVP-5002965&tabs=windows)。

当前 Sample 提供了 .NET 7.0 的 Mvc 项目示例，可以直接进行部署测试（高级功能测试需要修改配置文件，如修改 appId 等）。

当前 Sample 也可用于调试 Senparc.Weixin SDK，支持 .NET 4.6.2+、.NET Standard 2.1+、.NET 7.0 的不同版本库编译，并可在 Release 编译条件下生成 nuget 包。

> 提示：由于 `/src` 目录下的源码中采用了条件编译，因此默认情况下，需要开发环境同时安装 `.NET Framework 4.6.2`、`.NET 6.0`、`.NET 7.0`才能编译成功！<br>
> 如果您只希望编译 .NET Core 3.1，请将各个 .csproj 文件中的编译版本条件做对应的修改。


## 特别说明

1. 本项目为 Senparc.Weixin SDK 各个模块集成在一起的综合示例，采用 .NET 7.0 作为默认 Web 运行框架。
2. 由于微信各模块的复杂度，以及整合到一起后的业务逻辑的复杂度，使得本示例的代码也具有一定的复杂度，因此使用本示例不可避免地需要一些基本功和耐心。如果您仅需要查看单个模块的示例代码（公众号、小程序、开放平台、微信支付等），可直接查看 `[/Samples/](../../)` 目录下的其他对应文件夹。
3. 但是您不用太过担心，示例中已经将各个模块使用注释、文件夹等方式进行了区分，阅读代码不会有障碍。
4. 本项目非常适合综合场景的实际项目参考，如果您在学习过程中有任何问题，欢迎加群询问。

## 使用 .NET 6.0 Demo

返回上一级后见：[net6-mvc/Senparc.Weixin.Sample.Net6](../net6-mvc)。

## 使用 .NET Framework 4.5 Demo

返回上一级后见：[net45-mvc/Senparc.Weixin.MP.Sample](../net45-mvc)。

> 注意： .NET Framework 4.5 Sample 已于 2019 年 9 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET 4.5 所有库更新不受影响）。
