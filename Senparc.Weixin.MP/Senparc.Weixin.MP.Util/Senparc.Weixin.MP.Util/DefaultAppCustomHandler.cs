

using System;
using System.Text;
using System.Text.RegularExpressions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Util.Content;

namespace Senparc.Weixin.MP.Util
{
    public class DefaultAppCustomHandler:IAppCustomHandler
    {



        public IResponseMessageBase SubscribeRequest(AppCtx ctx, RequestMessageEvent_Subscribe requestMessage)
        {
            String content = ctx.GetConfig().WxWelcomeMessage;
            //if (content.Trim().Length == 0)
            //{
            //   Config.Logln("关注：素材");
            //    return WeixinRender.Render(this, "welcome");
            // }
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = content;

            //Config.Logln("关注:消息:");
            return responseMessage;
        }


        public IResponseMessageBase UnsubscribeRequest(AppCtx ctx, RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "有空再来";
            return responseMessage;
        }

        public IResponseMessageBase ScancodePushRequest(AppCtx ctx, RequestMessageEvent_Scancode_Push requestMessage)
        {
            throw new NotImplementedException();
        }

        public IResponseMessageBase ClickEventRequest(AppCtx ctx, string eventKey)
        {
            IResponseMessageBase reponseMessage = null;
            //菜单点击，需要跟创建菜单时的Key匹配
            switch (eventKey)
            {
                case "OneClick":
                    {
                        //这个过程实际已经在OnTextOrEventRequest中完成，这里不会执行到。
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
                    }
                    break;
                case "SubClickRoot_Text":
                    {
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "您点击了子菜单按钮。";
                    }
                    break;
                case "product_promotion":
                    {

                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageNews>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "您点击了子菜单图文按钮",
                            Description = "您点击了子菜单图文按钮，这是一条图文信息。",
                            PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg",
                            Url = "http://weixin.senparc.com"
                        });
                    }
                    break;
                case "SubClickRoot_Music":
                    {
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageMusic>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Music.MusicUrl = "http://weixin.senparc.com/Content/music1.mp3";
                    }
                    break;
                case "SubClickRoot_Image":
                    {
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageImage>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Image.MediaId = "Mj0WUTZeeG9yuBKhGP7iR5n1xUJO9IpTjGNC4buMuswfEOmk6QSIRb_i98do5nwo";
                    }
                    break;

                case "OAuth"://OAuth授权测试
                    {
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageNews>();
                        strongResponseMessage.Articles.Add(new Article()
                        {
                            Title = "用户登陆",
                            Description = "点击【查看全文】进入授权页面。",
                            Url = ctx.GetConfig().ApiDomain + "oauth2",
                            PicUrl = "http://weixin.senparc.com/Images/qrcode.jpg"
                        });
                        reponseMessage = strongResponseMessage;
                    }
                    break;
                case "Description":
                    {
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = ctx.GetConfig().WxWelcomeMessage;
                        reponseMessage = strongResponseMessage;
                    }
                    break;
                default:
                    {
                        var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您点击了按钮，EventKey：" + eventKey;
                        reponseMessage = strongResponseMessage;
                    }
                    break;
            }

