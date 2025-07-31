/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：ChatStore.cs
    文件功能描述：按个人信息隔离的 Chat 缓存
    
    
    创建标识：Senparc - 20240524

    修改标识：Wang Qian - 20250728
    修改描述：为长对话模式的支持新增了LastStoredMemory、LastStoredPrompt、UseLongChat属性

----------------------------------------------------------------*/

using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.ChatCompletion;
using Senparc.AI.Kernel.Handlers;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Senparc.Weixin.MP.Sample.CommonService.AI.MessageHandlers
{
    public class WeixinAiChatHistory(AuthorRole role, string content)
    {
        public AuthorRole Role { get; set; } = role;
        public string Content { get; set; } = content;

    }

    /// <summary>
    /// 按个人信息隔离的 Chat 缓存
    /// </summary>
    public class ChatStore
    {
        public ChatStatus Status { get; set; }

        public MultimodelType MultimodelType { get; set; }
        //public string History { get; set; }

        public List<WeixinAiChatHistory> History { get; set; }

        /// <summary>
        /// 是否使用Markdown格式输出
        /// </summary>
        public bool UseMarkdown { get; set; }

        /// <summary>
        /// 是否使用长对话模式
        /// </summary>
        public bool UseLongChat { get; set; }

        /// <summary>
        /// 上一次保存的记忆
        /// </summary>
        public string LastStoredMemory { get; set; }

        /// <summary>
        /// 上一次提问的prompt
        /// </summary>
        public string LastStoredPrompt { get; set; }

        public ChatStore()
        {
            Status = ChatStatus.None;
            MultimodelType = MultimodelType.None;
            UseMarkdown = true;
            UseLongChat = false;
        }

        public ChatHistory GetChatHistory()
        {
            var history = new ChatHistory();
            foreach (var item in History)
            {
                history.AddMessage(item.Role, item.Content);
            }
            return history;
        }

        public void SetChatHistory(ChatHistory chatHistory)
        {
            if (chatHistory == null)
            {
                ClearHistory();
            }

            History = new List<WeixinAiChatHistory>();
            foreach (var message in chatHistory)
            {
                History.Add(new WeixinAiChatHistory(message.Role, message.Content));
            }
        }

        public void AddUserMessage(string content)
        {
            History.Add(new WeixinAiChatHistory(AuthorRole.User, content));
        }

        public void AddAssistantMessage(string content)
        {
            History.Add(new WeixinAiChatHistory(AuthorRole.Assistant, content));
        }

        public void AddSystemMessage(string content)
        {
            History.Add(new WeixinAiChatHistory(AuthorRole.System, content));
        }

        public void AddToolMessage(string content)
        {
            History.Add(new WeixinAiChatHistory(AuthorRole.Tool, content));
        }

        public void ClearHistory()
        {
            History.Clear();
        }
    }

    /// <summary>
    /// 聊天状态
    /// </summary>
    public enum ChatStatus
    {
        /// <summary>
        /// 默认状态（可能是转换失败）
        /// </summary>
        None,
        /// <summary>
        /// 聊天中
        /// </summary>
        Chat,
        /// <summary>
        /// 暂停
        /// </summary>
        Paused
    }

    /// <summary>
    /// 多模态综合对话状态
    /// </summary>
    public enum MultimodelType
    {
        None,
        SimpleChat,
        ChatAndImage
    }
    
}
