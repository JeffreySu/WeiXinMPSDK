#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Senparc.Weixin.MP.Sample.Tests.Mock
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext(Stream inputStream = null,string userAgent=null)
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            // var requestInputStream = new Mock<Stream>();

            //* 如果出现错误：System.ArgumentException: Unable to obtain public key for StrongNameKeyPair
            //* 是因为无法对MachineKeys目录进行写和删除操作，
            //* 需要给这个文件夹重设权限（Administrators has full control, everyone has had read/write access, but not delete access.）：
            //* C:\Documents and Settings\All Users\Application Data\Microsoft\Crypto\RSA
            //* 参考：http://ayende.com/blog/1441/unable-to-obtain-public-key-for-strongnamekeypair
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            request.Setup(r => r.InputStream).Returns(inputStream);
            request.Setup(r => r.UserAgent).Returns(userAgent);
            request.Setup(r => r.Url).Returns(new Uri("https://sdk.weixin.senparc.com"));

            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url, Stream inputStream = null, string userAgent = null)
        {
            HttpContextBase context = FakeHttpContext(inputStream,userAgent);
            context.Request.SetupRequestUrl(url);
            return context;
        }

        public static void SetFakeControllerContext(this Controller controller, Stream inputStream = null, string userAgent = null)
        {
            var httpContext = FakeHttpContext(inputStream,userAgent);
            ControllerContext context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }

        static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0, url.IndexOf("?"));
            else
                return url;
        }

        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                NameValueCollection parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock<HttpRequestBase>.Get(request)
                .Setup(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (!url.StartsWith("~/"))
                throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

            var mock = Mock<HttpRequestBase>.Get(request);

            mock.Setup(req => req.QueryString)
                .Returns(GetQueryStringParameters(url));
            mock.Setup(req => req.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            mock.Setup(req => req.PathInfo)
                .Returns(string.Empty);
        }

        public static IValueProvider SetupFormValueProvider(Dictionary<string, string> formValues)
        {
            //List<IValueProvider> valueProviders = new List<IValueProvider>();

            FormCollection form = new FormCollection();
            if (formValues != null)
            {
                foreach (string key in formValues.Keys)
                {
                    form.Add(key, formValues[key]);
                }
            }
            return form.ToValueProvider();

            //valueProviders.Add(form);
            //return new ValueProviderCollection(valueProviders);
        }

        public static IValueProvider SetupFormValueProvider<T>(T obj, string prefix = null) where T : class
        {
            Dictionary<string, string> formValues = new Dictionary<string, string>();
            string valuePrefix = prefix == null ? null : prefix + ".";
            try
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    var value = prop.GetValue(obj, null);
                    formValues[valuePrefix + prop.Name] = value == null ? "" : value.ToString();
                }
            }
            catch { }
            return SetupFormValueProvider(formValues);
        }
    }
}
