---
home: true
navbar: false
heroImage: /icon.jpg
heroText: JeffreySu/WeiXinMPSDK
tagline: 轻松打造微信各平台的扩展应用
actionText: 快速上手 →
actionLink: /zh/guide/
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

## 文档目录

| 中文文档目录                                                                     | 说明                     |
| ------------------------------------------------------------------------------- | ------------------------ |
| [/zh/guide/](/zh/guide/)                                                          | **概要**                 |
| [/zh/guide/mp/](/zh/guide/mp/install.md)                                        | **公众号模块文档**       |
| [&emsp;/zh/guide/mp/jssdk/](/zh/guide/mp/jssdk.md)                              | &emsp;JSSDK              |
| [&emsp;/zh/guide/mp/oauth2.0/](/zh/guide/mp/oauth2.0.md)                        | &emsp;OAuth 2.0          |
| [&emsp;/zh/guide/mp/menu/](/zh/guide/mp/menu.md)                                | &emsp;菜单设置            |
| [/zh/guide/wxopen/](/zh/guide/wxopen/install.md)                                | **小程序文档**           |
| [&emsp;/zh/guide/request-service/](/zh/guide/wxopen/request-service.md)         | &emsp;小程序请求服务      |
| [&emsp;/zh/guide/login/](/zh/guide/wxopen/login.md)                             | &emsp;登录               |
| [&emsp;/zh/guide/get-phone-number/](/zh/guide/wxopen/get-phone-number.md)       | &emsp;获取手机号          |
| [/zh/guide/work/](/zh/guide/work/install.md)                                    | **企业微信文档**         |
| [&emsp;/zh/guide/work/jssdk-general/](/zh/guide/work/jssdk-general.md)          | &emsp;JSSDK常规          |
| [&emsp;/zh/guide/work/jssdk-agent-config/](/zh/guide/work/jssdk-agent-config.md)| &emsp;JSSDK(agentConfig) |
| [&emsp;/zh/guide/work/oauth2.0/](/zh/guide/work/oauth2.0.md)                    | &emsp;OAuth 2.0          |
| [&emsp;/zh/guide/work/menu/](/zh/guide/work/menu.md)                            | &emsp;菜单设置           |
| [/zh/guide/tenpayv3/](/zh/guide/tenpayv3/install.md)                            | **微信支付 V3 文档**     |
| [&emsp;/zh/guide/tenpayv3/jssdk/](/zh/guide/tenpayv3/jssdk.md)                  | &emsp;JSAPI 支付         |
| [&emsp;/zh/guide/tenpayv3/callback/](/zh/guide/tenpayv3/callback.md)            | &emsp;支付回调           |
| [&emsp;/zh/guide/tenpayv3/nativepay/](/zh/guide/tenpayv3/nativepay.md)          | &emsp;Native 支付        |
| [&emsp;/zh/guide/tenpayv3/refund/](/zh/guide/tenpayv3/refund.md)                | &emsp;退款               |
| [/zh/guide/tenpayv2/](/zh/guide/tenpayv2/install.md)                            | **微信支付 V2 文档**      |
| [&emsp;/zh/guide/tenpayv2/jssdk/](/zh/guide/tenpayv2/jssdk.md)                  | &emsp;JSAPI 支付          |
| [&emsp;/zh/guide/tenpayv2/callback/](/zh/guide/tenpayv2/callback.md)            | &emsp;支付回调            |
| [&emsp;/zh/guide/tenpayv2/nativepay/](/zh/guide/tenpayv2/nativepay.md)          | &emsp;Native 支付         |
| [&emsp;/zh/guide/tenpayv2/refund/](/zh/guide/tenpayv2/refund.md)                | &emsp;退款                |

::: slot footer
Apache License Version 2.0 | Copyright © 2006-present [JeffreySu/WeiXinMPSDK](https://github.com/JeffreySu/WeiXinMPSDK)
:::
