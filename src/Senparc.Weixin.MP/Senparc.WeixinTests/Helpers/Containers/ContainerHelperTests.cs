#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Containers.Tests;

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
            var result = ContainerHelper.GetItemCacheKey(typeof(TestContainerBag1),shortKey);
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