            return reponseMessage;
        }


        public IResponseMessageBase TextOrEventRequest(AppCtx ctx, RequestMessageText requestMessage)
        {
            return null;
//            string txt = requestMessage.Content.ToLower();
//            if (Regex.IsMatch(txt, "^(a|b|c)$", RegexOptions.IgnoreCase))
//            {
//                return WeixinRender.Render(this, "res:" + txt);
//            }
//
//            if (!String.IsNullOrEmpty(Variables.WxDefaultResponseMessage))
//            {
//                var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
//                strongResponseMessage.Content = Variables.WxDefaultResponseMessage;
//                return strongResponseMessage;
//            }
//            return null;


            // 预处理文字或事件类型请求。
            // 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
            // 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
            // 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
            // 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
            // 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey
            return null;

//            if (requestMessage.Content == "OneClick")
//            {
//                var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
//                strongResponseMessage.Content = "您点击了底部按钮。\r\n为了测试微信软件换行bug的应对措施，这里做了一个——\r\n换行";
//                return strongResponseMessage;
//            }
            return null;//返回null，则继续执行OnTextRequest或OnEventRequest
        }


        public IResponseMessageBase EnterEventRequest(AppCtx ctx, RequestMessageEvent_Enter requestMessage)
        {
            return null;
            //            String content = Variables.WxEnterMessage;
            //            if (content.Trim().Length == 0)
            //            {
            //                Config.Logln("进入(素材)");
            //                return WeixinRender.Render(this, "enter");
            //            }
            //            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            //            responseMessage.Content = content;
            //            //responseMessage.Content = "您刚才发送了ENTER事件请求。";
            //
            //            Config.Logln("进入(设置)"+responseMessage.Content);
            //            return responseMessage;
        }


        public IResponseMessageBase TextRequest(AppCtx ctx, RequestMessageText requestMessage)
        {

            //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs

//            string txt = requestMessage.Content.ToLower();
//            if (Regex.IsMatch(txt, "^(a|b|c)$", RegexOptions.IgnoreCase))
//            {
//                return WeixinRender.Render(this, "res:" + txt);
//            }


            //方法一（v0.1），此方法调用太过繁琐，已过时（但仍是所有方法的核心基础），建议使用方法二到四
            //var responseMessage =
            //    ResponseMessageBase.CreateFromRequestMessage(RequestMessage, ResponseMsgType.Text) as
            //    ResponseMessageText;

            //方法二（v0.4）
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);

            //方法三（v0.4），扩展方法，需要using Senparc.Weixin.MP.Helpers;
            //var responseMessage = RequestMessage.CreateResponseMessage<ResponseMessageText>();

            //方法四（v0.6+），仅适合在HandlerMessage内部使用，本质上是对方法三的封装
            //注意：下面泛型ResponseMessageText即返回给客户端的类型，可以根据自己的需要填写ResponseMessageNews等不同类型。
            var responseMessage = ctx.CreateResponseMessage<ResponseMessageText>();

            if (requestMessage.Content == "测试")
            {
                responseMessage.Content = "测试完成！";
            }
            else
            {
                var result = new StringBuilder();
                result.AppendFormat("您刚才发送了文字信息：{0}\r\n\r\n", requestMessage.Content);

                var msgCtx = ctx.ContextHandler.CurrentMessageContext;
                if (msgCtx.RequestMessages.Count > 1)
                {
                    result.AppendFormat("您刚才还发送了如下消息（{0}/{1}）：\r\n",msgCtx.RequestMessages.Count,
                        msgCtx.StorageData);
                    for (int i = msgCtx.RequestMessages.Count - 2; i >= 0; i--)
                    {
                        var historyMessage = msgCtx
                            .RequestMessages[i];
                        result.AppendFormat("{0} 【{1}】{2}\r\n",
                                            historyMessage.CreateTime.ToShortTimeString(),
                                            historyMessage.MsgType.ToString(),
                                            (historyMessage is RequestMessageText)
                                                ? (historyMessage as RequestMessageText).Content
                                                : "[非文字类型]"
                            );
                    }
                    result.AppendLine("\r\n");
                }

                responseMessage.Content = result.ToString();
            }
            return responseMessage;
        }


        public IResponseMessageBase RequestAgent(AppCtx ctx, IRequestMessageBase requestMessage)
        {

            /* 所有没有被处理的消息会默认返回这里的结果，
             * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
             * 只需要在这里统一发出委托请求，如：
             * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
             * return responseMessage;
             */

            String defaultMsg = ctx.GetConfig().WxDefaultResponseMessage;
            if (!String.IsNullOrEmpty(defaultMsg))
            {
                var strongResponseMessage = ctx.CreateResponseMessage<ResponseMessageText>();
                strongResponseMessage.Content =defaultMsg;
                return strongResponseMessage;
            }
            return null;
        }
    }
}
