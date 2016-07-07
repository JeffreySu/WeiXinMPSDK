/*----------------------------------------------------------------
	Copyright (C) 2015 LSW
	
	文件名：WeixinException.cs
	文件功能描述：微信自定义异常基类
	
	
	创建标识：LSW - 20150211
	
	修改标识：LSW - 20150303
	修改描述：整理接口
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
	/// <summary>
	/// 微信自定义异常基类
	/// </summary>
	public class WeixinException
#if NET451
		: ApplicationException
#else
		: Exception
#endif
	{
		public WeixinException(string message)
			: base(message, null)
		{
		}

		public WeixinException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
