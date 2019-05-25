/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：SenparcOAuthAttribute.cs
    文件功能描述：自动判断OAuth授权状态


    创建标识：Senparc - 20170509
    
    修改标识：Senparc - 20170509
    修改描述：v4.5.0 添加SenparcOAuthAttribute，自动进行OAuth授权
    
    修改标识：Senparc - 20181226
    修改描述：v7.2.2 修改 DateTime 为 DateTimeOffset

    修改标识：Senparc - 20181226
    修改描述：v7.2.8 升级 OAuth 重定向功能，改为永久重定向（301)


----------------------------------------------------------------*/

using System;
using System.Diagnostics.CodeAnalysis;
using Senparc.Weixin.MP.AdvancedAPIs;
#if NET45
using System.Web.Mvc;
using System.Web;
#else
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

namespace Senparc.Weixin.MP.MvcExtension
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
            Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public abstract class SenparcOAuthAttribute :
#if NET45
        FilterAttribute,/* AuthorizeAttribute,*/ IAuthorizationFilter
#else
        ActionFilterAttribute,/* AuthorizeAttribute,*/ IAuthorizationFilter
#endif
    {
        protected string _appId { get; set; }
        protected string _oauthCallbackUrl { get; set; }
        protected OAuthScope _oauthScope { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="oauthCallbackUrl">网站内路径（如：/TenpayV3/OAuthCallback），以/开头！当前页面地址会加在Url中的returlUrl=xx参数中</param>
        public SenparcOAuthAttribute(string appId, string oauthCallbackUrl, OAuthScope oauthScope = OAuthScope.snsapi_userinfo)
        {
            _appId = appId;
            _oauthCallbackUrl = oauthCallbackUrl;
            _oauthScope = oauthScope;
        }


        /// <summary>
        /// 判断用户是否已经登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
#if NET45
        public abstract bool IsLogined(HttpContextBase httpContext);
#else
        public abstract bool IsLogined(HttpContext httpContext);
#endif

#if NET45
        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
#else
        protected virtual bool AuthorizeCore(HttpContext httpContext)
#endif

        {
            //return true;
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!IsLogined(httpContext))
            {
                return false;//未登录
            }

            return true;
        }


#if NET45
        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
#endif

#if NET45

#else

#endif


#if NET45
        public virtual void OnAuthorization(AuthorizationContext filterContext)
#else
        public virtual void OnAuthorization(AuthorizationFilterContext filterContext)
#endif
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

#if NET45
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
#endif
            }
            else
            {
                if (IsLogined(filterContext.HttpContext))
                {
                    //已经登录
                }
                else
                {
                    var callbackUrl = Senparc.Weixin.HttpUtility.UrlUtility.GenerateOAuthCallbackUrl(filterContext.HttpContext, _oauthCallbackUrl);
                    var state = string.Format("{0}|{1}", "FromSenparc", SystemTime.Now.Ticks);
                    var url = OAuthApi.GetAuthorizeUrl(_appId, callbackUrl, state, _oauthScope);
                    filterContext.Result = new RedirectResult(url/*, true*/);
                }
            }
        }

#if NET45
        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }
#endif
    }
}
