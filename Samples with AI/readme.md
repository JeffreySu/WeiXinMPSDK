# Senparc.Weixin.Samples powered by AI

## 说明

当前文档用于说明 Senparc.Weixin SDK 结合 AI 的各项能力。

AI 能力来自于 [Senparc.AI](https://github.com/Senparc/Senparc.AI)，并深度集成了 [Semantic Kernel](https://github.com/microsoft/semantic-kernel)、[AutoGen](https://github.com/microsoft/autogen) 等模块，同时进行了扩展，开箱即用，极易上手。

基础功能已正式上线，当前项目正在持续更新中，欢迎大家一起参与贡献代码或想法 💡

内容将涵盖：

1. [X] 微信公众号 Chat 机器人（文字） - 已于 2024 年 5 月 25 日上线
2. [X] 微信公众号 Chat 机器人（图片） - 已于 2024 年 5 月 25 日上线
3. [X] 微信公众号 Chat 机器人（多模态混合） - 已于 2024 年 5 月 26 日上线
4. [X] 微信公众号带搜索功能的 Chat 机器人
5. [X] 企业微信集成 Agent（智能体）机器人 - 已在 [NCF](https://github.com/NeuCharFramework/NCF) 中的 [AgentsManager](https://github.com/NeuCharFramework/NcfPackageSources/tree/master/src/Extensions/Senparc.Xncf.AgentsManager) 模块集成
6. [X] 使用 RAG 构建知识库问答 - 已于 2025 年 1 月 2 日上线，可查看 Senparc.AI 项目
7. [X] 企业微信智能机器人 - 已完成初步开发并集成到 Weixin.Work 中

> 更多示例欢迎发 issue 或群内留言！

## 代码位置

AI 功能将整合在 [/Samples/All/net8-mvc](../Samples/All/net8-mvc/Senparc.Weixin.Sample.Net8/) 集成案例中。

企业微信智能机器人sample整合在[/Samples/Work/Senparc.Weixin.Sample.Work](../Samples/Work/Senparc.Weixin.Sample.Work)中



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

### 企业微信智能机器人

> 企业微信智能机器人的文档: https://developer.work.weixin.qq.com/document/path/101039

这里要说明的是，企业微信智能机器人使用的是**json格式**来进行消息的包装，而非传统微信消息的**xml格式**，在这里，为了可以使用原来的消息处理逻辑进行开发，并且兼容NeuChar的标准，我们设计了 Converter [查看代码](src/Senparc.Weixin.Work/Senparc.Weixin.Work/Helpers/BotEntityHelper.cs)，以及在**加解密库**中添加了对json字符串的支持，以将微信服务器发来的消息json字符串转换为MessageHandler可以处理的标准消息类型、将标准类型消息实例转换为将要发给微信服务器的json字符串。这样做是为了方便开发者用以前的开发模式进行开发，同时避免了对NeuChar标准的改动。

1.创建MessageHandler，并继承Senparc.Weixin.Work.MessageHandlers命名空间内的WorkBotMessageHandler

2.按照常规步骤进行开发

```c#
public class WorkBotCustomMessageHandler : WorkBotMessageHandler<DefaultWorkMessageContext>
    {
        public WorkBotCustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }

        public override IWorkResponseMessageBase DefaultResponseMessage(IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条默认的 Bot 消息。";
            return responseMessage;
        }
    }
```

目前仅支持进入对话窗口默认回复消息、文字请求回复模板卡片的基础内容，流式大模型响应消息正在开发中 :)

3.集成到Controller中

```c#
public class WorkBotController : Controller
    {

        public static readonly string Token = "";//与企业微信机器人后台的 Token 设置保持一致。
        public static readonly string EncodingAESKey = "";//与企业微信机器人后台的 EncodingAESKey 设置保持一致。
        public static readonly string CorpId = "";//注意！企业微信智能机器人的CorpId为空字符串

        public WorkBotController()
        {
        }

        /// <summary>
        /// 微信后台验证地址（使用Get），企业微信后台应用的“修改配置”的Url填写如：https://sdk.weixin.senparc.com/WorkBot
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> Get(string msg_signature = "", string timestamp = "", string nonce = "", string echostr = "")
        {
            //return Content(echostr); //返回随机字符串则表示验证通过
            var verifyUrl = Signature.VerifyURL(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce, echostr);
            if (verifyUrl != null)
            {
                return Content(verifyUrl); //返回解密后的随机字符串则表示验证通过
            }
            else
            {
                return Content("如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 微信后台验证地址（使用Post），企业微信后台应用的“修改配置”的Url填写如：https://sdk.weixin.senparc.com/WorkBot
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> Post(PostModel postModel)
        {
            var maxRecordCount = 10;

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.CorpId = CorpId;


            #region 用于生产环境测试原始数据
            //var ms = new MemoryStream();
            //Request.InputStream.CopyTo(ms);
            //ms.Seek(0, SeekOrigin.Begin);

            //var sr = new StreamReader(ms);
            //var xml = sr.ReadToEnd();
            //var doc = XDocument.Parse(xml);
            //doc.Save(ServerUtility.ContentRootMapPath("~/App_Data/TestWork.log"));
            //return null;
            #endregion

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WorkBotCustomMessageHandler(Request.GetRequestMemoryStream(), postModel, maxRecordCount);

            if (messageHandler.RequestMessage == null)
            {
                //验证不通过或接受信息有错误
            }

            try
            {
                Senparc.Weixin.WeixinTrace.SendApiLog("企业微信 Bot 收到消息", messageHandler.RequestJsonStr);

                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.SaveRequestMessageLog();//记录 Request 日志（可选）

                await messageHandler.ExecuteAsync(new CancellationToken());//执行微信处理过程（关键）

                messageHandler.SaveResponseMessageLog();//记录 Response 日志（可选）
                
                //自动返回加密后结果
                return Content(messageHandler.FinalResponseJsonStr, "application/json");
                
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(ServerUtility.ContentRootMapPath("~/App_Data/Work_Error_" + SystemTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine("ExecptionMessage:" + ex.Message);
                    tw.WriteLine(ex.Source);
                    tw.WriteLine(ex.StackTrace);
                    //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                    if (!string.IsNullOrEmpty(messageHandler.FinalResponseJsonStr))
                    {
                        tw.WriteLine(messageHandler.FinalResponseJsonStr);
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content("");
            }
        }
```

注意，在Bot配置中，**CorpId应填写""空字符串**，这是因为在加解密过程中，企业内部智能机器人场景中，ReceiveId为""，而在加解密库的方法中，ReceiveId形参由CorpId作为实参传入。设置出错会导致验证失败！

注：目前暂不支持中间件方式的使用

