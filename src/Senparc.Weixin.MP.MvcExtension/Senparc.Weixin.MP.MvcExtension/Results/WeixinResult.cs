using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MessageHandlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace Senparc.Weixin.MP.MvcExtension
{

	//public static class WeixinResultExtension
	//{
	//    public WeixinResult WeixinResult()
	//    { 

	//    }
	//}

	/// <summary>
	/// 返回MessageHandler结果
	/// </summary>
	public class WeixinResult : ContentResult
	{
		//private string _content;
		protected IMessageHandlerDocument _messageHandlerDocument;

		public WeixinResult(string content)
		{
			//_content = content;
			base.Content = content;
		}

		public WeixinResult(IMessageHandlerDocument messageHandlerDocument)
		{
			_messageHandlerDocument = messageHandlerDocument;
		}

		/// <summary>
		/// 获取ContentResult中的Content或IMessageHandler中的ResponseDocument文本结果。
		/// 一般在测试的时候使用。
		/// </summary>
		new public string Content
		{
			get
			{
				if (base.Content != null)
				{
					return base.Content;
				}
				else if (_messageHandlerDocument?.FinalResponseDocument != null)
				{
					return _messageHandlerDocument.FinalResponseDocument.ToString();
				}
				else
				{
					return null;
				}
			}
			set { base.Content = value; }
		}

		
        /// <summary>
        /// 执行该Result
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>不需要重写Async方法，CoreMVC 总是优先调用异步方法，并且会自动调用同步方法，因此方法不包含Async awit 所以只重写同步方法</remarks>
		public override void ExecuteResult(ActionContext context)
		{
			if (base.Content == null)
			{
				//使用IMessageHandler输出
				if (_messageHandlerDocument == null)
				{
					throw new Exceptions.WeixinException("执行WeixinResult时提供的MessageHandler不能为Null！", null);
				}

				if (_messageHandlerDocument.FinalResponseDocument == null)
				{
					//throw new Senparc.Weixin.MP.WeixinException("ResponseMessage不能为Null！", null);
				}
				else
				{
					//context.HttpContext.Response.ClearContent();
					context.HttpContext.Response.ContentType = "text/xml";
					_messageHandlerDocument.FinalResponseDocument.Save(context.HttpContext.Response.Body);
				}
			}

			base.ExecuteResult(context);
		}
	}
}
