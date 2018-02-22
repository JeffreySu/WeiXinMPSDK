#if NET45 || NET461

#else
using Enyim.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class MemcachedApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSenparcMemcached(this IApplicationBuilder app)
        {
            app.UseEnyimMemcached();
            return app;
        }
    }
}
#endif