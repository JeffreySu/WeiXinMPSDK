using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
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
}