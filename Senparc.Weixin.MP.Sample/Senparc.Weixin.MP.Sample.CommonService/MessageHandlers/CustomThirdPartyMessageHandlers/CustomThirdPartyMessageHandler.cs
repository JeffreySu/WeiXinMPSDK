using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Open;
using Senparc.Weixin.Open.MessageHandlers;
using System.IO;
using Senparc.Weixin.Open.Entities.Request;

namespace Senparc.Weixin.MP.Sample.CommonService.ThirdPartyMessageHandlers
{
    public class CustomThirdPartyMessageHandler : ThirdPartyMessageHandler
    {
        public CustomThirdPartyMessageHandler(Stream inputStream,PostModel encryptPostModel)
            : base(inputStream, encryptPostModel)
        { }

        public override string OnComponentVerifyTicketRequest(RequestMessageComponentVerifyTicket requestMessage)
        {
            return base.OnComponentVerifyTicketRequest(requestMessage);
        }

        public override string OnUnauthorizedRequest(RequestMessageUnauthorized requestMessage)
        {
            return base.OnUnauthorizedRequest(requestMessage);
        }
    }
}
