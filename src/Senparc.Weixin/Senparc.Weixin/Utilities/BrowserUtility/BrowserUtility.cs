/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：BrowserUtility.cs
    文件功能描述：浏览器公共类


    创建标识：Senparc - 20150419

    修改标识：Senparc - 20161219
    修改描述：v4.9.6 修改错别字：Browser->Browser
----------------------------------------------------------------*/

using System.Web;

namespace Senparc.Weixin.BrowserUtility
{
    public static class BrowserUtility
    {
        /// <summary>
        /// 判断是否在微信内置浏览器中
        /// </summary>
        /// <param name="httpContext">HttpContextBase对象</param>
        /// <returns>true：在微信内置浏览器内。false：不在微信内置浏览器内。</returns>
        public static bool SideInWeixinBrowser(this HttpContextBase httpContext)
        {
            var userAgent = httpContext.Request.UserAgent;
            if (string.IsNullOrEmpty(userAgent) || (!userAgent.Contains("MicroMessenger") && !userAgent.Contains("Windows Phone")))
            {
                //在微信外部
                return false;
            }
            //在微信内部
            return true;
        }
    }
}
