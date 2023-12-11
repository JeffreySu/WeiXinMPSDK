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

1. 进度项目根目录的文档子文件docs

   ``` bash
   cd docs
   ```

2. 通过Node安装yarn

   ``` bash
   npm install -g yarn
   ```

3. 安装项目依赖运行(项目源码根目录运行)

   ``` bash
   yarn install
   ```

4. 运行文档项目

   ``` bash
   yarn docs:dev
   ```

## 文档目录

| 中文文档目录                                                                               | 说明                     |
| ----------------------------------------------------------------------------------------- | ------------------------ |
| [/docs/zh/guide/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/)                                                        | **概要**                 |
| [/docs/zh/guide/mp/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/mp/install.md)                                        | **公众号模块文档**       |
| [&emsp;/docs/zh/guide/mp/jssdk/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/mp/jssdk.md)                              | &emsp;JSSDK              |
| [&emsp;/docs/zh/guide/mp/oauth2.0/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/mp/oauth2.0.md)                        | &emsp;OAuth 2.0          |
| [&emsp;/docs/zh/guide/mp/menu/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/mp/menu.md)                                | &emsp;菜单设置            |
| [/docs/zh/guide/wxopen/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/wxopen/install.md)                                | **小程序文档**           |
| [&emsp;/docs/zh/guide/request-service/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/wxopen/request-service.md)         | &emsp;小程序请求服务      |
| [&emsp;/docs/zh/guide/login/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/wxopen/login.md)                             | &emsp;登录               |
| [&emsp;/docs/zh/guide/get-phone-number/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/wxopen/get-phone-number.md)       | &emsp;获取手机号          |
| [/docs/zh/guide/work/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/work/install.md)                                    | **企业微信文档**         |
| [&emsp;/docs/zh/guide/work/jssdk-general/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/work/jssdk-general.md)          | &emsp;JSSDK常规          |
| [&emsp;/docs/zh/guide/work/jssdk-agent-config/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/work/jssdk-agent-config.md)| &emsp;JSSDK(agentConfig) |
| [&emsp;/docs/zh/guide/work/oauth2.0/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/work/oauth2.0.md)                    | &emsp;OAuth 2.0          |
| [&emsp;/docs/zh/guide/work/menu/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/work/menu.md)                            | &emsp;菜单设置           |
| [/docs/zh/guide/tenpayv3/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv3/install.md)                            | **微信支付 V3 文档**     |
| [&emsp;/docs/zh/guide/tenpayv3/jssdk/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv3/jssdk.md)                  | &emsp;JSAPI 支付         |
| [&emsp;/docs/zh/guide/tenpayv3/callback/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv3/callback.md)            | &emsp;支付回调           |
| [&emsp;/docs/zh/guide/tenpayv3/nativepay/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv3/nativepay.md)          | &emsp;Native 支付        |
| [&emsp;/docs/zh/guide/tenpayv3/refund/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv3/refund.md)                | &emsp;退款               |
| [/docs/zh/guide/tenpayv2/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv2/install.md)                            | **微信支付 V2 文档**      |
| [&emsp;/docs/zh/guide/tenpayv2/jssdk/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv2/jssdk.md)                  | &emsp;JSAPI 支付          |
| [&emsp;/docs/zh/guide/tenpayv2/callback/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv2/callback.md)            | &emsp;支付回调            |
| [&emsp;/docs/zh/guide/tenpayv2/nativepay/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv2/nativepay.md)          | &emsp;Native 支付         |
| [&emsp;/docs/zh/guide/tenpayv2/refund/](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/docs/zh/guide/tenpayv2/refund.md)                | &emsp;退款                |

::: slot footer
Apache License Version 2.0 | Copyright © 2006-present [JeffreySu/WeiXinMPSDK](https://github.com/JeffreySu/WeiXinMPSDK)
:::
