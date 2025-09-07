/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotEntityHelper.cs
    文件功能描述：NeuChar标准的消息实体与json字符串的相互转换
    因为MessageHandler中对消息的处理是基于WorkRequestMessageBase和WorkResponseMessageBase的，
    所以需要将json字符串转换为WorkRequestMessageBase和WorkResponseMessageBase，
    然后调用MessageHandler中的方法进行处理，最后将处理结果转换为json字符串。这样做是为了节约代码，符合标准。
    
    
    创建标识：Wang Qian - 20250815

    修改标识: Wang Qian - 20250825
    修改描述:
    重构转换方法，增强可维护性和运行效率
----------------------------------------------------------------*/
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Work.Entities;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using System;
using Senparc.CO2NET.Extensions;



namespace Senparc.Weixin.Work.Helpers
{
    public class BotEntityHelper
    {


        /// <summary>
        /// 创建请求实例并为请求消息设置公共字段
        /// </summary>
        /// <typeparam name="T">请求消息实体类型</typeparam>
        /// <param name="requestMessage">请求消息实体</param>
        /// <param name="baseMessage">基础消息对象</param>
        private static T SetRequestCommonProperties<T>(WorkBotRequestMessageBase baseMessage) where T : WorkRequestMessageBase, new()
        {
            var requestMessage = new T();
            requestMessage.FromUserName = baseMessage.from.userid;
            requestMessage.ToUserName = baseMessage.aibotid;
            requestMessage.MsgId = baseMessage.msgid;
            return requestMessage;
        }

        /// <summary>
        /// 为回复消息设置公共字段
        /// </summary>
        /// <typeparam name="T">回复消息实体类型</typeparam>
        /// <param name="responseMessage">回复消息实体</param>
        /// <param name="baseMessage">基础消息对象</param>
        /// <remarks>
        /// 注意：由于WorkResponseMessageBase的MsgType属性是只读的，
        /// 每个具体的响应消息类型会在自己的类中重写MsgType属性来设置正确的类型
        /// </remarks>
        private static T SetResponseCommonProperties<T>(WorkBotResponseMessageBase baseMessage) where T : WorkResponseMessageBase, new()
        {
            var responseMessage = new T();
            // 目前回复消息的公共字段只有msgtype，但这个字段由各个子类自己设置
            // 这里可以设置其他公共字段（如果将来有的话）
            // responseMessage.CreateTime = DateTime.Now; // 示例
            return responseMessage;
        }


