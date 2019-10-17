/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageBase.cs
    文件功能描述：响应回复消息基类
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20150505
    修改描述：添加ResponseMessageNoResponse类型处理
----------------------------------------------------------------*/

using System;
using System.Xml.Linq;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.Helpers;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
	public interface IWorkResponseMessageBase : Senparc.NeuChar.Entities.IResponseMessageBase, IMessageBase
	{
		ResponseMsgType MsgType { get; }
		//string Content { get; set; }
		//bool FuncFlag { get; set; }
	}

	/// <summary>
	/// 响应回复消息
	/// </summary>
	public class WorkResponseMessageBase : ResponseMessageBase, IWorkResponseMessageBase
	{
		public virtual ResponseMsgType MsgType
		{
			get { return ResponseMsgType.Text; }
		}
		//public string Content { get; set; }
		//public bool FuncFlag { get; set; }

		/// <summary>
		/// 获取响应类型实例，并初始化
		/// </summary>
		/// <param name="requestMessage">请求</param>
		/// <param name="msgType">响应类型</param>
		/// <returns></returns>
		[Obsolete("建议使用CreateFromRequestMessage<T>(IRequestMessageBase requestMessage)取代此方法")]
		private static WorkResponseMessageBase CreateFromRequestMessage(IWorkRequestMessageBase requestMessage, ResponseMsgType msgType)
		{
            //注意：SDK 内部此方法仍然是重要的最终执行的方法，只是提供了简化的重写方法，不建议外部直接调用。

			WorkResponseMessageBase responseMessage = null;
			try
			{
				switch (msgType)
				{
					case ResponseMsgType.Text:
						responseMessage = new ResponseMessageText();
						break;
					case ResponseMsgType.News:
						responseMessage = new ResponseMessageNews();
						break;
					case ResponseMsgType.Image:
						responseMessage = new ResponseMessageImage();
						break;
					case ResponseMsgType.Voice:
						responseMessage = new ResponseMessageVoice();
						break;
					case ResponseMsgType.Video:
						responseMessage = new ResponseMessageVideo();
						break;
					case ResponseMsgType.MpNews:
						responseMessage = new ResponseMessageMpNews();
						break;
                    case ResponseMsgType.NoResponse:
                        responseMessage = new WorkResponseMessageNoResponse();
				        break;
					default:
						throw new UnknownRequestMsgTypeException(string.Format("ResponseMsgType没有为 {0} 提供对应处理程序。", msgType), new ArgumentOutOfRangeException());
				}

				responseMessage.ToUserName = requestMessage.FromUserName;
				responseMessage.FromUserName = requestMessage.ToUserName;
				responseMessage.CreateTime = SystemTime.Now; //使用当前最新时间

			}
			catch (Exception ex)
			{
				throw new WeixinException("CreateFromRequestMessage过程发生异常", ex);
			}

			return responseMessage;
		}

		/// <summary>
		/// 获取响应类型实例，并初始化
		/// </summary>
		/// <typeparam name="T">需要返回的类型</typeparam>
		/// <param name="requestMessage">请求数据</param>
		/// <returns></returns>
		public static T CreateFromRequestMessage<T>(IWorkRequestMessageBase requestMessage) where T : WorkResponseMessageBase
		{
			try
			{
				var tType = typeof(T);
				var responseName = tType.Name.Replace("ResponseMessage", ""); //请求名称
				ResponseMsgType msgType = (ResponseMsgType)Enum.Parse(typeof(ResponseMsgType), responseName);
				return CreateFromRequestMessage(requestMessage, msgType) as T;
			}
			catch (Exception ex)
			{
				throw new WeixinException("ResponseMessageBase.CreateFromRequestMessage<T>过程发生异常！", ex);
			}
		}

		/// <summary>
		/// 从返回结果XML转换成IResponseMessageBase实体类
		/// </summary>
		/// <param name="xml">返回给服务器的Response Xml</param>
		/// <returns></returns>
		public static IWorkResponseMessageBase CreateFromResponseXml(string xml)
		{
			try
			{
				if (string.IsNullOrEmpty(xml))
				{
					return null;
				}

				var doc = XDocument.Parse(xml);
				WorkResponseMessageBase responseMessage = null;
				var msgType = (ResponseMsgType)Enum.Parse(typeof(ResponseMsgType), doc.Root.Element("MsgType").Value, true);
				switch (msgType)
				{
					case ResponseMsgType.Text:
						responseMessage = new ResponseMessageText();
						break;
					case ResponseMsgType.Image:
						responseMessage = new ResponseMessageImage();
						break;
					case ResponseMsgType.Voice:
						responseMessage = new ResponseMessageVoice();
						break;
					case ResponseMsgType.Video:
						responseMessage = new ResponseMessageVideo();
						break;
					case ResponseMsgType.News:
						responseMessage = new ResponseMessageNews();
						break;
                    case ResponseMsgType.MpNews:
                        responseMessage = new ResponseMessageMpNews();
						break;
                    case ResponseMsgType.NoResponse:
                        responseMessage = new WorkResponseMessageNoResponse();
                        break;
                }

				responseMessage.FillEntityWithXml(doc);
				return responseMessage;
			}
			catch (Exception ex)
			{
				throw new WeixinException("ResponseMessageBase.CreateFromResponseXml<T>过程发生异常！" + ex.Message, ex);
			}
		}
	}
}
