/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：CustomMessageHandler_AI.cs
    文件功能描述：自定义MessageHandler（AI 方法）
    
    
    创建标识：Senparc - 20240524

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.AI.Entities;
using Senparc.AI.Kernel;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Sample.CommonService.AI.MessageHandlers;

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
        /// 开始 AI 对话
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

                    #region 请求 AI 模型进入 Chat 的经典模式

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

                    #endregion

                    //保存历史记录
                    if (storeHistory)
                    {
                        chatStore.History = iWantToRun.StoredAiArguments.Context["history"]?.ToString();
                        await UpdateMessageContext(currentMessageContext, chatStore);
                    }

                    //组织返回消息
                    var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                    responseMessage.Content = result.OutputString;
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

        private async Task UpdateMessageContext(CustomMessageContext currentMessageContext, ChatStore chatStore)
        {
            currentMessageContext.StorageData = chatStore == null ? null : chatStore.ToJson();
            await GlobalMessageContext.UpdateMessageContextAsync(currentMessageContext);//储存到缓存
        }
    }
}