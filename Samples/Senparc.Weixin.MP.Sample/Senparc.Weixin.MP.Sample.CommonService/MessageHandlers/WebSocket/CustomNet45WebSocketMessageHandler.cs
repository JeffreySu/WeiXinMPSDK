//DPBMARK_FILE WebSocket
#if NET45

using System;
using System.Linq;
using System.Threading.Tasks;
using Senparc.WebSocket;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.WxOpen.Containers;
using System.Web.Configuration;
using Senparc.Weixin.MP.Sample.CommonService.TemplateMessage.WxOpen;


namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.WebSocket
{
    /// <summary>
    /// 自定义 WebSocket 处理类
    /// </summary>
    public class CustomNet45WebSocketMessageHandler : WebSocketMessageHandler
    {
        public override Task OnConnecting(WebSocketHelper webSocketHandler)
        {
            //TODO:处理连接时的逻辑
            return base.OnConnecting(webSocketHandler);
        }

        public override Task OnDisConnected(WebSocketHelper webSocketHandler)
        {
            //TODO:处理断开连接时的逻辑
            return base.OnDisConnected(webSocketHandler);
        }

        public override async Task OnMessageReceiced(WebSocketHelper webSocketHandler, ReceivedMessage receivedMessage, string originalData)
        {
            if (receivedMessage == null || string.IsNullOrEmpty(receivedMessage.Message))
            {
                return;
            }

            var message = receivedMessage.Message;

            await webSocketHandler.SendMessage("originalData：" + originalData);
            await webSocketHandler.SendMessage("您发送了文字：" + message);
            await webSocketHandler.SendMessage("正在处理中...");

            await Task.Delay(1000);

            //处理文字
            var result = string.Concat(message.Reverse());
            await webSocketHandler.SendMessage(result);

#if NET45
            var appId = Config.SenparcWeixinSetting.WxOpenAppId;//与微信小程序账号后台的AppId设置保持一致，区分大小写。
#else
            var appId = "WxOpenAppId";//与微信小程序账号后台的AppId设置保持一致，区分大小写。
#endif


            try
            {
                //发送模板消息
                var formId = receivedMessage.FormId;//发送模板消息使用，需要在wxml中设置<form report-submit="true">

                var sessionBag = SessionContainer.GetSession(receivedMessage.SessionId);

                //临时演示使用固定openId
                var openId = sessionBag != null ? sessionBag.OpenId : "onh7q0DGM1dctSDbdByIHvX4imxA";// "用户未正确登陆";

                await webSocketHandler.SendMessage("OpenId：" + openId);
                //await webSocketHandler.SendMessage("FormId：" + formId);

                if (sessionBag == null)
                {
                    openId = "onh7q0DGM1dctSDbdByIHvX4imxA";//临时测试
                }

                //var data = new WxOpenTemplateMessage_PaySuccessNotice(
                //    "在线购买", SystemTime.Now, "图书众筹", "1234567890",
                //    100, "400-9939-858", "http://sdk.senparc.weixin.com");

                var data = new
                {
                    keyword1 = new TemplateDataItem("来自小程序WebSocket的模板消息"),
                    keyword2 = new TemplateDataItem(SystemTime.Now.LocalDateTime.ToString()),
                    keyword3 = new TemplateDataItem("Name"),
                    keyword4 = new TemplateDataItem("Number"),
                    keyword5 = new TemplateDataItem(100.ToString("C")),
                    keyword6 = new TemplateDataItem("400-031-8816"),
                };

                var tmResult = Senparc.Weixin.WxOpen.AdvancedAPIs.Template.TemplateApi.SendTemplateMessage(appId, openId, "Ap1S3tRvsB8BXsWkiILLz93nhe7S8IgAipZDfygy9Bg", data, receivedMessage.FormId, "pages/websocket/websocket", "websocket",
                         null);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\r\n\r\n" + originalData + "\r\n\r\nAPPID:" + appId;

                await webSocketHandler.SendMessage(msg); //VS2017以下如果编译不通过，可以注释掉这一行

                WeixinTrace.SendCustomLog("WebSocket OnMessageReceiced()过程出错", msg);
            }
        }
    }
}
#endif
