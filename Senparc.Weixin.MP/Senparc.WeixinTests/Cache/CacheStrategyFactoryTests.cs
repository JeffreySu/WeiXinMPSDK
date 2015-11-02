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
                Console.WriteLine(c1.Count);
                var c1Strategy = CacheStrategyFactory.GetContainerCacheStragegyInstance();
                Assert.IsNotNull(c1Strategy);


                var data = c1Strategy.Get(typeof(TestContainer1).ToString());
                Console.WriteLine(data.Count);

                var collectionList = TestContainer1.GetCollectionList()[typeof (TestContainer1).ToString()];
                collectionList.Add("ABC",new TestContainerBag1());
                data = c1Strategy.Get(typeof(TestContainer1).ToString());
                Assert.AreEqual(1,data.Count);
                Console.WriteLine(data.Count);

            }

            {
                //进行注册
            }
        }
    }
}