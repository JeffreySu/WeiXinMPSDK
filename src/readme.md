Senparc.Weixin SDK 全部源代码
================

本文件件用于存放 Senparc.Weixin SDK 所有模块的源代码。

> Nuget 所有版本的包也由此项目生成，包括：.NET Framework、.NET Standard 2.x、.NET 6.0+

## 项目文件夹说明


| 文件夹 | 说明 |
|--------|--------|
|[Senparc.WebSocket](src/Senparc.WebSocket/)|WebSocket 模块|
|[Senparc.Weixin.Cache](src/Senparc.Weixin.Cache)							|Senparc.Weixin.Cache.Memcached.dll 、 Senparc.Weixin.Cache.Redis.dll 等分布式缓存扩展方案|
|[Senparc.Weixin.AspNet](src/Senparc.Weixin.AspNet)							|Senparc.Weixin.AspNet.dll 专为 Web 提供支撑的类库|
|[Senparc.Weixin.MP.MvcExtension](src/Senparc.Weixin.MP.MvcExtension)		|Senparc.Weixin.MP.MvcExtension.dll源码，为 MVC 项目提供的扩展包 |
|[Senparc.Weixin.MP](src/Senparc.Weixin.MP)									|Senparc.Weixin.MP.dll 微信公众账号SDK源代码|
|[Senparc.Weixin.MP.Middleware](src/Senparc.Weixin.MP.Middleware)           |Senparc.Weixin.MP.Middleware.dll 微信公众账号消息中间件源代码|
|[Senparc.Weixin.Open](src/Senparc.Weixin.Open)								|Senparc.Weixin.Open.dll 第三方开放平台SDK源代码|
|[Senparc.Weixin.TenPay](src/Senparc.Weixin.TenPay)							|Senparc.Weixin.TenPay.dll & Senparc.Weixin.TenPayV3.dll 包含微信支付 [V2](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer/src/Senparc.Weixin.TenPay/Senparc.Weixin.TenPay) 和 [V3](https://github.com/JeffreySu/WeiXinMPSDK/tree/Developer/src/Senparc.Weixin.TenPay/Senparc.Weixin.TenPayV3) 的源代码|
|[Senparc.Weixin.Work](src/Senparc.Weixin.Work)								|Senparc.Weixin.Work.dll 企业微信SDK源代码|
|[Senparc.Weixin.Work.Middleware](src/Senparc.Weixin.Work.Middleware)       |Senparc.Weixin.Work.Middleware.dll 企业微信消息中间件源代码|
|[Senparc.Weixin.WxOpen](src/Senparc.Weixin.WxOpen)							|Senparc.Weixin.WxOpen.dll 微信小程序SDK源代码，包括小游戏|
|[Senparc.Weixin.WxOpen.Middleware](src/Senparc.Weixin.WxOpen.Middleware)   |Senparc.Weixin.WxOpen.Middleware.dll 微信小程序消息中间件源代码，包括小游戏|
|[Senparc.Weixin](src/Senparc.Weixin)										|所有Senparc.Weixin.[x].dll 基础类库源代码|