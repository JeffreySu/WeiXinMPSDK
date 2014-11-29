using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    public class RequestMessageVoice : RequestMessageBase,IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Voice; }
        }

        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式：amr
        /// </summary>
        public string Format { get; set; }
    }
}
