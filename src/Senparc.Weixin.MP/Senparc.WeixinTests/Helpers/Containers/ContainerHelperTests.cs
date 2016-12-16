using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Containers.Tests;
using System;

namespace Senparc.Weixin.Helpers.Tests
{
    [TestClass()]
    public class ContainerHelperTests
    {
        [TestMethod()]
        public void GetCacheKeyNamespaceTest()
        {
            var obj = new TestContainerBag1() as IBaseContainerBag;//使用接口或基类
            var result = ContainerHelper.GetCacheKeyNamespace(obj.GetType());
            Console.WriteLine(result);
            Assert.AreEqual("Container:Senparc.Weixin.Containers.Tests.TestContainerBag1", result);
        }

        [TestMethod()]
        public void GetItemCacheKeyTest()
        {
            //var obj = new TestContainerBag1() as IBaseContainerBag;//使用接口或基类
            var shortKey = "abc123";
            var result = ContainerHelper.GetItemCacheKey(typeof(TestContainerBag1), shortKey);
            Assert.AreEqual("Container:Senparc.Weixin.Containers.Tests.TestContainerBag1:" + shortKey, result);
        }

        [TestMethod()]
        public void GetItemCacheKeyTest1()
        {
            var shortKey = "abc123";
            var obj = new TestContainerBag1()
            {
                Key = shortKey
            } as IBaseContainerBag;//使用接口或基类
            var result = ContainerHelper.GetItemCacheKey(obj);
            Assert.AreEqual("Container:Senparc.Weixin.Containers.Tests.TestContainerBag1:" + shortKey, result);
        }

        [TestMethod()]
        public void GetItemCacheKeyTest2()
        {
            var shortKey = "456BCD";
            var obj = new TestContainerBag1()
            {
                Key = "key"
            } as IBaseContainerBag;//使用接口或基类
            var result = ContainerHelper.GetItemCacheKey(obj, shortKey);
            Assert.AreEqual("Container:Senparc.Weixin.Containers.Tests.TestContainerBag1:" + shortKey, result);
        }
    }
}