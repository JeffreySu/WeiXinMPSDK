/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageVoice.cs
    文件功能描述：响应回复语音消息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 需要预先上传多媒体文件到微信服务器，只支持认证服务号。
    /// </summary>
    public class ResponseMessageVoice : WorkResponseMessageBase, IResponseMessageVoice
    {
        public new virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Voice; }
        }

        public Voice Voice { get; set; }

        public ResponseMessageVoice()
        {
            Voice = new Voice();
        }
    }
}
