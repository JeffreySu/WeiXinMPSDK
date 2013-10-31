using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageVoice : RequestMessageBase,IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Voice; }
        }

        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式：amr
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果，UTF8编码
        /// 开通语音识别功能，用户每次发送语音给公众号时，微信会在推送的语音消息XML数据包中，增加一个Recongnition字段。
        /// 注：由于客户端缓存，开发者开启或者关闭语音识别功能，对新关注者立刻生效，对已关注用户需要24小时生效。开发者可以重新关注此帐号进行测试。
        /// </summary>
        public string Recognition { get; set; }
    }
}
