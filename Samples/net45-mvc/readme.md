# Senparc.Weixin.MP.Sample 项目说明

本项目为 .NET Framework 4.5 的 Demo，可以直接编译并发布运行（[在线示例](http://net45.sdk.weixin.senparc.com/)）。

> 注意： .NET Framework 4.5 Sample 已于 2019 年 9 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET 4.5 所有库更新不受影响）。

## 解决方案文件说明

| 文件名 |  说明
|-------|---------
| Senparc.Weixin.MP.Sample.sln | 包含示例、源代码、单元测试项目的解决方案，需要使用 VS2017 以上打开
| Senparc.Weixin.MP.Sample.Libraries.sln | 只包含少数项目源代码的解决方案，仅供自动化检测用，无实用性

## .NET Core 及所有版本 Demo

返回上一级后见目录：[Senparc.Weixin.MP.Sample.vs2017.sln](../netcore2.2-mvc/) (.Net Core 2.2) / [Senparc.Weixin.Sample.NetCore3.vs2019.sln](../netcore3.0-mvc/) (.Net Core 3.0，推荐)。


## 其他说明

Senparc.Weixin.MP.Sample.CommonService 里面包含了 CustomMessageHandler 及原 Senparc.Weixin.MP.Sample/Service 目录的代码。

分离这些文件是为了在 WebForms 项目中重用。
