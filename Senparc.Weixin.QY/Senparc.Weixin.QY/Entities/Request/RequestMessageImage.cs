/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageImage.cs
    文件功能描述：接收普通图片消息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    public class RequestMessageImage : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Image; }
        }

        public string MediaId { get; set; }
        public string PicUrl { get; set; }
    }
}
