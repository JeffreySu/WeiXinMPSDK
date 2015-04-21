using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.Open.Helpers;

namespace Senparc.Weixin.Open.MessageHandlers
{
    public class ThirdPartyMessageHandler
    {
        public XDocument RequestDocument { get; set; }
        public object RequestMessage { get; set; }

        public string ResponseMessageText { get; set; }

        public ThirdPartyMessageHandler(Stream inputStream)
        {
            RequestDocument = XmlUtility.XmlUtility.Convert(inputStream);//转成XDocument

//转成实体
            RequestMessageBase requestMessage = null;
            InfoType infoType;
            try
            {
                infoType = InfoTypeHelper.GetRequestInfoType(RequestDocument);
                switch (infoType)
                {
                    case InfoType.component_verify_ticket:
                        requestMessage = new RequestMessageComponentVerifyTicket();
                        break;
                    case InfoType.unauthorized:
                        requestMessage = new RequestMessageUnauthorized();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }


            requestMessage.FillEntityWithXml(RequestDocument);

            RequestMessage = requestMessage;
        }

        public void Excute()
        {

        }


    }
}
