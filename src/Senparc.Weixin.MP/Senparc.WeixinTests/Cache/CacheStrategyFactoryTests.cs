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
            Console.WriteLine("不注册");

            // {
            //    //不注册，使用默认
            //    var c1 = TestContainer1.GetCollectionList();
            //    Console.WriteLine(c1.Count);//0
            //    var c1Strategy = CacheStrategyFactory.GetContainerCacheStragegyInstance();
            //    Assert.IsNotNull(c1Strategy);

            //    var key = typeof(TestContainer1).ToString();
            //    var data = c1Strategy.Get(key);
            //    Assert.IsNotNull(data);

            //    var newData = new ContainerItemCollection();
            //    newData["A"] = new TestContainerBag1();
            //    c1Strategy.InsertToCache(key, newData);
            //    data = c1Strategy.Get(key);
            //    Assert.AreEqual(1, data.GetCount());
            //    Console.WriteLine(data.GetCount());//1

            //    var collectionList = TestContainer1.GetCollectionList()[key];
            //    collectionList.InsertToCache("ABC", new TestContainerBag1());
            //    data = c1Strategy.Get(key);
            //    Assert.AreEqual(2, data.GetCount());
            //    Console.WriteLine(data.GetCount());//2
            //}
            //Console.WriteLine("使用注册");
            //{
            //    //进行注册
            //    CacheStrategyFactory.RegisterContainerCacheStrategy(() =>
            //    {
            //        return LocalContainerCacheStrategy.Instance as IContainerCacheStragegy;
            //    });

            //    var key = typeof(TestContainer2).ToString();

            //    var c2 = TestContainer2.GetCollectionList();
            //    Console.WriteLine(c2.Count);//1（位注册的时候已经注册过一个TestContainer1）
            //    var c2Strategy = CacheStrategyFactory.GetContainerCacheStragegyInstance();
            //    Assert.IsNotNull(c2Strategy);


            //    var data = c2Strategy.Get(key);
            //    Assert.IsNotNull(data);

            //    var newData = new ContainerItemCollection();
            //    newData["A"] = new TestContainerBag1();
            //    c2Strategy.InsertToCache(key, newData);
            //    data = c2Strategy.Get(key);
            //    Assert.AreEqual(1, data.GetCount());
            //    Console.WriteLine(data.GetCount());//1

            //    var collectionList = TestContainer2.GetCollectionList()[typeof(TestContainer2).ToString()];
            //    collectionList.InsertToCache("DEF", new TestContainerBag2());
            //    data = c2Strategy.Get(key);
            //    Assert.AreEqual(2, data.GetCount());
            //    Console.WriteLine(data.GetCount());//1
            //}
        }
    }
}