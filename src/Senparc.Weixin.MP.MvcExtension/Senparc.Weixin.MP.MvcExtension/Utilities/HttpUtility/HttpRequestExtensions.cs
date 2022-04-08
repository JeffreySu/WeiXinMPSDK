using System;
using System.Collections.Generic;
using System.Text;
#if NET462
using System.Web;
#endif

namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtensions
    {

        public static string GetAbsoluteUri(this HttpRequest request)
        {
#if NET462
            return request.Url.PathAndQuery;
#else
                 return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
#endif
        }
    }
}
