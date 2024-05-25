/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：CustomMessageHandler_AI.cs
    文件功能描述：自定义MessageHandler（AI 方法）
    
    
    创建标识：Senparc - 20240524

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.TextToImage;
using Senparc.AI;
using Senparc.AI.Entities;
using Senparc.AI.Kernel;
using Senparc.AI.Kernel.Handlers;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.MessageQueue;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Sample.CommonService.AI.MessageHandlers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Senparc.Weixin.Sample.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler（公众号）
    /// </summary>
    public partial class CustomMessageHandler
    {

        const string WELCOME_MESSAGE = @"

输入“p”暂停，可以暂时保留记忆
输入“e”退出，彻底删除记忆

[结果由 AI 生成，仅供参考]";

        const string GEN_IMAGE_PATTERN = @"(?i)img\s+(.+)";


        /// <summary>
        /// 开始 AI 对话
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        private async Task<IResponseMessageBase> StartAIChatAsync()
        {
            var currentMessageContext = await base.GetCurrentMessageContext();


            //新建个人对话缓存（由于使用了 CurrentMessageContext，多用户之前完全隔离，对话不会串）
            var storage = new ChatStore()
            {
                Status = ChatStatus.Chat,
                History = ""
            };

            currentMessageContext.StorageData = storage.ToJson();//为了提升兼容性，采用字符格式

            await GlobalMessageContext.UpdateMessageContextAsync(currentMessageContext);//储存到缓存

            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "小嗨 Bot 已启动！" + WELCOME_MESSAGE;

            return responseMessage;
        }

        /// <summary>
        /// 进行 AI 对话
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        private async Task<IResponseMessageBase> AIChatAsync(RequestMessageBase requestMessage)
        {
            var currentMessageContext = await base.GetCurrentMessageContext();

            if (!(currentMessageContext.StorageData is string chatJson))
            {
                return null;
            }

            ChatStore chatStore;

            try
            {
                chatStore = chatJson.GetObject<ChatStore>();
                if (chatStore == null || chatStore.Status == ChatStatus.None || chatStore.History == null)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            try
            {
                if (requestMessage is RequestMessageText requestMessageText)
                {
                    string prompt;
                    bool storeHistory = true;

                    if (requestMessageText.Content.Equals("E", StringComparison.OrdinalIgnoreCase))
                    {
                        prompt = $"我即将结束对话，请发送一段文字和我告别，并提醒我：输入“AI”可以再次启动对话。";
                        storeHistory = false;

                        //消除状态记录
                        await UpdateMessageContext(currentMessageContext, null);
                    }
                    else if (requestMessageText.Content.Equals("P", StringComparison.OrdinalIgnoreCase))
                    {
                        prompt = $"我即将临时暂停对话，当下次启动会话时，发送再次欢迎我回来的信息。本次请发送一段文字和我告别，并提醒我：输入“AI”可以再次启动对话。";

                        // 修改状态记录
                        chatStore.Status = ChatStatus.Paused;
                        await UpdateMessageContext(currentMessageContext, chatStore);
                    }
                    else if (chatStore.Status == ChatStatus.Paused)
                    {
                        if (requestMessageText.Content.Equals("AI", StringComparison.OrdinalIgnoreCase))
                        {
                            prompt = @"我将重新开始对话，请发送一段欢迎信息，并且在最后提示我（注意保留换行）：" + WELCOME_MESSAGE;

                            // 修改状态记录
                            chatStore.Status = ChatStatus.Chat;
                            await UpdateMessageContext(currentMessageContext, chatStore);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        prompt = requestMessageText.Content;
                    }

                    //组织返回消息
                    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();

                    #region 请求 AI 模型进入文字聊天及图片生成的经典模式（这里结合起来演示）

                    Match match = Regex.Match(prompt, GEN_IMAGE_PATTERN);
                    if (match.Success)
                    {
                        //生成图片
                        var history = chatStore.History;
                        await GenerateImage(requestMessageText, match.Groups[1].Value,history: history);

                        var noResponse = base.CreateResponseMessage<ResponseMessageNoResponse>();
                        return noResponse;
                    }
                    else
                    {
                        //文字问答
                        var result = await TextChat(prompt, chatStore, storeHistory, currentMessageContext);
                        responseMessage.Content = result;
                    }
                    #endregion

                    return responseMessage;
                }
                else
                {
                    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                    responseMessage.Content = "暂时不支持此数据格式！";
                    return responseMessage;
                }
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);

                var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "系统忙，请稍后再试！";
                return responseMessage;
            }

        }

        /// <summary>
        /// 更新此用户上下文
        /// </summary>
        /// <param name="currentMessageContext"></param>
        /// <param name="chatStore"></param>
        /// <returns></returns>
        private async Task UpdateMessageContext(CustomMessageContext currentMessageContext, ChatStore chatStore)
        {
            currentMessageContext.StorageData = chatStore == null ? null : chatStore.ToJson();
            await GlobalMessageContext.UpdateMessageContextAsync(currentMessageContext);//储存到缓存
        }

        /// <summary>
        /// 获取历史对话单条记录信息
        /// </summary>
        /// <param name="human"></param>
        /// <param name="chatBot"></param>
        /// <returns></returns>
        private string GetHistoryItem(string human, string chatBot)
        {
            return "\nHuman: " + human + "\nChatBot: 任务已完成。";
        }

        /// <summary>
        /// 根据历史记录信息，获取 AI 推荐后的图片生成 Prompt
        /// </summary>
        /// <param name="imgPrompt"></param>
        /// <param name="history"></param>
        /// <returns></returns>
        private async Task<string> GetGenerateImgagePrompt(string imgPrompt, string history)
        {
            //把对话上下文也给到，此处是一个能力演示，这是可选的。通常可以直接使用 Prompt 生成图片。
            var newImgPrompt = $@"请根据下方的 [对话历史] 以及 [最新命令]，理解意图，并生成一条可以提供给 DallE3 模型使用的图片生成 Prompt。
[对话历史]
{history}

[最新命令]
{imgPrompt}

Prompt：";

            //模型请求参数
            var parameter = new PromptConfigParameter()
            {
                MaxTokens = 300,
                Temperature = 0.7,
                TopP = 0.5,
            };

            var setting = (SenparcAiSetting)Senparc.AI.Config.SenparcAiSetting;
            var aiHandler = new SemanticAiHandler(setting);
            var iWantTo = aiHandler.IWantTo()
                        .ConfigModel(ConfigModel.TextCompletion, "Jeffrey")
                        .BuildKernel()
                        .SetPromptConfigParameter(parameter);
            var request = iWantTo.CreateRequest(newImgPrompt);
            var result = await iWantTo.RunAsync(request);

            //使用消息队列异步发送消息，不等待
            var smq = new SenparcMessageQueue();
            smq.Add($"SendTextMessage-{OpenId}-{SystemTime.NowTicks}", async () =>
            {
                //发送提示消息，不等待
                await Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync(appId, OpenId, "根据我们的对话内容，已经为你优化图片生成 Prompt：" + imgPrompt);
            });

            return result.OutputString;
        }

        /// <summary>
        /// 图片生成
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <param name="imgPrompt">生成图片的 Prompt</param>
        /// <returns></returns>
        private async Task GenerateImage(RequestMessageText requestMessage, string imgPrompt, string history = null)
        {
            //先发一条回复，提醒用户等待（为避免公众号响应时间超时），不等待
            _ = Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync(appId, OpenId, "图片生成中，通常需要 1 分钟左，请稍等！");

            if (!history.IsNullOrEmpty())
            {
                imgPrompt = await GetGenerateImgagePrompt(imgPrompt, history);
            }

            //使用消息队列处理
            var smq = new SenparcMessageQueue();
            smq.Add($"GenImg-{requestMessage.FromUserName}-{SystemTime.NowTicks}", async () =>
            {
                try
                {

                    //指定 AzureDallE3 模型，同样需要在 appsettings.json 文件中配置
                    var dalleSetting = ((SenparcAiSetting)Senparc.AI.Config.SenparcAiSetting)["AzureDallE3"];
                    var aiHandler = new SemanticAiHandler(dalleSetting);

                    var iWantTo = aiHandler.IWantTo()
                                .ConfigModel(ConfigModel.TextToImage, "Jeffrey")
                                .BuildKernel();

#pragma warning disable SKEXP0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。

                    var dallE = iWantTo.GetRequiredService<ITextToImageService>();

                    var imageUrl = await dallE.GenerateImageAsync(imgPrompt, 1024, 1024);

                    var tempFileDir = Senparc.CO2NET.Utilities.ServerUtility.ContentRootMapPath("~/App_Data/TempGenImg");
                    Senparc.CO2NET.Helpers.FileHelper.TryCreateDirectory(tempFileDir);

                    var tempFilePath = Path.Combine(tempFileDir, $"Senparc.AI.Dalle-{SystemTime.NowTicks}.jpg");

                    using (var fs = new FileStream(tempFilePath, FileMode.OpenOrCreate))
                    {
                        await Senparc.CO2NET.HttpUtility.Get.DownloadAsync(ServiceProvider, imageUrl, fs);
                        await fs.FlushAsync();
                        await Console.Out.WriteLineAsync("图片已保存：" + tempFilePath);
                    }

                    //及时给一个提示，不等待
                    _ = Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendTextAsync(appId, OpenId, "已生成，正在保存，请稍后！");
                    //上传图片
                    var uploadResult = await Senparc.Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMediaAsync(appId, MP.UploadMediaFileType.image, tempFilePath, 100 * 1000);
                    //把图片发送到客户端
                    await Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendImageAsync(appId, OpenId, uploadResult.media_id);

                    //重新获取最新的上下文，把这次结果添加到上下文中（实际使用过程中建议加同步锁）
                    var currentMessageContext = await base.GetCurrentMessageContext();
                    if (currentMessageContext.StorageData is string chatJson)
                    {
                        ChatStore chatStore = chatJson.GetObject<ChatStore>();

                        var chatItem = GetHistoryItem($"按要求生成图片：" + imgPrompt, "任务已完成。");

                        chatStore.History += chatItem;
                        await UpdateMessageContext(currentMessageContext, chatStore);
                    }

#pragma warning restore SKEXP0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。

                }
                catch (Exception ex)
                {
                    SenparcTrace.BaseExceptionLog(ex);
                }
            });

        }

        /// <summary>
        /// 文本对话
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="chatStore"></param>
        /// <param name="storeHistory"></param>
        /// <param name="currentMessageContext"></param>
        /// <returns></returns>
        private async Task<string> TextChat(string prompt, ChatStore chatStore, bool storeHistory, CustomMessageContext currentMessageContext)
        {
            /* 模型配置
             * 注意：需要在 appsettings.json 中的 <SenparcAiSetting> 节点配置 AI 模型参数，否则无法使用 AI 能力
             */
            var setting = (SenparcAiSetting)Senparc.AI.Config.SenparcAiSetting;//也可以留空，将自动获取

            //模型请求参数
            var parameter = new PromptConfigParameter()
            {
                MaxTokens = 2000,
                Temperature = 0.7,
                TopP = 0.5,
            };

            //最大保存 AI 对话记录数
            var maxHistoryCount = 10;

            //默认 SystemMessage（可根据自己需要修改）
            var systemMessage = Senparc.AI.DefaultSetting.DEFAULT_SYSTEM_MESSAGE;

            var aiHandler = new SemanticAiHandler(setting);
            var iWantToRun = aiHandler.ChatConfig(parameter,
                                userId: "Jeffrey",
                                maxHistoryStore: maxHistoryCount,
                                chatSystemMessage: systemMessage,
                                senparcAiSetting: setting).iWantToRun;

            //注入历史记录（也可以把 iWantToRun 对象缓存起来，其中会自动包含 history，不需要每次读取或者保存）
            iWantToRun.StoredAiArguments.Context["history"] = chatStore.History;

            //获取请求（注意：因为微信需要一次返回所有文本，所以此处不使用 AI 流行的 Stream（流式）输出
            var result = await aiHandler.ChatAsync(iWantToRun, prompt);

            //保存历史记录
            if (storeHistory)
            {
                chatStore.History = iWantToRun.StoredAiArguments.Context["history"]?.ToString();
                await UpdateMessageContext(currentMessageContext, chatStore);
            }

            return result.OutputString;
        }
    }
}