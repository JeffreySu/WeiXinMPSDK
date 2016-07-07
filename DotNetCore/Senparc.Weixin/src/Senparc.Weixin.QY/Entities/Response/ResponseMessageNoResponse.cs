using Senparc.Weixin.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.QY.Entities.Response
{
	/// <summary>
	/// 当MessageHandler接收到IResponseNothing的返回类型参数时，只会向微信服务器返回空字符串，等同于return null  
	/// </summary>
	public class ResponseMessageNoResponse : ResponseMessageBase, IResponseMessageNoResponse
	{
	}
}