        /// <summary>
        /// 获取请求实体
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>请求实体</returns>
        public static WorkRequestMessageBase GetRequestEntity(string json)
        {
            var baseMessage = SerializerHelper.GetObject<WorkBotRequestMessageBase>(json);
            var msgType = NeuChar.Helpers.MsgTypeHelper.GetRequestMsgType(baseMessage.msgtype);
            WorkRequestMessageBase requestMessage = null;
            
            //先判断是否是Text，因为Text最常用，这样运行效率最高
            if (msgType == RequestMsgType.Text)
            {
                // 直接创建 RequestMessageText 实例
                var textJsonObject = SerializerHelper.GetObject<BotRequestMessageText>(json);
                var textRequestMessage = SetRequestCommonProperties<RequestMessageText>(baseMessage);
                
                // 对特有属性赋值   
                textRequestMessage.Content = textJsonObject.text.content;
                requestMessage = textRequestMessage;
            }
            else
            {
                switch(msgType)
                {
                    case RequestMsgType.Image:
                        var imageJsonObject = SerializerHelper.GetObject<BotRequestMessageImage>(json);

                        //创建请求消息实体,并设置共有字段
                        var imageRequestMessage = SetRequestCommonProperties<RequestMessageImage>(baseMessage);
                        
                        // 对特有属性赋值   
                        imageRequestMessage.PicUrl = imageJsonObject.image.url;
                        requestMessage = imageRequestMessage;
                        break;
                    case RequestMsgType.Event:
                        var eventJsonObject = SerializerHelper.GetObject<BotRequestMessageEventBase>(json);
                        switch(eventJsonObject.@event.eventtype)
                        {
                            case "enter_chat":
                                var enterChatJsonObject = SerializerHelper.GetObject<BotRequestMessageEvent_Enter>(json);

                                //创建请求消息实体,并设置共有字段
                                var enterChatRequestMessage = SetRequestCommonProperties<RequestMessageEvent_Enter_Agent>(baseMessage);
                                
                                
                                // 对特有属性赋值   
                                requestMessage = enterChatRequestMessage;
                                break;
                            case "template_card_event":
                                var templateCardEventJsonObject = SerializerHelper.GetObject<BotRequestMessageEvent_TemplateCardEvent>(json);

                                //创建请求消息实体,并设置共有字段
                                var templateCardEventRequestMessage = SetRequestCommonProperties<RequestMessageEvent_TemplateCardEvent>(baseMessage);
                                
                                // 对特有属性赋值   
                                requestMessage = templateCardEventRequestMessage;
                                break;
                            default:
                                throw new NotSupportedException($"不支持的事件类型: {eventJsonObject.@event.eventtype}");
                        }
                        break;
                    case RequestMsgType.Mixed:
                        var mixedJsonObject = SerializerHelper.GetObject<BotRequestMessageMixed>(json);
                        //目前缺少RequestMessageMixed类，所以暂时不处理
                        //var mixedRequestMessage = new RequestMessageMixed();
                        
                        //为共有字段赋值
                        //SetRequestCommonFields(mixedRequestMessage, baseMessage);
                        
                        // 对特有属性赋值   
                        //requestMessage = mixedRequestMessage;
                        throw new NotSupportedException($"Mixed类型的消息暂未实现，需要先创建对应的RequestMessageMixed类");
                        break;
                    case RequestMsgType.Stream:
                        var streamJsonObject = SerializerHelper.GetObject<BotRequestMessageStream>(json);
                        //目前缺少RequestMessageStream类，所以暂时不处理
                        //var streamRequestMessage = new RequestMessageStream();
                        
                        //为共有字段赋值
                        //SetRequestCommonFields(streamRequestMessage, baseMessage);
                        
                        // 对特有属性赋值   
                        //requestMessage = streamRequestMessage;
                        throw new NotSupportedException($"Stream类型的消息暂未实现，需要先创建对应的RequestMessageStream类");
                        break;
                    default:
                        throw new NotSupportedException($"不支持的消息类型: {msgType}");
                }
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
            var msgType = NeuChar.Helpers.MsgTypeHelper.GetResponseMsgType(baseMessage?.msgtype);
            WorkResponseMessageBase responseMessage = null;

            switch (msgType)
            {
                case ResponseMsgType.Text:
                    var textJsonObject = SerializerHelper.GetObject<BotResponseMessageText>(json);

                    //创建回复消息实体,并设置共有字段
                    var textResponseMessage = SetResponseCommonProperties<ResponseMessageText>(baseMessage);
                    
                    // 对特有属性赋值
                    textResponseMessage.Content = textJsonObject?.text?.content;
                    responseMessage = textResponseMessage;
                    break;
                // 注意：目前没有标准的ResponseMessageStream类，Stream类型暂时不处理
                // 如需要支持Stream类型，需要先创建对应的标准Response类
                case ResponseMsgType.Stream:
                    throw new NotSupportedException($"Stream类型的回复消息暂未实现，需要先创建对应的ResponseMessageStream类");
                //     var streamJsonObject = SerializerHelper.GetObject<BotResponseMessageStream>(json);
                //     // TODO: 创建对应的ResponseMessageStream类
                //     break;
                // TODO: 添加模板卡片类型的处理,但是目前NeuChar标准中没有对应枚举，需要补充
                // case ResponseMsgType.TemplateCard:
                //     var templateCardJsonObject = SerializerHelper.GetObject<BotResponseMessageTemplateCard>(json);
                //     var templateCardResponseMessage = SetResponseCommonProperties<ResponseMessageTemplateCard>(baseMessage);
                //     
                //     // 对特有属性赋值
                //     // TODO: 根据 templateCardJsonObject 设置特有属性
                //     responseMessage = templateCardResponseMessage;
                //     break;
                default:
                    throw new NotSupportedException($"不支持的回复消息类型: {msgType}");
            }

            return responseMessage;
        }


        
        /// <summary>
        /// 获取回复消息的json字符串
        /// </summary>
        /// <param name="responseMessage">标准回复消息实体</param>
        /// <returns>JSON字符串</returns>
        public static string GetResponseMsgString(WorkResponseMessageBase responseMessage)
        {
            if (responseMessage == null)
            {
                throw new ArgumentNullException(nameof(responseMessage), "回复消息不能为空");
            }

            WorkBotResponseMessageBase responseJsonObject = null;

            switch(responseMessage.MsgType)
            {
                case ResponseMsgType.Text:
                    var textResponseMessage = responseMessage as ResponseMessageText;
                    var textJsonObject = new BotResponseMessageText();

                    //创建一个text对象，避免空引用
                    textJsonObject.text = new BotResponseMessageText.TextContent();
                    textJsonObject.text.content = textResponseMessage?.Content;
                    responseJsonObject = textJsonObject;
                    break;
                    
                case ResponseMsgType.Stream:
                    // TODO: 当有对应的ResponseMessageStream类时，在此处理Stream类型
                    // var streamResponseMessage = responseMessage as ResponseMessageStream;
                    // var streamJsonObject = new BotResponseMessageStream();
                    // // 设置stream相关属性
                    // responseJsonObject = streamJsonObject;
                    throw new NotSupportedException($"Stream类型的回复消息暂未实现，需要先创建对应的ResponseMessageStream类");
                    
                // TODO: 添加模板卡片类型的处理,但是目前NeuChar标准中没有对应枚举，需要补充
                //case ResponseMsgType.TemplateCard:
                    //throw new NotSupportedException($"TemplateCard类型的回复消息暂未实现，需要先创建对应的ResponseMessageTemplateCard类");
                //     var templateCardResponseMessage = responseMessage as ResponseMessageTemplateCard;
                //     var templateCardJsonObject = new BotResponseMessageTemplateCard();
                //     // 设置templateCard相关属性
                //     responseJsonObject = templateCardJsonObject;
                //     break;
                    
                default:
                    throw new NotSupportedException($"不支持的回复消息类型: {responseMessage.MsgType}");
            }
            return responseJsonObject.ToJson(true);
        }

    }
}
