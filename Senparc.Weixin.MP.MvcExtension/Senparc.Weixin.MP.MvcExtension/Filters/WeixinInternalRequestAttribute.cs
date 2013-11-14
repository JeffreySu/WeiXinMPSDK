using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.MvcExtension
{
    /// <summary>
    /// 过滤来自非微信客户端浏览器的请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WeixinInternalRequestAttribute : ActionFilterAttribute
    {
        private string _message;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">错误提示信息</param>
        public WeixinInternalRequestAttribute(string message)
        {
            _message = message;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userAgent = filterContext.HttpContext.Request.UserAgent;
            if (string.IsNullOrEmpty(userAgent) || (!userAgent.Contains("MicroMessenger") && !userAgent.Contains("Windows Phone")))
            {
                //TODO:判断网页版登陆状态
                filterContext.Result = new ContentResult()
                {
                    Content = _message
                };
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
