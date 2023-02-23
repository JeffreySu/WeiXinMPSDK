using Senparc.CO2NET.Extensions;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.WxOpen.MessageContexts;
using Senparc.Weixin.WxOpen.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.WxOpen.Tests.MessageHandlers.TestEntities
{
    public class CustomWxOpenMessageHandler : WxOpenMessageHandler<DefaultWxOpenMessageContext>
    {
        public CustomWxOpenMessageHandler(XDocument requestDocument) : base(requestDocument, null)
        {
        }

        public override IResponseMessageBase OnEvent_MediaCheckRequest(RequestMessageEvent_MediaCheck requestMessage)
        {
            Console.WriteLine("OnEvent_MediaCheckRequest 成功接收信息：" + requestMessage.ToJson(true));
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            return new SuccessResponseMessage();
        }
    }
}
