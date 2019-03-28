# Senparc.WebSocket

| 分支      | 生成                                           | 备注
|-----------|------------------------------------------------|--------------
| master    | [![master Build Status][1.1]][1.2]             | Mono似乎找不到System.Web.WebSockets，导致左侧自动编译出错，不影响使用
| Developer | [![Developer Build Status][2.1]][2.2]          | Mono似乎找不到System.Web.WebSockets，导致左侧自动编译出错，不影响使用

[1.1]: https://travis-ci.org/JeffreySu/Senparc.WebSocket.svg?branch=master
[1.2]: https://travis-ci.org/JeffreySu/Senparc.WebSocket
[2.1]: https://travis-ci.org/JeffreySu/Senparc.WebSocket.svg?branch=Developer
[2.2]: https://travis-ci.org/JeffreySu/Senparc.WebSocket


为微信小程序等提供独立的 WebSocket 服务器端环境，当前版本为：`beta1`。

开发分支为Developer。

欢迎贡献代码！


## 贡献代码

> 如果需要使用或修改此项目的源代码，建议先Fork。也欢迎将您修改的通用版本Pull Request过来。

1. Fork
2. 创建您的特性分支 (`git checkout -b my-new-feature`)
3. 提交您的改动 (`git commit -am 'Added some feature'`)
4. 将您的修改记录提交到远程 `git` 仓库 (`git push origin my-new-feature`)
5. 然后到 github 网站的该 `git` 远程仓库的 `my-new-feature` 分支下发起 Pull Request
（请提交到 `Developer` 分支，不要直接提交到 `master` 分支）

## 如何使用 Nuget 安装？

* 微信小程序 Nuget 地址：https://www.nuget.org/packages/Senparc.WebSocket
* 命令：
```
PM> Install-Package Senparc.WebSocket
```

> PS：[Senparc.Weixin.WxOpen（小程序）](https://www.nuget.org/packages/Senparc.Weixin.WxOpen) 项目已经自动依赖Senparc.WebSocket