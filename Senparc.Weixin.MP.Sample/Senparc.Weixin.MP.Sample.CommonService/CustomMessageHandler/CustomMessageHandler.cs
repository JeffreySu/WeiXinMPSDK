using System;
using System.IO;
using System.Text;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.Sample.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public CustomMessageHandler(Stream inputStream)
            : base(inputStream)
        {
            WeixinContext.ExpireMinutes = 3;//用于测试
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs

            //方法一
            //var responseMessage =
            //    ResponseMessageBase.CreateFromRequestMessage(RequestMessage, ResponseMsgType.Text) as
            //    ResponseMessageText;

            //方法二
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);

            var result = new StringBuilder();
            result.AppendFormat("您刚才发送了文字信息：{0}\r\n\r\n", requestMessage.Content);

            if (CurrentMessageContext.RequestMessages.Count > 1)
            {
                result.AppendLine("您刚才还发送了如下消息：");
                for (int i = CurrentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
                {
                    var historyMessage = CurrentMessageContext.RequestMessages[i];
                    result.AppendFormat("【{0}】{1}\r\n - {2}\r\n",
                                        historyMessage.MsgType.ToString(),
                                        (historyMessage is RequestMessageText)
                                            ? (historyMessage as RequestMessageText).Content
                                            : "",
                                        historyMessage.CreateTime.ToShortTimeString()
                        );
                }
                result.AppendLine("\r\n");
            }

            var expireLastTime = CurrentMessageContext.LastActiveTime.AddMinutes(WeixinContext.ExpireMinutes) - DateTime.Now;
            result.AppendFormat("如果您在{0}分钟内连续发送消息，记录将被自动保留。过期后记录将会自动清除。\r\n", (int)expireLastTime.TotalMinutes);
            result.AppendLine("\r\n");
            result.AppendLine("您还可以发送【位置】【图片】【语音】等类型的信息，查看不同格式的回复。\r\nSDK官方地址：http://weixin.senparc.com");

            responseMessage.Content = result.ToString();
            return responseMessage;
        }

        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var locationService = new LocationService();
            var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            return responseMessage;
        }

        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            responseMessage.Content = "这里是正文内容，一共将发2条Article。";
            responseMessage.Articles.Add(new Article()
            {
                Title = "您刚才发送了图片信息",
                Description = "您发送的图片将会显示在边上",
                PicUrl = requestMessage.PicUrl,
                Url = "http://weixin.senparc.com"
            });
            responseMessage.Articles.Add(new Article()
            {
                Title = "第二条",
                Description = "第二条带连接的内容",
                PicUrl = requestMessage.PicUrl,
                Url = "http://weixin.senparc.com"
            });
            return responseMessage;
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
            responseMessage.Music.MusicUrl = "http://weixin.senparc.com/Content/music1.mp3";
            return responseMessage;
        }

        /// <summary>
        /// 处理链接消息请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = string.Format(@"您发送了一条连接信息：
Title：{0}
Description:{1}
Url:{2}", requestMessage.Title, requestMessage.Description, requestMessage.Url);
            return responseMessage;
        }

        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = base.OnEventRequest(requestMessage);//对于Event下属分类的重写方法，见：CustomerMessageHandler_Events.cs
            //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }

    }
}
