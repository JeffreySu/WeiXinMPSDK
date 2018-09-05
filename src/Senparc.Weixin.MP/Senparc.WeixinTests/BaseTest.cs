#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //注册开始
            RegisterService register;

            //注册 CON2ET 全局
            var senparcSetting = new SenparcSetting() { IsDebug = true };

#if NETCOREAPP2_0 || NETCOREAPP2_1
            var mockEnv = new Mock<IHostingEnvironment>();
            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            register = RegisterService.Start(mockEnv.Object, senparcSetting);

            RegisterServiceCollection();
#else
            register = RegisterService.Start(senparcSetting);
#endif

            Func<IList<IDomainExtensionCacheStrategy>> func = () =>
            {
                var list = new List<IDomainExtensionCacheStrategy>();
                list.Add(LocalContainerCacheStrategy.Instance);
                list.Add(RedisContainerCacheStrategy.Instance);
                //list.Add(MemcachedContainerCacheStrategy.Instance);
                return list;
            };
            register.UseSenparcGlobal(false, func);

            //注册微信
            var senparcWeixinSetting = new SenparcWeixinSetting(true);
            register.UseSenparcWeixin(senparcWeixinSetting);
        }

#if NETCOREAPP2_0 || NETCOREAPP2_1
        /// <summary>
        /// 注册 IServiceCollection 和 MemoryCache
        /// </summary>
        public static void RegisterServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();
            var config = configBuilder.Build();
            serviceCollection.AddSenparcGlobalServices(config);
            serviceCollection.AddMemoryCache();//使用内存缓存
        }
#endif

        /// <summary>
        /// 获取到达项目根目录的相对路径
        /// </summary>
        /// <returns></returns>
        protected string GetParentRootRelativePath()
        {
#if NETCOREAPP2_0 || NETCOREAPP2_1
            return @"..\..\..\";
#else
            return @"..\..\";
#endif
        }
    }
}
