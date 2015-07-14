using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.OpenMessageHandler
{
    /// <summary>
    /// 开放平台全网发布之前需要做的验证
    /// </summary>
    public class OpenCheckMessageHandler : MessageHandler<CustomMessageContext>
    {
        private string testAppId = "wx570bc396a51b8ff8";
        private string componentAppId = WebConfigurationManager.AppSettings["Component_Appid"];
        private string componentSecret = WebConfigurationManager.AppSettings["Component_Secret"];

        public OpenCheckMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {

        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            if (requestMessage.Content == "TESTCOMPONENT_MSG_TYPE_TEXT")
            {
                responseMessage.Content = requestMessage.Content + "from_callback";//固定为TESTCOMPONENT_MSG_TYPE_TEXT_callback
            }
            else if (requestMessage.Content.StartsWith("QUERY_AUTH_CODE:"))
            {
                string openTicket = null;
            var openTicketPath = Server.GetMapPath("~/App_Data/OpenTicket");
                var filePath = Path.Combine(openTicketPath, string.Format("{0}.txt", componentAppId));
                if (File.Exists(filePath))
                {
                     using (TextReader tr = new StreamReader(filePath))
                 {
                     openTicket = tr.ReadToEnd();
                 }
                }
                else
                {
                    throw new Exception("OpenTicket不存在："+componentAppId);
                }

                var componentAccessTokenResult = Open.CommonAPIs.CommonApi.GetComponentAccessToken(componentAppId,componentSecret,openTicket);
                var authorization_code = requestMessage.Content.Replace("QUERY_AUTH_CODE:","");
                var query_auth_code = Open.AdvancedAPIs.OAuth.OAuthApi.GetAccessToken(testAppId, componentAppId,
                    componentAccessTokenResult.component_access_token, authorization_code);

                var content = query_auth_code + "_from_api";

                //调用客服接口
                AdvancedAPIs.Custom.CustomApi.SendText(authorization_code, requestMessage.FromUserName, content);

                return null;
            }
            return responseMessage;
        }

        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = requestMessage.Event + "from_callback";
            return responseMessage;
        }

        public override Entities.IResponseMessageBase DefaultResponseMessage(Entities.IRequestMessageBase requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "默认消息";
            return responseMessage;
        }
    }
}
