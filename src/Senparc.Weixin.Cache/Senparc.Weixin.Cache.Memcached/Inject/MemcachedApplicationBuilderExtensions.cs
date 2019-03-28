/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：MemcachedApplicationBuilderExtensions.cs
    文件功能描述：Memcached 依赖注入设置。


    创建标识：Senparc - 20180222

----------------------------------------------------------------*/

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