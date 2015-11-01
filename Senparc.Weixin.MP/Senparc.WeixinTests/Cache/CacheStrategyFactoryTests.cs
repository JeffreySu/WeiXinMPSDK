using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers.Tests;

namespace Senparc.Weixin.Cache.Tests
{
    [TestClass()]
    public class CacheStrategyFactoryTests
    {
        [TestMethod()]
        public void RegisterContainerCacheStrategyTest()
        {
            {
                //不注册，使用默认
                var c1 = TestContainer1.GetCollectionList();
                var c1Strategy = CacheStrategyFactory.GetContainerCacheStragegyInstance<TestContainerBag1>();

            }

            {
                //进行注册
            }
        }
    }
}