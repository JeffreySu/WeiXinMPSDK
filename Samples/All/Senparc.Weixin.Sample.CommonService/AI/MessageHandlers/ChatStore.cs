/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：ChatStore.cs
    文件功能描述：按个人信息隔离的 Chat 缓存
    
    
    创建标识：Senparc - 20240524

----------------------------------------------------------------*/

using Senparc.AI.Kernel.Handlers;

namespace Senparc.Weixin.MP.Sample.CommonService.AI.MessageHandlers
{
    /// <summary>
    /// 按个人信息隔离的 Chat 缓存
    /// </summary>
    public class ChatStore
    {
        public ChatStatus Status { get; set; }

        public MultimodelType MultimodelType { get; set; }
        public string History { get; set; }

        public ChatStore()
        {
            Status = ChatStatus.None;
            MultimodelType = MultimodelType.None;
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
