using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Url.ShortUrlJson
{
	/// <summary>
	/// ShortUrl返回结果
	/// </summary>
	public class ShortUrlResult : WxJsonResult
	{
		public string short_url { get; set; }
	}
}
