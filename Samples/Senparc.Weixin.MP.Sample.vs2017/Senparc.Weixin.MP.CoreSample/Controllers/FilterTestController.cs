/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：FilterTestController.cs
    文件功能描述：演示Senparc.Weixin.MP.MvcExtension.WeixinInternalRequestAttribute
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Senparc.Weixin.MP.MvcExtension;

    /// <summary>
    /// 演示Senparc.Weixin.MP.MvcExtension.WeixinInternalRequestAttribute
    /// </summary>
    public class FilterTestController : Controller
    {
        [WeixinInternalRequest("访问被拒绝，请通过微信客户端访问！","nofilter")]
        public ContentResult Index()
        {
            return Content("访问正常。当前地址：" + Request.PathAndQuery() + "<br />请点击右上角转发按钮，使用【在浏览器中打开】功能进行测试！<br />或者也可以直接在外部浏览器打开 http://sdk.weixin.senparc.com/FilterTest/ 进行测试。");
        }

        [WeixinInternalRequest("Message参数将被忽略", "nofilter", RedirectUrl = "/FilterTest/Index?note=has-been-redirected-url")]
        public ContentResult Redirect()
        {
            return Content("访问正常。当前地址：" + Request.PathAndQuery() + "<br />请点击右上角转发按钮，使用【在浏览器中打开】功能进行测试！<br />或者也可以直接在外部浏览器打开 http://sdk.weixin.senparc.com/FilterTest/Redirect 进行测试。");
        }

        /// <summary>
        /// 直接输出UserAgent
        /// </summary>
        /// <returns></returns>
        public ContentResult UserAgent()
        {
            return Content(Request.UserAgent());
        }
    }
}