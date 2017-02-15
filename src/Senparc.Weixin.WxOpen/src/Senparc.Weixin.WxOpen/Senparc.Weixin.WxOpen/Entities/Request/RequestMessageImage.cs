/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：RequestMessageImage.cs
    文件功能描述：接收普通图片消息
    
    
    创建标识：Senparc - 20170106
----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    public class RequestMessageImage : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Image; }
        }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
    }
}
