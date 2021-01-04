# Senparc.Weixin.Sample.NetCore3 项目说明

> 推荐您使用当前 Sample（[在线示例](https://sdk.weixin.senparc.com/)）。<br>
> 如果您使用的是 Visual Studio，
请升级到 VS 2019 v16.3 以上，并安装 [.NET Core 3.1 SDK](https://docs.microsoft.com/zh-cn/aspnet/core/getting-started/?view=aspnetcore-3.1tabs=windows&WT.mc_id=DT-MVP-5002965)。

当前 Sample 提供了 .NET Core 3.1 的 Mvc 项目示例，可以直接进行部署测试（高级功能测试需要修改配置文件，如修改 appId 等）。

当前 Sample 也可用于调试 Senparc.Weixin SDK，支持 .NET 4.5、.NET Standard 2.0、.NET Core 3.0+（.NET Standard 2.1） 的不同版本库编译，并可在 Release 编译条件下生成 nuget 包。

> 提示：由于源码中采用了条件编译，因此默认情况下，需要开发环境同时安装 `.NET Framework 4.5`、`.NET Core 2.2`、`.NET Core 3.1` 才能编译成功！<br>
> 如果您只希望编译 .NET Core 3.1，请将各个 .csproj 文件中的编译版本条件做对应的修改。

## 使用 .NET Core 2.2 Demo

返回上一级后见：[netcore2.2-mvc/Senparc.Weixin.MP.Sample.vs2017](../netcore2.2-mvc)。

> 注意： .NET Core 2.2 Sample 将于 2019 年 10 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET Standard 2.2 所有库更新不受影响）。


## 使用 .NET Framework 4.5 Demo

返回上一级后见：[net45-mvc/Senparc.Weixin.MP.Sample](../net45-mvc)。

> 注意： .NET Framework 4.5 Sample 将于 2019 年 9 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET 4.5 所有库更新不受影响）。

