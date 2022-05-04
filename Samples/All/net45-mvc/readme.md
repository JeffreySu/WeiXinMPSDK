# Senparc.Weixin.MP.Sample 项目说明

本项目为 .NET Framework 4.5 的 Demo，可以直接编译并发布运行（[在线示例](http://net45.sdk.weixin.senparc.com/)）。

> 注意： .NET Framework 4.5 Sample 已于 2019 年 9 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET 4.5 所有库更新不受影响）。

> 注意： 当前 Sample 自 2022 年 5 月 4 日起，升级为 .NET Framework 4.6.2，并将一直支持到微软官方停止对该版本的支持，其后升级到 .NET Framework 4.8。为了方便交流，暂时保留 `net45` 这个名字，用以代表 .NET Framework Sample。

## 解决方案文件说明

| 文件名 |  说明
|-------|---------
| Senparc.Weixin.MP.Sample.sln | 包含示例、源代码、单元测试项目的解决方案，需要使用 VS2019 以上打开（部分源码包含 C# 9.0 以上语法）

## .NET Core 及所有版本 Demo

返回上一级后见目录：[Senparc.Weixin.Sample.Net6.sln](../net6-mvc/) (.NET 6 版本，加载完整源码，推荐)。


## 其他说明

Senparc.Weixin.MP.Sample.CommonService 里面包含了 CustomMessageHandler 及原 Senparc.Weixin.MP.Sample/Service 目录的代码。

分离这些文件是为了在 WebForms 项目中重用。
