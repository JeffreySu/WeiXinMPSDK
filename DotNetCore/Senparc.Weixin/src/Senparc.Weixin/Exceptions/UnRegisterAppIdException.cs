using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Exceptions
{
	public class UnRegisterAppIdException : WeixinException
	{
		public string AppId { get; set; }
		public UnRegisterAppIdException(string appId, string message, Exception inner = null)
			: base(message, inner)
		{
			AppId = appId;
		}
	}
}
