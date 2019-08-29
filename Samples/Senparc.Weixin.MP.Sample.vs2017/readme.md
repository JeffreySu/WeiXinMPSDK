# Senparc.Weixin.MP.Sample.vs2017 项目说明

1. 本项目为 同时支持 .NET 3.5/4.0/4.5/.NET Standard 2.0（兼容 .NET Core） 的 Demo，可以直接编译并发布运行。
2. 本项目支持 VS2017 / **VS2019**。


> Senparc.Weixin.MP.Sample.vs2017.sln 为包含单元测试的完整解决方案（推荐，Senparc 团队一般在这个解决方案下开发和测试）<br>
> Senparc.Weixin.MP.Sample.vs2017.without-tests.sln 为不包含单元测试的解决方案

运行本解决方案建议安装 .NET Core（建议 2.2 以上)：https://dotnet.microsoft.com/download

https://sdk.weixin.senparc.com 官方在线示例已经使用此完整 Sample 项目。

## 使用 .NET Framework 4.5 Demo

返回上一级后见：[Senparc.Weixin.MP.Sample](..//Senparc.Weixin.MP.Sample)。

> 注意： .NET Framework 4.5 Sample 将于 2019 年 9 月 1 日起停止小版本更新（大版本更新仍将保持同步，.NET 4.0/4.5 所有库更新不受影响）。


## 其他说明

Senparc.Weixin.MP.Sample.CommonService 里面包含了CustomMessageHandler及原Senparc.Weixin.MP.Sample/Service目录的代码。

分离这些文件是为了在WebForms项目中重用。
