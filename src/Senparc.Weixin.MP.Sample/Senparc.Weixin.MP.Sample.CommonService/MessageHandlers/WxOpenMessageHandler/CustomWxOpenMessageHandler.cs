/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：CustomMessageHandler.cs
    文件功能描述：微信公众号自定义MessageHandler


    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Xml.Linq;
using Senparc.Weixin.WxOpen;
using Senparc.Weixin.WxOpen.MessageHandlers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Entities.Request;
using IRequestMessageBase = Senparc.Weixin.WxOpen.Entities.IRequestMessageBase;
using IResponseMessageBase = Senparc.Weixin.WxOpen.Entities.IResponseMessageBase;

namespace Senparc.Weixin.MP.Sample.CommonService.WxOpenMessageHandler
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomWxOpenMessageHandler : WxOpenMessageHandler<CustomWxOpenMessageContext>
    {
        private string appId = WebConfigurationManager.AppSettings["WxOpenAppId"];
        private string appSecret = WebConfigurationManager.AppSettings["WxOpenAppSecret"];

        public CustomWxOpenMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;

            if (!string.IsNullOrEmpty(postModel.AppId))
            {
                appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            }

            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
        }

        public override XDocument ResponseDocument
        {
            get { return new XDocument(); }//暂时没有需要输出的XML格式内容
        }

        public override XDocument FinalResponseDocument
        {
            get { return new XDocument(); }//暂时没有需要输出的XML格式内容
        }

        public override void OnExecuting()
    {
        //测试MessageContext.StorageData
        if (CurrentMessageContext.StorageData == null)
        {
            CurrentMessageContext.StorageData = 0;
        }
        base.OnExecuting();
    }

    public override void OnExecuted()
    {
        base.OnExecuted();
        CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
    }


    /// <summary>
    /// 处理文字请求
    /// </summary>
    /// <returns></returns>
    public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
    {
        //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs

        //这里可以进行数据库记录或处理

        //发送一条客服消息回复用户

        var result = new StringBuilder();
        result.AppendFormat("您刚才发送了文字信息：{0}\r\n\r\n", requestMessage.Content);

        if (CurrentMessageContext.RequestMessages.Count > 1)
        {
            result.AppendFormat("您刚才还发送了如下消息（{0}/{1}）：\r\n", CurrentMessageContext.RequestMessages.Count,
                CurrentMessageContext.StorageData);
            for (int i = CurrentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
            {
                var historyMessage = CurrentMessageContext.RequestMessages[i];
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

        Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendText(appId, requestMessage.FromUserName, result.ToString());

        return new SuccessResponseMessage();
    }

        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            //发来图片

            return DefaultResponseMessage(requestMessage);
        }

        public override IResponseMessageBase OnEvent_UserEnterTempSessionRequest(RequestMessageEvent_UserEnterTempSession requestMessage)
        {
            //进入客服
            var msg = "欢迎您！这条消息来自Senparc.Weixin事件。";
            Senparc.Weixin.MP.AdvancedAPIs.CustomApi.SendText(appId, requestMessage.FromUserName, msg);

            return DefaultResponseMessage(requestMessage);
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
    {
        //所有没有被处理的消息会默认返回这里的结果

        return new SuccessResponseMessage();

        //return new SuccessResponseMessage();等效于：
        //base.TextResponseMessage = "success";
        //return null;
    }
}
}