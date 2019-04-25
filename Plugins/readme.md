> 看上去 Plugins 的贡献者并不多，我们正在考虑创作更多的 Demo 和接口让大家了解 Plugins 的好处。其中 [NeuChar.com](https://www.neuchar.com) 已经实现了部分扩展的功能。

# 基于 Senparc.Weixin SDK 的插件

> `Senparc.Weixin.Plugins` 是为 Senparc.Weixin SDK 进行通用性功能扩展插件的计划。<br>
> 我们向所有开发者征集基于 Senparc.Weixin SDK 的通用插件。<br>
> 为方便管理和分离，所有文件夹使用 submodule 等方式引用外部项目。<br>
> 您可以通过以下两种方式推荐自己的项目：<br>
> 1. 发送 [issue](https://github.com/JeffreySu/WeiXinMPSDK/issues/new) 
> 2.  `Pull Request` 说明文件到本文件夹下<br>
> <br>
> 插件入选后将获得一份盛派特制的纪念小礼包！

## Senparc.Weixin.Plugin 标准
> 1. 所有插件使用独立项目管理，此处只做汇总；<br>
> 2. 命名空间前缀统一为 `Senparc.Weixin.Plugins`，方便索引；<br>
> 3. 插件需要继承本项目开源协议（Apache License Version 2.0），原作者信息将完全保留并醒目提示；<br>
> 4. 插件中必须提供可以直接运行的 Demo，并包含必要的测试数据；<br>
> 5. 建议插件统一在 [OpenSenparc](https://github.com/OpenSenparc) 下创建，贡献者可申请加入组织。<br>
> <br>
> 更多标准制定中，将会给出参考 Demo，目前可以参考 [WeixinTraceManager](https://github.com/JeffreySu/Senparc.Weixin.Plugins.WeixinTraceManager) 项目。

## 关于 OpenSenparc
[OpenSenparc](https://github.com/OpenSenparc) 是一个开源组织，加入到组织内的开发者可以更好地协同工作，并提创建自己的扩展开源项目，其中包括微信插件。

## 插件项目

| 项目名称 | 说明 | 源代码项目  |  作者
|---------|------|------|-------|
|  WeixinTraceManager     | 日志管理   | [Senparc.Weixin.Plugins.WeixinTraceManager](https://github.com/OpenSenparc/Senparc.Weixin.Plugins.WeixinTraceManager)  |  [Jeffrey Su](https://github.com/JeffreySu)
|  TemplateMessageManager | 模板消息管理 | [Senparc.Weixin.Plugins.TemplateMessageManager](https://github.com/OpenSenparc/Senparc.Weixin.Plugins.TemplateMessageManager) |  [Jeffrey Su](https://github.com/JeffreySu)

> 欢迎大家一起来贡献和维护插件！
