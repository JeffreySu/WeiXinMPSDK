# Senparc.Weixin SDK Sample

本文件夹内项目，用于演示微信各平台模块的使用。

> 注意：每个模块中会包含多项功能，请根据实际需要选用。

## Sample 文件夹说明

| 文件夹 | 说明 |  SDK 引用方式
|--------|--------|----|
|[All](/Samples/All/)         |   包含微信公众号、小程序、微信支付、企业微信等所有功能的混合场景演示，<br>推荐用于集成多个平台的项目，或许要进行深度开发的场景参考（进阶） | 
| ┣ [All/console](/Samples/All/console)			|命令行 Console Demo（.NET Core）| Nuget 包
| ┣ [All/net45-mvc](/Samples/All/net45-mvc)						|可以直接发布使用的 Demo（.NET Framework 4.5 + ASP.NET MVC）|  Nuget 包
| ┣ [All/net6-mvc](/Samples/All/net6-mvc)			|可以直接发布使用的 Demo（.NET 6.0），兼容 .NET 5.0 和 .NET Core | <strong>源码<strong>
| ┣ [All/net7-mvc](/Samples/All/net7-mvc)			|可以直接发布使用的 Demo（.NET 7.0），兼容 .NET 5.0、6.0 和 .NET Core	 | <strong>源码<strong>
| ┣ [All/net8-mvc](/Samples/All/net8-mvc)			|可以直接发布使用的 Demo（.NET 8.0），兼容 .NET 5.0、6.0、7.0 和 .NET Core	 | <strong>源码（最新）<strong>
| ┗ [All/Senparc.Weixin.<br>Sample.CommonService](/Samples/All/Senparc.Weixin.Sample.CommonService)			| 所有 `All` 文件夹下的 Sample 公用的类库和逻辑代码 |
|[MP](/Samples/MP/)          |   公众号 | Nuget 包
|[TenPayV2](/Samples/TenPayV2/)    |   微信支付 V1 和 V2  | Nuget 包
|[TenPayV3](/Samples/TenPayV3/)    |   微信支付 V3（TenPay APIv3） | Nuget 包
|[Work](/Samples/Work/)        |   企业微信 | Nuget 包
|[WxOpen](/Samples/WxOpen/)      |   微信小程序 | Nuget 包
|[Shared](/Samples/Shared)      |   所有当前 Samples 根目录下的独立模块 Sample 都需要用到的共享文件（实际项目中不需要）


