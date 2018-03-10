#if NETCOREAPP2_1
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.HttpUtility
{
#if NETCOREAPP2_1
    public static class SenparcHttpClientExtensions
    {
        public static IApplicationBuilder UseStaticSenparcHttpClient(this IApplicationBuilder app)
        {
            var senparcHttpClient = app.ApplicationServices.GetRequiredService<SenparcHttpClient>();
            RequestUtility.Configure(senparcHttpClient);
            return app;
        }
    }
#endif
}
