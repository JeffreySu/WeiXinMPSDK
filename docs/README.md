---
home: true
navbar: false
heroImage: /icon.jpg
heroText: JeffreySu/WeiXinMPSDK
tagline: 轻松打造微信各平台的扩展应用
actionText: 快速上手 →
actionLink: /zh/guide/
features:
  - title: 应用广泛
    details: 目前使用率最高的微信 .NET SDK。
  - title: 多平台
    details: 微信公众号、小程序、微信支付V2/V3、JS-SDK、开放平台、企业号、企业微信……
  - title: 可扩展
    details: Senparc.Weixin SDK 扩展组件用于提供缓存、WebSocket 等一系列扩展模块。
---

## 如何使用文档

::: warning 前提条件
文档需要 [Node.js](https://nodejs.org/en/) >= 8.6
:::

**相关阅读：** 可使用NVM管理Node版本，下载[NVM](https://github.com/coreybutler/nvm-windows/releases)。

1. 通过Node安装yarn

   ``` bash
   npm install -g yarn
   ```

2. 安装项目依赖运行(项目源码根目录运行)

   ``` bash
   yarn install
   ```

3. 运行文档项目

   ``` bash
   yarn docs:dev
   ```

<style>
table{
    display: table;
    min-width: 100%;
}
table th:first-of-type {
    width: 60%;
}
table th:nth-of-type(2) {
    width: 40%;
}
</style>

## 文档目录

| 中文文档目录                                                                     | 说明                     |
| ------------------------------------------------------------------------------- | ------------------------ |
| [zh/guide/](zh/guide/)                                                          | **概要**                 |
| [zh/guide/mp/](zh/guide/mp/install.html)                                        | **公众号模块文档**       |
| [&emsp;zh/guide/mp/jssdk/](zh/guide/mp/jssdk.html)                              | &emsp;JSSDK              |
| [&emsp;zh/guide/mp/oauth2.0/](zh/guide/mp/oauth2.0.html)                        | &emsp;OAuth 2.0          |
| [&emsp;zh/guide/mp/menu/](zh/guide/mp/menu.html)                                | &emsp;菜单设置            |
| [zh/guide/wxopen/](zh/guide/wxopen/install.html)                                | **小程序文档**           |
| [&emsp;zh/guide/request-service/](zh/guide/wxopen/request-service.html)         | &emsp;小程序请求服务      |
| [&emsp;zh/guide/login/](zh/guide/wxopen/login.html)                             | &emsp;登录               |
| [&emsp;zh/guide/get-phone-number/](zh/guide/wxopen/get-phone-number.html)       | &emsp;获取手机号          |
| [zh/guide/work/](zh/guide/work/install.html)                                    | **企业微信文档**         |
| [&emsp;zh/guide/work/jssdk-general/](zh/guide/work/jssdk-general.html)          | &emsp;JSSDK常规          |
| [&emsp;zh/guide/work/jssdk-agent-config/](zh/guide/work/jssdk-agent-config.html)| &emsp;JSSDK(agentConfig) |
| [&emsp;zh/guide/work/oauth2.0/](zh/guide/work/oauth2.0.html)                    | &emsp;OAuth 2.0          |
| [&emsp;zh/guide/work/menu/](zh/guide/work/menu.html)                            | &emsp;菜单设置           |
| [zh/guide/tenpayv3/](zh/guide/tenpayv3/install.html)                            | **微信支付 V3 文档**     |
| [&emsp;zh/guide/tenpayv3/jssdk/](zh/guide/tenpayv3/jssdk.html)                  | &emsp;JSAPI 支付         |
| [&emsp;zh/guide/tenpayv3/callback/](zh/guide/tenpayv3/callback.html)            | &emsp;支付回调           |
| [&emsp;zh/guide/tenpayv3/nativepay/](zh/guide/tenpayv3/nativepay.html)          | &emsp;Native 支付        |
| [&emsp;zh/guide/tenpayv3/refund/](zh/guide/tenpayv3/refund.html)                | &emsp;退款               |
| [zh/guide/tenpayv2/](zh/guide/tenpayv2/install.html)                            | **微信支付 V2 文档**      |
| [&emsp;zh/guide/tenpayv2/jssdk/](zh/guide/tenpayv2/jssdk.html)                  | &emsp;JSAPI 支付          |
| [&emsp;zh/guide/tenpayv2/callback/](zh/guide/tenpayv2/callback.html)            | &emsp;支付回调            |
| [&emsp;zh/guide/tenpayv2/nativepay/](zh/guide/tenpayv2/nativepay.html)          | &emsp;Native 支付         |
| [&emsp;zh/guide/tenpayv2/refund/](zh/guide/tenpayv2/refund.html)                | &emsp;退款                |

::: slot footer
Apache License Version 2.0 | Copyright © 2006-present [JeffreySu/WeiXinMPSDK](https://github.com/JeffreySu/WeiXinMPSDK)
:::
