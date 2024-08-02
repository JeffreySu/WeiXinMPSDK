# Senparc.Weixin.Samples powered by AI

## 说明

当前文档用于说明 Senparc.Weixin SDK 结合 AI 的各项能力。

AI 能力来自于 [Senparc.AI](https://github.com/Senparc/Senparc.AI)，并深度集成了 [Semantic Kernel](https://github.com/microsoft/semantic-kernel)、[AutoGen](https://github.com/microsoft/autogen) 等模块，同时进行了扩展，开箱即用，极易上手。

当前项目正在构建完善中，预计在 2024 年 7 月 1 日左右正式上线。

内容将涵盖：

1. [X] 微信公众号 Chat 机器人（文字） - 已于 2024 年 5 月 25 日上线
2. [X] 微信公众号 Chat 机器人（图片） - 已于 2024 年 5 月 25 日上线
3. [X] 微信公众号 Chat 机器人（多模态混合） - 已于 2024 年 5 月 26 日上线
4. [ ] 微信公众号带搜索功能的 Chat 机器人
5. [ ] 企业微信集成 Agent（智能体）机器人
6. [ ] 使用 RAG 构建知识库问答

> 更多示例欢迎发 issue 或群内留言！

## 代码位置

AI 功能将整合在 [/Samples/All/net8-mvc](../Samples/All/net8-mvc/Senparc.Weixin.Sample.Net8/) 集成案例中。

更多说明将在对应功能上线后在本文档中补充。

## 开发说明

### 微信公众号 Chat 机器人（文字）

1. 使用常规步骤开发微信公众号
2. 在 `OnTextRequestAsync` 事件中，加入对进入 AI 对话状态的激活关键字（从节约 AI 用量和用户体验，以及公众号实际功能考虑，建议不要始终保持 AI 对话），如：

``` C#
.Keyword("AI", () => this.StartAIChatAsync().Result)
```

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/f28a5995b3e5f01b3be384b5c7462324ec6f0886/Samples/All/Senparc.Weixin.Sample.CommonService/MessageHandlers/CustomMessageHandler/CustomMessageHandler.cs#L194-L194)

其中 `StartAIChatAsync()` 用于激活当前用户对话山下文的 AI 对话状态

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/d721b118b036b6f37d2cf4e932fb954653eba667/Samples/All/Senparc.Weixin.Sample.CommonService/AI/MessageHandlers/CustomMessageHandler_AI.cs#L70-L70)


3. 为了能够让系统优先判断当前是否在 AI 状态，需要在上述代码执行前，加入尝试 AI 对话的代码，如：

``` C#
var aiResponseMessage = await this.AIChatAsync(requestMessage);
if (aiResponseMessage != null)
{
    return aiResponseMessage;
}
```

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/f28a5995b3e5f01b3be384b5c7462324ec6f0886/Samples/All/Senparc.Weixin.Sample.CommonService/MessageHandlers/CustomMessageHandler/CustomMessageHandler.cs#L179-L179)

其中 `AIChatAsync()` 方法用于提供尝试向 AI 发送对话消息的业务逻辑（如果不在对话状态则返回 null，程序继续执行常规代码）

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/d721b118b036b6f37d2cf4e932fb954653eba667/Samples/All/Senparc.Weixin.Sample.CommonService/AI/MessageHandlers/CustomMessageHandler_AI.cs#L43-L43)

4. 配置 AI 参数，请参考 `Senparc.AI 【开发过程】第一步：配置账号`，在 appsettings.json 文件中追加 ”SenparcAiSetting“ 节点（[查看](https://github.com/Senparc/Senparc.AI/blob/main/README.md#%E7%AC%AC%E4%B8%80%E6%AD%A5%E9%85%8D%E7%BD%AE%E8%B4%A6%E5%8F%B7)）（注意：通常只需设置其中一种平台的配置）

5. 引用 Senparc.AI.Kernel 包，并在启动代码中激活 Senparc.AI：

``` C#
services.AddSenparcAI(Configuration) // 注册 AI
```

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/f28a5995b3e5f01b3be384b5c7462324ec6f0886/Samples/All/net8-mvc/Senparc.Weixin.Sample.Net8/Startup.cs#L88-L88)

``` C#
registerService.UseSenparcAI();// 启用 AI
```

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/f28a5995b3e5f01b3be384b5c7462324ec6f0886/Samples/All/net8-mvc/Senparc.Weixin.Sample.Net8/Startup.cs#L452-L452)


### 微信公众号 Chat 机器人（图片）

图片示例默认使用 Dall·E3 模型，通过配置 `appsettings.json` 节点中的 `Items`-`AzureDalle3` 中的模型参数进行配置进行自动绑定：

``` json
"Items": {
  "AzureDalle3": {
    "AiPlatform": "AzureOpenAI",
    "AzureOpenAIKeys": {
      "ApiKey": "<My AzureOpenAI Keys>",
      "AzureEndpoint": "<My AzureOpenAI DallE3 Endpoint>",
      "AzureOpenAIApiVersion": "2022-12-01",
      "ModelName": {
        "TextToImage": "dall-e-3"
      }
    }
  }
}
```

在程序中，可以通过索引方式找到 `AzureDalle3` 的配置：

``` C#
var dalleSetting = ((SenparcAiSetting)Senparc.AI.Config.SenparcAiSetting)["AzureDallE3"];
```

其他模型初始化方法和聊天模式近似，这里不再赘述。

> [查看代码](https://github.com/JeffreySu/WeiXinMPSDK/blob/6a1593fce4e9c77ae0b04069c5e34f1234f726a3/Samples/All/Senparc.Weixin.Sample.CommonService/AI/MessageHandlers/CustomMessageHandler_AI.cs) `GenerateImageAsync()` 方法

> 此示例延续 [微信公众号 Chat 机器人（文字）](#%E5%BE%AE%E4%BF%A1%E5%85%AC%E4%BC%97%E5%8F%B7-chat-%E6%9C%BA%E5%99%A8%E4%BA%BA%E6%96%87%E5%AD%97)，需要用户进入到对话状态后，输入 `img 创作内容` 字符串激活图片创作流程。示例代码中默认载入了文字对话的历史记录，因此创作内容可以根据对话内容进行综合调整，提升作品的准确度。
