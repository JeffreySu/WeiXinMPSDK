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
            switch((baseMessage?.msgtype ?? string.Empty).Trim().ToUpper())
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

        /// <summary>
        /// 获取回复实体（从 JSON 入手，仿照 GetRequestEntity 的流程）
        /// 1) 先以基类获取 msgtype；2) 反序列化为具体回复 JSON 模型；3) 转为统一的 WorkResponseMessageBase
        /// </summary>
        /// <param name="json">机器人回复 JSON 字符串</param>
        /// <returns>统一回复实体 WorkResponseMessageBase</returns>
        public static WorkResponseMessageBase GetResponseEntity(string json)
        {
            var baseMessage = SerializerHelper.GetObject<WorkBotResponseMessageBase>(json);
            var responseMessage = new WorkResponseMessageBase();
            var jsonObject = new WorkBotResponseMessageBase();

            switch ((baseMessage?.msgtype ?? string.Empty).Trim().ToUpper())
            {
                case "TEXT":
                    jsonObject = SerializerHelper.GetObject<BotResponseMessageText>(json);
                    responseMessage = ConvertJsonObjectToResponseMessage(jsonObject, json);
                    break;
                case "TEMPLATE_CARD":
                    jsonObject = SerializerHelper.GetObject<BotResponseMessageTemplateCard>(json);
                    responseMessage = ConvertJsonObjectToResponseMessage(jsonObject, json);
                    break;
                case "STREAM":
                    jsonObject = SerializerHelper.GetObject<BotResponseMessageStream>(json);
                    responseMessage = ConvertJsonObjectToResponseMessage(jsonObject, json);
                    break;
                default:
                    break;
            }

            return responseMessage;
        }

        /// <summary>
        /// 将回复 json 对象转换为统一的回复实体
        /// </summary>
        /// <param name="baseMessage">具体的回复 JSON 基类对象（通过 msgtype 判型）</param>
        /// <param name="json">原始 JSON（如需二次反序列化可用）</param>
        /// <returns>统一回复实体 WorkResponseMessageBase</returns>
        public static WorkResponseMessageBase ConvertJsonObjectToResponseMessage(WorkBotResponseMessageBase baseMessage, string json = null)
        {
            var responseMessage = new WorkResponseMessageBase();

            var msgType = NeuChar.Helpers.MsgTypeHelper.GetResponseMsgType(baseMessage.msgtype);
            switch (msgType)
            {
                case ResponseMsgType.Text:
                    var textJsonObject = baseMessage as BotResponseMessageText;
                    // TODO: 在此将 textJsonObject 映射为具体的 ResponseMessageText 并赋给 responseMessage
                    // var textResponse = new ResponseMessageText();
                    // textResponse.Content = textJsonObject?.text?.content;
                    // responseMessage = textResponse;
                    break;

                // 缺少模板卡片类型枚举
                // case ResponseMsgType.TemplateCard:
                //     var templateCardJsonObject = baseMessage as BotResponseMessageTemplateCard;
                //     // TODO: 在此将 templateCardJsonObject 映射为具体的回复实体并赋给 responseMessage
                //     break;

                case ResponseMsgType.Stream:
                    var streamJsonObject = baseMessage as BotResponseMessageStream;
                    // TODO: 在此将 streamJsonObject 映射为具体的回复实体并赋给 responseMessage
                    break;

                default:
                    // TODO: 其他类型
                    break;
            }

            return responseMessage;
        }
        
        /// <summary>
        /// 获取回复消息的json字符串
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static string GetResponseMsgString(WorkResponseMessageBase responseMessage)
        {
            switch(responseMessage.MsgType)
            {
                case ResponseMsgType.Text:
                    var jsonObjectText = ConvertResponseMessageToJsonObject(responseMessage);
                    return SerializerHelper.GetJsonString(jsonObjectText);
                case ResponseMsgType.Stream:
                    var jsonObjectStream = ConvertResponseMessageToJsonObject(responseMessage);
                    return SerializerHelper.GetJsonString(jsonObjectStream);
                // case ResponseMsgType.TemplateCard:
                //     var jsonObjectTemplateCard = ConvertResponseMessageToJsonObject(responseMessage);
                //     return SerializerHelper.GetJsonString(jsonObjectTemplateCard);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 将标准回复实体转换为json对象
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static WorkBotResponseMessageBase ConvertResponseMessageToJsonObject(WorkResponseMessageBase responseMessage)
        {
            var responseJsonObject = new WorkBotResponseMessageBase();
            switch(responseMessage.MsgType)
            {
                case ResponseMsgType.Text:
                    var textResponseMessage = responseMessage as ResponseMessageText;
                    var textJsonObject = new BotResponseMessageText();

                    //创建一个text对象，避免空引用
                    textJsonObject.text = new BotResponseMessageText.TextContent();
                    textJsonObject.text.content = textResponseMessage.Content;
                    responseJsonObject = textJsonObject;
                    break;
                case ResponseMsgType.Stream:
                    // var streamResponseMessage = responseMessage as ResponseMessageStream;
                    // var streamJsonObject = new BotResponseMessageStream();
                    // responseJsonObject = streamJsonObject;
                    break;
                //case ResponseMsgType.TemplateCard:
                    // var templateCardResponseMessage = responseMessage as ResponseMessageTemplateCard;
                    // var templateCardJsonObject = new BotResponseMessageTemplateCard();
                    // responseJsonObject = templateCardJsonObject;
                    // break;
                default:
                    break;
            }
            return responseJsonObject;
        }

    }
}
