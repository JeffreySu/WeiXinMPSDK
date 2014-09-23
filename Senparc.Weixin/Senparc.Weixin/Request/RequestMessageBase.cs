namespace Senparc.Weixin.Entities
{
    public interface IRequestMessageBase : IMessageBase
    {
        RequestMsgType MsgType { get; }
        long MsgId { get; set; }
    }

    /// <summary>
    /// 接收到请求的消息
    /// </summary>
    public abstract class RequestMessageBase : MessageBase, IRequestMessageBase
    {
        public RequestMessageBase()
        {

        }

        public virtual RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }

        public long MsgId { get; set; }
    }
}
