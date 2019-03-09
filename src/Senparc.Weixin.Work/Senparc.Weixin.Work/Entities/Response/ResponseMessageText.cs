/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageText.cs
    文件功能描述：响应回复文本消息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class ResponseMessageText : WorkResponseMessageBase, IResponseMessageText
    {
        public new virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Text; }
        }

        public string Content { get; set; }
    }
}
