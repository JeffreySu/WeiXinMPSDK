/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkCustomMessageHandler_AI.cs
    文件功能描述：企业微信自定义MessageHandler（AI 方法）
    
    
    创建标识：Wang Qian - 20250730

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.ChatCompletion;
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
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Sample.CommonService.AI.MessageHandlers;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;

namespace Senparc.Weixin.Sample.CommonService.WorkMessageHandlers
{
    /// <summary>
    /// 自定义MessageHandler（企业微信）
    /// </summary>
    public partial class WorkCustomMessageHandler
    {

        const string WELCOME_MESSAGE = @"
欢迎来到企业微信对话机器人
输入“p”暂停，可以暂时保留记忆
输入“e”退出，彻底删除记忆
输入“dm”可以关闭Markdown格式输出，使用纯文本回复

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
                History = new List<WeixinAiChatHistory>()
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
        /// <returns>如果返回 null，则表明 AI 未介入，继续执行传统方法</returns>
        private async Task<IWorkResponseMessageBase> AIChatAsync(RequestMessageBase requestMessage)
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
                    //文字类型消息

                    string prompt;
                    bool storeHistory = true;
                    var oldChatStatus = chatStore.Status;

                    if (requestMessageText.Content.Equals("E", StringComparison.OrdinalIgnoreCase))
                    {
                        prompt = $"我即将结束对话，请发送一段文字和我告别，并提醒我：输入“AI”可以再次启动对话。";
                        storeHistory = false;

                        //消除状态记录
                        await UpdateMessageContextAsync(currentMessageContext, null);
                    }
                    else if (requestMessageText.Content.Equals("P", StringComparison.OrdinalIgnoreCase))
                    {
                        prompt = $"我即将临时暂停对话，当下次启动会话时，发送再次欢迎我回来的信息。本次请发送一段文字和我告别，并提醒我：输入“AI”可以再次启动对话。";

                        // 修改状态记录
                        chatStore.Status = ChatStatus.Paused;
                        await UpdateMessageContextAsync(currentMessageContext, chatStore);

                    }
                    else if (chatStore.Status == ChatStatus.Paused)
                    {
                        if (requestMessageText.Content.Equals("AI", StringComparison.OrdinalIgnoreCase))
                        {
                            prompt = @"我将重新开始对话，请发送一段欢迎信息，并且在最后提示我（注意保留换行）：" + WELCOME_MESSAGE;

                            // 修改状态记录
                            chatStore.Status = ChatStatus.Chat;
                            await UpdateMessageContextAsync(currentMessageContext, chatStore);
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

                    Func<string, bool> markDownFunc = content => requestMessageText.Content.Equals(content, StringComparison.OrdinalIgnoreCase);

                    // 在对话、暂停状态下、可以切换 Markdown 格式输出
                    if (chatStore.Status == oldChatStatus &&
                         (markDownFunc("DM") || markDownFunc("MD")))
                    {
                        //是否启用 Markdown 格式输出
                        var useMarkdown = markDownFunc("MD");
                        //设置并更新状态
                        chatStore.UseMarkdown = useMarkdown;
                        await UpdateMessageContextAsync(currentMessageContext, chatStore);

                        //返回提示
                        var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                        responseMessage.Content = useMarkdown 
                            ? "已恢复Markdown格式输出，输入dm可关闭Markdown格式输出"
                            : "已关闭Markdown格式输出，使用纯文本回复，输入md可恢复Markdown格式输出";
                        return responseMessage;
                    }

                    //组织返回消息
                    #region 请求 AI 模型进入文字聊天经典模式

                    //使用消息队列处理
                    var smq = new SenparcMessageQueue();
                    smq.Add($"ChatGenerate-{requestMessage.FromUserName}-{SystemTime.NowTicks}", async () =>
                    {
                            //文字问答
                            //根据是否使用Markdown格式输出，为prompt添加前缀，不影响文生图的prompt
                            prompt = chatStore.UseMarkdown ? "请使用Markdown格式回答： " + prompt : "请使用纯文本格式（非Markdown)回答： " + prompt;
                            await TextChatAsync(prompt, chatStore, storeHistory, currentMessageContext);
                    });

                    //直接返回空响应，等待客服接口发送消息
                    var noResponse = new WorkResponseMessageNoResponse
                    {
                        ToUserName = requestMessage.FromUserName,
                        FromUserName = requestMessage.ToUserName,
                        CreateTime = SystemTime.Now
                    };
                    return noResponse;

                    #endregion
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
        private async Task UpdateMessageContextAsync(WorkCustomMessageContext currentMessageContext, ChatStore chatStore)
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
        /// 文本对话
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="chatStore"></param>
        /// <param name="storeHistory"></param>
        /// <param name="currentMessageContext"></param>
        /// <returns></returns>
        private async Task TextChatAsync(string prompt, ChatStore chatStore, bool storeHistory, WorkCustomMessageContext currentMessageContext)
        {
            /* 模型配置
             * 注意：需要在 appsettings.json 中的 <SenparcAiSetting> 节点配置 AI 模型参数，否则无法使用 AI 能力
             */
            var setting = (SenparcAiSetting)Senparc.AI.Config.SenparcAiSetting;//也可以留空，将自动获取

            //模型请求参数
            var parameter = new PromptConfigParameter()
            {
                MaxTokens = 2000,
                Temperature = 0.3,
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
                                senparcAiSetting: setting);

            //注入历史记录（也可以把 iWantToRun 对象缓存起来，其中会自动包含 history，不需要每次读取或者保存）
            iWantToRun.StoredAiArguments.Context["history"] = chatStore.GetChatHistory();// AIKernl 的 history 为 ChatHistory 类型

            //获取请求（注意：因为微信需要一次返回所有文本，所以此处不使用 AI 流行的 Stream（流式）输出
            var result = await aiHandler.ChatAsync(iWantToRun, prompt);

            //保存历史记录
            if (storeHistory)
            {
                chatStore.SetChatHistory(iWantToRun.StoredAiArguments.Context["history"] as ChatHistory);
                await UpdateMessageContextAsync(currentMessageContext, chatStore);
            }

            try
            {
                var weixinSetting = Config.SenparcWeixinSetting.WorkSetting;
                var appKey = AccessTokenContainer.BuildingKey(weixinSetting.WeixinCorpId, weixinSetting.WeixinCorpSecret);
                Console.WriteLine($"AI 对话({weixinSetting.WeixinCorpAgentId},{OpenId})：{result.OutputString}");
                await MassApi.SendTextAsync(appKey, weixinSetting.WeixinCorpAgentId,result.OutputString, OpenId);
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
            }
        }
    }
}