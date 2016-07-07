using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.QY.Exceptions
{
	/// <summary>
	/// 企业号异常
	/// </summary>
	public class WeixinQyException : WeixinException
	{
		public AccessTokenBag AccessTokenBag { get; set; }

		public WeixinQyException(string message, AccessTokenBag accessTokenBag = null, Exception inner = null)
			: base(message, inner)
		{
			AccessTokenBag = accessTokenBag;
		}
	}
}
