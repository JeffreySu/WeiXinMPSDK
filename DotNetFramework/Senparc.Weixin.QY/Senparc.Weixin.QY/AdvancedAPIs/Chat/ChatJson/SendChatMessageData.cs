/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SendChatMessageData.cs
    文件功能描述：会话接口返回结果
    
    
    创建标识：Senparc - 20150728
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.AdvancedAPIs.Chat
{
    /// <summary>
    /// 发送消息基础数据
    /// </summary>
    public class BaseSendChatMessageData 
    {
        /// <summary>
        /// 接收人
        /// </summary>
        public Receiver receiver { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string sender { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }
    }

    public class Receiver
    {
        /// <summary>
        /// 接收人类型：single|group，分别表示：群聊|单聊
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 接收人的值，为userid|chatid，分别表示：成员id|会话id
        /// </summary>
        public string id { get; set; }
    }

    /// <summary>
    /// 发送text消息数据
    /// </summary>
    public class SendTextMessageData : BaseSendChatMessageData
    {
        public Chat_Content text { get; set; }
    }

    public class Chat_Content
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string content { get; set; }
    }

    /// <summary>
    /// 发送image消息数据
    /// </summary>
    public class SendImageMessageData : BaseSendChatMessageData
    {
        public Chat_Image image { get; set; }
    }

    public class Chat_Image
    {
        /// <summary>
        /// 图片媒体文件id，可以调用上传素材文件接口获取
        /// </summary>
        public string media_id { get; set; }
    }

    /// <summary>
    /// 发送file消息数据
    /// </summary>
    public class SendFileMessageData : BaseSendChatMessageData
    {
        public Chat_File file { get; set; }
    }

    public class Chat_File
    {
        /// <summary>
        /// 图片媒体文件id，可以调用上传素材文件接口获取
        /// </summary>
        public string media_id { get; set; }
    }
}