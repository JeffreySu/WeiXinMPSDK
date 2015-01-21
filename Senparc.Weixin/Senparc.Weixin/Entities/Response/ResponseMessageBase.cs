namespace Senparc.Weixin.Entities
{
	public interface IResponseMessageBase : IMessageBase
	{
		//ResponseMsgType MsgType { get; }
		//string Content { get; set; }
		//bool FuncFlag { get; set; }
	}

	/// <summary>
	/// 响应回复消息
	/// </summary>
	public abstract class ResponseMessageBase : MessageBase, IResponseMessageBase
	{
        //public virtual ResponseMsgType MsgType
        //{
        //    get { return ResponseMsgType.Text; }
        //}
	}
}
