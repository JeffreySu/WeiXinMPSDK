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
                responseMessage.Content = requestMessage.Content + "_callback";//固定为TESTCOMPONENT_MSG_TYPE_TEXT_callback
            }
            else if (requestMessage.Content.StartsWith("QUERY_AUTH_CODE:"))
            {
                var openTicketPath = Server.GetMapPath("~/App_Data/OpenTicket");
                using (TextWriter tw = new StreamWriter(Path.Combine(openTicketPath, string.Format("{0}.txt", DateTime.Now.Ticks)), false))
                {
                    string openTicket = null;
                    var filePath = Path.Combine(openTicketPath, string.Format("{0}.txt", componentAppId));
                    if (File.Exists(filePath))
                    {
                        using (TextReader tr = new StreamReader(filePath))
                        {
                            openTicket = tr.ReadToEnd();
                            tw.WriteLine("openTicket:" + openTicket);
                            tw.Flush();
                        }
                    }
                    else
                    {
                        tw.WriteLine("openTicket缓存文件不存在！");
                        tw.Flush();
                    }


                    var query_auth_code = requestMessage.Content.Replace("QUERY_AUTH_CODE:", "");
                    tw.WriteLine("query_auth_code:" + query_auth_code);
                    tw.Flush();

                    try
                    {
                        var component_access_token = Open.CommonAPIs.CommonApi.GetComponentAccessToken(componentAppId, componentSecret, openTicket).component_access_token;
                        tw.WriteLine("component_access_token:" + component_access_token);
                        tw.Flush();

                        var oauthResult = Open.OAuthJoin.OAuthJoinAPI.GetJoinAccessToken(component_access_token, componentAppId, query_auth_code);
                        tw.WriteLine("oauthResult:" + oauthResult);
                        tw.Flush();

                        //调用客服接口
                        var content = query_auth_code + "_from_api";
                        var sendResult = AdvancedAPIs.Custom.CustomApi.SendText(oauthResult.authorization_info.authorizer_access_token,
                              requestMessage.FromUserName, content);
                        tw.WriteLine("sendResult:" + sendResult.errcode);
                        tw.Flush();
                    }
                    catch (Exception ex)
                    {
                        tw.WriteLine("error:" + ex.Message);
                        tw.WriteLine(ex.StackTrace);
                    }

                    tw.Close();
                }
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
