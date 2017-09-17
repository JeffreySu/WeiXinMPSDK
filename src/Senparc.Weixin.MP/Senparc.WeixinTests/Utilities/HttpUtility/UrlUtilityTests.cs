using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass()]
    public class UrlUtilityTests
    {
        [TestMethod()]
        public void GenerateOAuthCallbackUrlTest()
        {
#if NET45
            Func<string, string, HttpContextBase> getHttpContextBase = (url, queryString) =>
            {
                var httpContext = new HttpContext(new HttpRequest("Senparc", url, queryString), new HttpResponse(null));
                var httpContextBase = new HttpContextWrapper(httpContext);
                return httpContextBase;
            };
#else
            Func<string, string, HttpContext> getHttpContextBase = (url, queryString) =>
            {
                var uri = new Uri(url);

                var urlData = url.Split("://");
                var scheme = urlData[0];
                var indexOfQuestionMark = urlData[1].IndexOf("?");
                var path = urlData[1].Substring(urlData[1].IndexOf('/'),
                    indexOfQuestionMark >= 0
                        ? urlData[1].Length - indexOfQuestionMark
                        : urlData[1].Length - urlData[1].IndexOf('/'));


                var httpContext = new DefaultHttpContext();
                httpContext.Request.Path = path;
                httpContext.Request.Host = new HostString(urlData[1].Split('/')[0]);
                httpContext.Request.Scheme = scheme;
                httpContext.Request.QueryString = new QueryString(url.Contains("?") ? url.Substring(url.IndexOf("?"), url.Length - url.IndexOf("?") - 1) : null);

                return httpContext;
            };
#endif

            {
                var httpContext = getHttpContextBase("http://sdk.weixin.senparc.com/Home/Index", "");
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("普通 HTTP Url：" + callbackUrl);
                Assert.AreEqual(
    "http://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=http%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex",
    callbackUrl);
            }


            {
                var httpContext = getHttpContextBase("http://sdk.weixin.senparc.com/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1");
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("带参数 HTTP Url：" + callbackUrl);
                Assert.AreEqual(
    "http://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=http%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl);
            }


            {
                var httpContext = getHttpContextBase("http://sdk.weixin.senparc.com:8080/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1");
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("带参数、带端口 HTTP Url：" + callbackUrl);
                Assert.AreEqual(
    "http://sdk.weixin.senparc.com:8080/TenpayV3/OAuthCallback?returnUrl=http%3a%2f%2fsdk.weixin.senparc.com%3a8080%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl);
            }

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com/Home/Index", "");
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("普通 HTTPS Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex",
    callbackUrl);
            }

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1");
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("带参数 HTTPS Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl);
            }

            {
                var httpContext = getHttpContextBase("https://sdk.weixin.senparc.com:1433/Home/Index?a=1&b=2&c=3-1", "a=1&b=2&c=3-1");
                var callbackUrl = UrlUtility.GenerateOAuthCallbackUrl(httpContext, "/TenpayV3/OAuthCallback");
                Console.WriteLine("带参数、带端口 HTTPS Url：" + callbackUrl);
                Assert.AreEqual(
    "https://sdk.weixin.senparc.com:1433/TenpayV3/OAuthCallback?returnUrl=https%3a%2f%2fsdk.weixin.senparc.com%3a1433%2fHome%2fIndex%3fa%3d1%26b%3d2%26c%3d3-1",
    callbackUrl);
            }
        }
    }
}