/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：BroswerUtility.cs
    文件功能描述：浏览器公共类


    创建标识：Senparc - 20150419

----------------------------------------------------------------*/

using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.BrowserUtility
{
    public static class BroswerUtility
    {
        /// <summary>
        /// 判断是否在微信内置浏览器中
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool SideInWeixinBroswer(this HttpContext httpContext)
        {
			string ustr = string.Empty;
			var userAgent = httpContext.Request.Headers["User-Agent"];
			if (userAgent.Count > 0)
				ustr = userAgent[0];

			if (string.IsNullOrEmpty(ustr) || (!ustr.Contains("MicroMessenger") && !ustr.Contains("Windows Phone")))
			{
				//在微信外部
				return false;
			}
			//在微信内部
			return true;
        }
    }
}
