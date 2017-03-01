using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.MP.Sample.CommonService.Utilities
{
    public static class Server
    {
        //可以使用IHostingEnvironment hostingEnvironment依赖注入获取_hostingEnvironment.WebRootPath及
        //_hostingEnvironment.ContentRootPath

        //private static string _appDomainAppPath;
        //public static string AppDomainAppPath
        //{
        //    get
        //    {
        //        if (_appDomainAppPath == null)
        //        {
        //            _appDomainAppPath =   HttpRuntime.AppDomainAppPath;
        //        }
        //        return _appDomainAppPath;
        //    }
        //    set
        //    {
        //        _appDomainAppPath = value;
        //    }
        //}

        //public static string GetMapPath(string virtualPath)
        //{
        //    if (virtualPath == null)
        //    {
        //        return "";
        //    }
        //    else if (virtualPath.StartsWith("~/"))
        //    {
        //        return virtualPath.Replace("~/", AppDomainAppPath).Replace("/", "\\");
        //    }
        //    else
        //    {
        //        return Path.Combine(AppDomainAppPath, virtualPath.Replace("/", "\\"));
        //    }
        //}

        public static IServiceProvider ServiceProvider;

        public static Microsoft.AspNetCore.Http.HttpContext HttpContext
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                return context;


                //HttpContext context = ;
                //if (context == null)
                //{
                //    HttpRequest request = new HttpRequest("Default.aspx", "http://sdk.weixin.senparc.com/default.aspx", null);
                //    StringWriter sw = new StringWriter();
                //    HttpResponse response = new HttpResponse(sw);
                //    context = new HttpContext(request, response);
                //}
                //return context;
            }
        }
    }
}
