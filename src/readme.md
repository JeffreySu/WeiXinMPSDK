.NET Core 2.0 + .NET Core + .NET Framework 4.5 版本
================

本文件件用于存放 .NET Core 2.0 + .NET Core + .NET Framework 4.5 版本 版本代码。

Nuget 包也由此项目生成

## 项目文件夹说明


| 文件夹 | 说明 |
|--------|--------|
|Senparc.WebSocket|WebSocket 模块|
|Senparc.Weixin.Cache|Senparc.Weixin.Cache.Memcached.dll 、 Senparc.Weixin.Cache.Redis.dll 等分布式缓存扩展方案|
|Senparc.Weixin.MP.BuildOutPut|所有最新版本DLL发布文件夹|
|Senparc.Weixin.MP.MvcExtension|Senparc.Weixin.MP.MvcExtension.dll源码，为MVC4.0项目提供的扩展包。|
|Senparc.Weixin.MP.Sample|可以直接发布使用的Demo（.NET Framework 4.5 + ASP.NET MVC）|
|Senparc.Weixin.MP.Sample.WebForms|可以直接发布使用的Demo（.NET Framework 4.5 + ASP.NET WebForms）|
|Senparc.Weixin.MP.Sample.vs2017|可以直接发布使用的Demo（ASP.NET Core 2.0）|
|Senparc.Weixin.MP|Senparc.Weixin.MP.dll 微信公众账号SDK源代码|
|Senparc.Weixin.Open|Senparc.Weixin.Open.dll 第三方开放平台SDK源代码|
|Senparc.Weixin.QY|Senparc.Weixin.QY.dll 微信企业号SDK源代码（已经停止更新，升级到Work）|
|Senparc.Weixin.Work|Senparc.Weixin.Work.dll 企业微信SDK源代码|
|Senparc.Weixin.WxOpen|Senparc.Weixin.WxOpen.dll 微信小程序SDK源代码|
|Senparc.Wiexin|所有Senparc.Weixin.[x].dll 基础类库源代码|


## 帮你选择

> 如果你已经安装了 VS2017，并且希望调试 .NET Core 版本（同时支持 .NET Framework 4.5 / .NET Core 1.1 / .NET Core 2.0），那么请打开：Senparc.Weixin.MP.Sample.vs2017/Senparc.Weixin.MP.Sample.vs2017.sln 解决方案

> 其他情况（如没有安装 VS2017，或者只是想调试 .NET Framework 4.5 项目），那么请打开：Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample.sln 解决方案

无论选择哪个解决方案，类库的功能都是一致的。
