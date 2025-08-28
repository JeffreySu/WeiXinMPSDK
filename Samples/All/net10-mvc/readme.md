# Senparc.Weixin.Sample.Net10 项目说明

> 推荐您使用当前 Sample（[在线示例](https://sdk.weixin.senparc.com/)）。<br>
> 如果您使用的是 Visual Studio，
请升级到 VS 2022（或将来更新版本），并安装 [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)。

当前 Sample 提供了 .NET 10.0 的 Mvc 项目示例，可以直接进行部署测试（高级功能测试需要修改配置文件，如修改 appId 等）。

当前 Sample 也可用于调试 Senparc.Weixin SDK，支持 .NET 4.6.2+、.NET Standard 2.1+、.NET 10.0 的不同版本库编译，并可在 Release 编译条件下生成 nuget 包。

> 提示：由于 `/src` 目录下的源码中采用了条件编译，因此默认情况下，需要开发环境同时安装 `.NET Framework 4.6.2`、`.NET 10.0`才能编译成功！

## 特别说明

1. 本示例同时集成了多个模块的功能，包括：微信公众号、微信支付、企业微信、小程序等功能，模块众多。
2. 如果您想要学习某个单一模块的用法，可以查看对应模块的说明和Sample，或查看单元测试项目。
3. 但是您不用太过担心，示例中已经将各个模块使用注释、文件夹等方式进行了区分，阅读代码不会有障碍。
4. 本项目非常适合综合场景的实际项目参考，如果您在学习过程中有任何问题，欢迎加群询问。

## 使用 .NET Framework 4.5 Demo

返回上一级后见：[net45-mvc/Senparc.Weixin.MP.Sample](../net45-mvc)。

> 注意： .NET Framework 4.5 Sample 已于 2019 年 9 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET 4.5 所有库更新不受影响）。