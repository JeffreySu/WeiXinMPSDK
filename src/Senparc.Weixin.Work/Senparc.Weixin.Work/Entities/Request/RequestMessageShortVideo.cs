/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageShortVideo.cs
    文件功能描述：接收小视频消息
    
    
    创建标识：Senparc - 20150708
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class RequestMessageShortVideo : WorkRequestMessageBase, IRequestMessageShortVideo
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.ShortVideo; }
        }

        public string MediaId { get; set;}
        public string ThumbMediaId { get; set; }
    }
}
