/*----------------------------------------------------------------
    Copyright (C) 2015 LSW
    
    文件名：ResponseMessageText.cs
    文件功能描述：响应回复文本消息
    
    
    创建标识：LSW - 20150313
    
    修改标识：LSW - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    public class ResponseMessageText : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Text; }
        }

        public string Content { get; set; }
    }
}
