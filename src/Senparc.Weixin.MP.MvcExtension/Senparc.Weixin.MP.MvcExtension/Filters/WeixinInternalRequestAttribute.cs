using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Senparc.Weixin.BrowserUtility;

namespace Senparc.Weixin.MP.MvcExtension
{
    /// <summary>
    /// 过滤来自非微信客户端浏览器的请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WeixinInternalRequestAttribute : ActionFilterAttribute
    {
        private string _message;
        private string _ignoreParameter;

        /// <summary>
        /// 重定向地址，如果提供了redirectResult将忽略构造函数中的message
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">错误提示信息</param>
        /// <param name="ignoreParameter">如果地址栏中提供改参数，则忽略浏览器判断，建议设置得复杂一些。如?abc=[任意字符]</param>
        public WeixinInternalRequestAttribute(string message,string ignoreParameter = null)
        {
            _message = message;
            _ignoreParameter = ignoreParameter;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(_ignoreParameter) || string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Request.QueryString[_ignoreParameter]))
            {
                if (!BroswerUtility.SideInWeixinBroswer(filterContext.HttpContext))
                {
                    //TODO:判断网页版登陆状态
                    ActionResult actionResult = null;
                    if (!string.IsNullOrEmpty(RedirectUrl))
                    {
                        actionResult = new RedirectResult(RedirectUrl);
                    }
                    else
                    {
                        actionResult = new ContentResult()
                        {
                            Content = _message
                        };
                    }

                    filterContext.Result = actionResult;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
