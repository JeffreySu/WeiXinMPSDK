/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SendResult.cs
    文件功能描述：群发结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 发送信息后的结果
    /// </summary>
    public class SendResult : WxJsonResult
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），图文消息为news
        /// </summary>
        public UploadMediaFileType type { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string msg_id { get; set; }
    }

    public class GetSendResult:WxJsonResult
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string msg_id { get; set; }

        /// <summary>
        /// 消息发送后的状态，SEND_SUCCESS表示发送成功
        /// </summary>
        public string msg_status { get; set; }
    }
}