using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if NET45
System.Web
#else
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.Weixin.MP.CoreSample.CommonService.Utilities
{
    public static class Server
    {
        private static string _appDomainAppPath;
        public static string AppDomainAppPath
        {
            get
            {
                if (_appDomainAppPath == null)
                {
#if NET45
                    _appDomainAppPath = HttpRuntime.AppDomainAppPath;
#else
                    _appDomainAppPath = AppContext.BaseDirectory; //dll所在目录：;
#endif
                }
                return _appDomainAppPath;
            }
            set
            {
                _appDomainAppPath = value;
#if NETSTANDARD1_6
                if (!_appDomainAppPath.EndsWith("\\"))
                {
                    _appDomainAppPath += "\\";
                }
#endif
            }
        }

        public static string GetMapPath(string virtualPath)
        {
            if (virtualPath == null)
            {
                return "";
            }
            else if (virtualPath.StartsWith("~/"))
            {
                return virtualPath.Replace("~/", AppDomainAppPath).Replace("/", "\\");
            }
            else
            {
                return Path.Combine(AppDomainAppPath, virtualPath.Replace("/", "\\"));
            }
        }

        public static HttpContext HttpContext
        {
            get
            {
#if NET45
                HttpContext context = HttpContext.Current;
                 if (context == null)
                {
                    HttpRequest request = new HttpRequest("Default.aspx", "http://sdk.weixin.senparc.com/default.aspx", null);
                    StringWriter sw = new StringWriter();
                    HttpResponse response = new HttpResponse(sw);
                    context = new HttpContext(request, response);
                }
#else
                HttpContext context = new DefaultHttpContext();
#endif
                return context;
            }
        }
    }
}
