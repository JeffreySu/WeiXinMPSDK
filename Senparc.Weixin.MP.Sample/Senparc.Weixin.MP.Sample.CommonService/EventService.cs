/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：EventService.cs
    文件功能描述：事件处理程序，此代码的简化MessageHandler方法已由/CustomerMessageHandler/CustomerMessageHandler_Event.cs完成
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Web;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Sample.CommonService
{
    /// <summary>
    /// 事件处理程序，此代码的简化MessageHandler方法已由/CustomerMessageHandler/CustomerMessageHandler_Event.cs完成，
    /// 此文件不再更新。
    /// </summary>
    public class EventService
    {
        public ResponseMessageBase GetResponseMessage(RequestMessageEventBase requestMessage)
        {
            ResponseMessageBase responseMessage = null;
            switch (requestMessage.Event)
            {
                case Event.ENTER:
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您刚才发送了ENTER事件请求。";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.LOCATION:
                    throw new Exception("暂不可用");
                    //break;
                case Event.subscribe://订阅
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();

                        //获取Senparc.Weixin.MP.dll版本信息
                        var fileVersionInfo = FileVersionInfo.GetVersionInfo(HttpContext.Current.Server.MapPath("~/bin/Senparc.Weixin.MP.dll"));
                        var version = fileVersionInfo.FileVersion;
                        strongResponseMessage.Content = string.Format(
                            "欢迎关注【Senparc.Weixin.MP 微信公众平台SDK】，当前运行版本：v{0}。\r\n您还可以发送【位置】【图片】【语音】信息，查看不同格式的回复。\r\nSDK官方地址：http://weixin.senparc.com",
                            version);
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.unsubscribe://退订
                    {
                        //实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
                        //unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "有空再来";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.CLICK://菜单点击事件，根据自己需要修改
                    //这里的CLICK在此DEMO中不会被执行到，因为已经重写了OnEvent_ClickRequest
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return responseMessage;         
        }
    }
}