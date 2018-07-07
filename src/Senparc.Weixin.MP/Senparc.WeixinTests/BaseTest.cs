#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.AspNetCore.Hosting;
#endif
using Moq;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.WeixinTests
{
    /// <summary>
    /// 单元测试基类
    /// </summary>
    public class BaseTest
    {
        public BaseTest()
        {
            RegisterStart();
        }


        /// <summary>
        /// 运行默认注册流程
        /// </summary>
        protected void RegisterStart()
        {
            //注册
            var senparcSetting = new SenparcSetting() { IsDebug = true };

#if NETCOREAPP2_0 || NETCOREAPP2_1
            var mockEnv = new Mock<IHostingEnvironment>();
            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            RegisterService.Start(mockEnv.Object, senparcSetting);
#else
            RegisterService.Start(senparcSetting);
#endif

            var senparcWeixinSetting = new SenparcWeixinSetting(true);

            Func<IList<IDomainExtensionCacheStrategy>> func = () =>
            {
                var list = new List<IDomainExtensionCacheStrategy>();
                list.Add(LocalContainerCacheStrategy.Instance);
                list.Add(RedisContainerCacheStrategy.Instance);
                //list.Add(MemcachedContainerCacheStrategy.Instance);
                return list;
            };


#if NETCOREAPP2_0 || NETCOREAPP2_1
            RegisterService.Start(null, senparcSetting)
#else
            RegisterService.Start(senparcSetting)
#endif
                .UseSenparcGlobal(false, func).UseSenparcWeixin(senparcWeixinSetting);

        }
    }
}
