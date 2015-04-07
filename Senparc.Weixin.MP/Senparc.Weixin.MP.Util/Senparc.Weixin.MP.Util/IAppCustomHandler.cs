using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Util.Content;

namespace Senparc.Weixin.MP.Util
{
    public interface IAppCustomHandler
    {
        IResponseMessageBase SubscribeRequest(AppCtx ctx, RequestMessageEvent_Subscribe requestMessage);

        IResponseMessageBase UnsubscribeRequest(AppCtx ctx,  RequestMessageEvent_Unsubscribe requestMessage);

        IResponseMessageBase ScancodePushRequest(AppCtx ctx,  RequestMessageEvent_Scancode_Push requestMessage);

        IResponseMessageBase ClickEventRequest(AppCtx ctx,  string eventKey);

        IResponseMessageBase TextOrEventRequest(AppCtx ctx,  RequestMessageText requestMessage);

        IResponseMessageBase EnterEventRequest(AppCtx ctx,  RequestMessageEvent_Enter requestMessage);

        /// <summary>
        /// 所有没有被处理的消息会默认返回这里的结果，
        ///  * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
        ///  * 只需要在这里统一发出委托请求，如：
        ///  * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
        /// return responseMessage;
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="handler"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        IResponseMessageBase RequestAgent(AppCtx ctx,  IRequestMessageBase requestMessage);
    }
}
