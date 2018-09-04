using Senparc.NeuChar.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers
{
  public  class MpMessageEntityEnlighten
    {
        public static MessageEntityEnlighten Enlighten = new MessageEntityEnlighten() {
             #region 请求消息

        public Func<IRequestMessageText> NewRequestMessageText { get; } = () => null;
        public Func<IRequestMessageLocation> NewRequestMessageLocation { get; } = () => null;
        public Func<IRequestMessageImage> NewRequestMessageImage { get; } = () => null;
        public Func<IRequestMessageVoice> NewRequestMessageVoice { get; } = () => null;
        public Func<IRequestMessageVideo> NewRequestMessageVideo { get; } = () => null;
        public Func<IRequestMessageLink> NewRequestMessageLink { get; } = () => null;
        public Func<IRequestMessageShortVideo> NewRequestMessageShortVideo { get; } = () => null;
        public Func<IRequestMessageEvent> NewRequestMessageEvent { get; } = () => null;
        public Func<IRequestMessageFile> NewRequestMessageFile { get; } = () => null;


        #endregion

        #region 响应消息

        public Func<IResponseMessageText> NewResponseMessageText { get; } = () => null;
        public Func<IResponseMessageNews> NewResponseMessageNews { get; } = () => null;
        public Func<IResponseMessageMusic> NewResponseMessageMusic { get; } = () => null;
        public Func<IResponseMessageImage> NewResponseMessageImage { get; } = () => null;
        public Func<IResponseMessageVoice> NewResponseMessageVoice { get; } = () => null;
        public Func<IResponseMessageVideo> NewResponseMessageVideo { get; } = () => null;
        public Func<IResponseMessageTransfer_Customer_Service> NewResponseMessageTransfer_Customer_Service { get; } = () => null;

        #endregion
    };
    }
}
