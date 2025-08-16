/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotEntityHelper.cs
    文件功能描述：消息实体与json相互转换
    
    
    创建标识：Wang Qian - 20250815
----------------------------------------------------------------*/
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Work.Entities;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;



namespace Senparc.Weixin.Work.Helpers
{
    public class BotEntityHelper
    {
        /// <summary>
        /// 获取请求实体
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>请求实体</returns>
        public static WorkRequestMessageBase GetRequestEntity(string json)
        {
            var baseMessage = SerializerHelper.GetObject<WorkBotRequestMessageBase>(json);
            var requestMessage = new WorkRequestMessageBase();
            var jsonObject = new WorkBotRequestMessageBase();
            switch(baseMessage.msgtype.ToUpper())
            {
                case "TEXT":
                    jsonObject = SerializerHelper.GetObject<BotRequestMessageText>(json);
                    requestMessage = ConvertJsonObjectToRequestMessage(jsonObject); 
                    break;
                case "IMAGE":
                    jsonObject = SerializerHelper.GetObject<BotRequestMessageImage>(json);
                    requestMessage = ConvertJsonObjectToRequestMessage(jsonObject);
                    break;
                case "EVENT":
                    jsonObject = SerializerHelper.GetObject<BotRequestMessageEventBase>(json);
                    requestMessage = ConvertJsonObjectToRequestMessage(jsonObject, json);
                    break;
                default:
                    break;
            }
            return requestMessage;
        }

        /// <summary>
        /// 将json对象转换为请求实体
        /// </summary>
        /// <param name="baseMessage"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static WorkRequestMessageBase ConvertJsonObjectToRequestMessage(WorkBotRequestMessageBase baseMessage, string json = null)
        {
            var requestMessage = new WorkRequestMessageBase();
            //获取消息类型
            var msgType = NeuChar.Helpers.MsgTypeHelper.GetRequestMsgType(baseMessage.msgtype);

            //根据消息类型转换为请求实体
            switch(msgType)
            {
                case RequestMsgType.Text:
                    var textJsonObject = baseMessage as BotRequestMessageText;
                    var textRequestMessage = new RequestMessageText();
                    textRequestMessage.Content = textJsonObject.text.content;
                    textRequestMessage.FromUserName = textJsonObject.from.userid;
                    textRequestMessage.ToUserName = textJsonObject.aibotid;
                    requestMessage = textRequestMessage;
                    break;
                case RequestMsgType.Image:
                    var imageJsonObject = baseMessage as BotRequestMessageImage;
                    var imageRequestMessage = new RequestMessageImage();
                    imageRequestMessage.PicUrl = imageJsonObject.image.url;
                    imageRequestMessage.FromUserName = imageJsonObject.from.userid;
                    imageRequestMessage.ToUserName = imageJsonObject.aibotid;
                    requestMessage = imageRequestMessage;
                    break;
                case RequestMsgType.Event:
                    var eventJsonObject = baseMessage as BotRequestMessageEventBase;
                    switch(eventJsonObject.@event.eventtype)
                    {
                        case "enter_chat":
                            var enterChatJsonObject = SerializerHelper.GetObject<BotRequestMessageEvent_Enter>(json);
                            var enterChatRequestMessage = new RequestMessageEvent_Enter_Agent();

                            requestMessage = enterChatRequestMessage;
                            break;
                        case "template_card_event":
                            var templateCardEventJsonObject = SerializerHelper.GetObject<BotRequestMessageEvent_TemplateCardEvent>(json);
                            var templateCardEventRequestMessage = new RequestMessageEvent_TemplateCardEvent();

                            requestMessage = templateCardEventRequestMessage;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return requestMessage;
        }

    }
}
