using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET45
using System.Web;
#else
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
#endif

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass()]
    public class UrlUtilityTests
    {
        [TestMethod()]
        public void GenerateOAuthCallbackUrlTest()
        {
#if NET45
            Func<string, string, string, HttpContextBase> getHttpContextBase = (url, queryString, baseUrl) =>
             {
                 var context = new Mock<HttpContextBase>();
                 var request = new Mock<HttpRequestBase>();
                 var response = new Mock<HttpResponseBase>();

                 request.Setup(r => r.Url).Returns(new Uri(url));
                 request.Setup(r => r.ApplicationPath).Returns(baseUrl);

                 context.Setup(c => c.Request).Returns(request.Object);
                 context.Setup(c => c.Response).Returns(response.Object);
                 context.Setup(c => c.Response).Returns(response.Object);
                 return context.Object;

                 var httpContext = new HttpContext(new HttpRequest("Senparc", url, queryString), new HttpResponse(null));
                 var httpContextBase = new HttpContextWrapper(httpContext);

                 return httpContextBase;
             };
#else
            Func<string, string, string, HttpContext> getHttpContextBase = (url, queryString, baseUrl) =>
             {
                 var uri = new Uri(url);

                 var scheme = uri.Scheme;
                 var path = uri.LocalPath;

                 var httpContext = new DefaultHttpContext();
                 httpContext.Request.Path = path;
                 httpContext.Request.Host = new HostString(uri.Host, uri.Port);
                 httpContext.Request.Scheme = scheme;
                 httpContext.Request.QueryString = new QueryString(uri.Query);
                 httpContext.Request.PathBase = baseUrl;
                 return httpContext;
             };
#endif

            string virtualPath = null;//虚拟路径

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com/Home/Index", "", virtualPath);
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n普通 HTTP Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=http%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex",
    callbackUrl, true);
            }


            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1", virtualPath);
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n带参数 HTTP Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=http%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl, true);
            }


            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com:8080/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1", virtualPath);
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n带参数、带端口 HTTP Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com:8080/TenpayV3/OAuthCallback?returnUrl=http%3a%2f%2fsdk.weixin.senparc.com%3a8080%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl, true);
            }

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com/Home/Index", "", virtualPath);
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n普通 HTTPS Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex",
    callbackUrl, true);
            }

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1", virtualPath);
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n带参数 HTTPS Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl, true);
            }

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com:1433/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1", virtualPath);
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n带参数、带端口 HTTPS Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com:1433/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%3a1433%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl, true);
            }

            //虚拟路径
            {
                virtualPath = "/VirtualPath";
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com:1433/VirtualPath/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1", virtualPath);
                //Console.WriteLine("\r\nhttpContext.Request.AbsoluteUri():"+httpContext.Request.AbsoluteUri());
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("\r\n带参数、带端口 HTTPS Url：" + callbackUrl);

#if NET45
                var expectedUrl = "https://sdk.weixin.senparc.com:1433/VirtualPath/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%3a1433%2fVirtualPath%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1";
#else
                //.NET Standard(.NET Core)下因为模拟会存在偏差（无法自动识别Url中的VirtualPath为特殊的虚拟目录，所以会出现重复），因此以下结果是可以接受的
                var expectedUrl = "https://sdk.weixin.senparc.com:1433/VirtualPath/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%3a1433%2fVirtualPath%2fVirtualPath%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1";
#endif
                Assert.AreEqual(expectedUrl, callbackUrl, true);
            }
        }
    }
}