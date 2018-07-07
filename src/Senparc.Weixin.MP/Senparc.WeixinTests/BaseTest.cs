using Microsoft.AspNetCore.Hosting;
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
            var mockEnv = new Mock<IHostingEnvironment>();
            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            RegisterService.Start(mockEnv.Object, new SenparcSetting() { IsDebug = true });

            var senparcSetting = new CO2NET.SenparcSetting() { IsDebug = true };
            var senparcWeixinSetting = new SenparcWeixinSetting(true);

            Func<IList<IDomainExtensionCacheStrategy>> func = () =>
            {
                var list = new List<IDomainExtensionCacheStrategy>();
                list.Add(LocalContainerCacheStrategy.Instance);
                list.Add(RedisContainerCacheStrategy.Instance);
                //list.Add(MemcachedContainerCacheStrategy.Instance);
                return list;
            };

            RegisterService.Start(null, senparcSetting)
                .UseSenparcGlobal(false, func).UseSenparcWeixin(senparcWeixinSetting);

            //设置本目录路径

        }
    }
}
