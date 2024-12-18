# Senparc.Weixin.MP Sample

当前 Sample 用于独立演示公众号的主要功能

打开 `Senparc.Weixin.Sample.MP.sln` 打开解决方案（使用 Nuget 包引用）。

在此解决方案中包含了几个不同的项目

项目  |  说明
---- | ----
Senparc.Weixin.Sample.MP | 公众号主要功能的简要演示项目
Senparc.Weixin.Sample.MP.Simple | 公众号的最精简模式演示项目

> 此项目中所演示的精简模式等，都可以举一反三用在其他平台上（如小程序、企业微信、微信支付等）。

## 打开全量 Sample 项目

当前您仍然可以使用 [全量 Sample](../All/net8-mvc/) 解决方案打开，并将 Senparc.Weixin.Sample.MP 项目设为启动项目。

## 引用源码调试

当前项目默认使用 Nuget 包引用，如果您想直接引用源码调试，可以打开 [全量 Sample](../All/net8-mvc/)，并编辑当前项目 .csproj 文件，根据注释，删除 Nuget 包引用代码，并启用源码项目引用。
