using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Senparc.Weixin.MP.Sample.Service
{
    using Senparc.Weixin.MP.Entities;
    using Senparc.Weixin.MP.Entities.GoogleMap;
    using Senparc.Weixin.MP.Helpers;

    public class EventService
    {
        public ResponseMessageBase GetResponseMessage(RequestMessageEventBase requestMessage)
        {
            ResponseMessageBase responseMessage = null;
            switch (requestMessage.Event)
            {
                case Event.ENTER:
                    {
                        var strongResponseMessage = ResponseMessageBase.CreateFromRequestMessage(requestMessage,
                                                                                                 ResponseMsgType.Text)
                                                    as ResponseMessageText;
                        strongResponseMessage.Content = "您刚才发送了ENTER事件请求。";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.LOCATION:
                    throw new Exception("暂不可用");
                    break;
                case Event.subscribe://订阅
                    {
                        var strongResponseMessage = ResponseMessageBase.CreateFromRequestMessage(requestMessage,
                                                                                       ResponseMsgType.Text) as ResponseMessageText;

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
                        var strongResponseMessage = ResponseMessageBase.CreateFromRequestMessage(requestMessage,
                                                                                       ResponseMsgType.Text) as ResponseMessageText;
                        strongResponseMessage.Content = "有空再来";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.CLICK://菜单点击事件，根据自己需要修改
                    throw new Exception("Demo中还没有加入CLICK的测试！");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return responseMessage;         
        }
    }
}