using System;
using System.IO;
using System.Xml.Linq;
using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.Tencent;
using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;
using Senparc.Weixin.Work.AdvancedAPIs;
using System.Threading.Tasks;
using System.Threading;

namespace Senparc.Weixin.Work.MessageHandlers
{
    public abstract partial class WorkMessageHandler<TMC>
           : MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>, IWorkMessageHandler
           where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
    {

        public override async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
        {
            //TODO:改写为调用异步方法

            switch (RequestMessage.MsgType)
            {
                case RequestMsgType.Unknown://第三方回调
                    {
                        if (RequestMessage is IThirdPartyInfoBase)
                        {
                            var thirdPartyInfo = RequestMessage as IThirdPartyInfoBase;
                            TextResponseMessage = OnThirdPartyEvent(thirdPartyInfo);
                        }
                        else
                        {
                            throw new WeixinException("没有找到合适的消息类型。");
                        }
                    }
                    break;
                //以下是普通信息
                case RequestMsgType.Text:
                    {
                        var requestMessage = RequestMessage as RequestMessageText;
                        ResponseMessage = OnTextOrEventRequest(requestMessage) ?? OnTextRequest(requestMessage);
                    }
                    break;
                case RequestMsgType.Location:
                    ResponseMessage = OnLocationRequest(RequestMessage as RequestMessageLocation);
                    break;
                case RequestMsgType.Image:
                    ResponseMessage = OnImageRequest(RequestMessage as RequestMessageImage);
                    break;
                case RequestMsgType.Voice:
                    ResponseMessage = OnVoiceRequest(RequestMessage as RequestMessageVoice);
                    break;
                case RequestMsgType.Video:
                    ResponseMessage = OnVideoRequest(RequestMessage as RequestMessageVideo);
                    break;
                case RequestMsgType.ShortVideo:
                    ResponseMessage = OnShortVideoRequest(RequestMessage as RequestMessageShortVideo);
                    break;
                case RequestMsgType.File:
                    ResponseMessage = OnFileRequest(RequestMessage as RequestMessageFile);
                    break;
                case RequestMsgType.Event:
                    {
                        var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();
                        ResponseMessage = OnTextOrEventRequest(requestMessageText) ?? OnEventRequest(RequestMessage as IRequestMessageEventBase);
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
            }
        }

    }
}
