/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageFile.cs
    文件功能描述：接收文件消息
    
    
    创建标识：Senparc - 20180216

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 文件消息请求
    /// </summary>
    public class RequestMessageFile : WorkRequestMessageBase, IRequestMessageFile
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.File; }
        }

        public string Title { get; set;}
        public string Description { get; set; }
        public string FileKey { get; set; }
        public string FileMd5 { get; set; }
        public long FileTotalLen { get; set; }
    }
}
