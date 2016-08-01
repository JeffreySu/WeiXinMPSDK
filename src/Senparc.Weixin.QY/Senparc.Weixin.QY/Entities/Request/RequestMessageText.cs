/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageText.cs
    文件功能描述：接收普通文本消息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    public class RequestMessageText : RequestMessageBase,IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }
        public string Content { get; set; }
    }
}
