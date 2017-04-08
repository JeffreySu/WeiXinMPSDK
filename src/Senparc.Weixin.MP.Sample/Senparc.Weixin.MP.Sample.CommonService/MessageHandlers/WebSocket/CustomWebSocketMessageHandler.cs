﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using Senparc.WebSocket;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Sample.CommonService.TemplateMessage.WxOpen;
using Senparc.Weixin.WxOpen.Containers;

namespace Senparc.Weixin.MP.Sample.CommonService.MessageHandlers.WebSocket
{
    /// <summary>
    /// 自定义 WebSocket 处理类
    /// </summary>
    public class CustomWebSocketMessageHandler : WebSocketMessageHandler
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
                //    "在线购买", DateTime.Now, "图书众筹", "1234567890",
                //    100, "400-9939-858", "http://sdk.senparc.weixin.com");

                var data = new
                {
                    keyword1 = new TemplateDataItem("ADD"),
                    keyword2 = new TemplateDataItem(DateTime.Now.ToString()),
                    keyword3 = new TemplateDataItem("Name"),
                    keyword4 = new TemplateDataItem("Number"),
                    keyword5 = new TemplateDataItem(100.ToString("C")),
                    keyword6 = new TemplateDataItem("400-9939-858"),
                };

                var appId = WebConfigurationManager.AppSettings["WxOpenAppId"];//与微信小程序账号后台的AppId设置保持一致，区分大小写。
                var tmResult = Senparc.Weixin.WxOpen.AdvancedAPIs.Template.TemplateApi.SendTemplateMessage(appId, openId, "Oc7R_U_23T8DtVgWn3d__-WkIctx_yDWTg8_4Mx8wgY", data, receivedMessage.FormId, null,
                         null);
            }
            catch (Exception ex)
            {
                await webSocketHandler.SendMessage(ex.Message + "\r\n\r\n" + originalData+"\r\n\r\nAPPID:" + WebConfigurationManager.AppSettings["WxOpenAppId"]);
            }
        }
    }
}
