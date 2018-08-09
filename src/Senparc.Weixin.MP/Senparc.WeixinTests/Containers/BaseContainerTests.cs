#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Containers.Tests
{
    internal class TestContainerBag1 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime { get; set; }
        //{
        //    get { return _dateTime; }
        //    set { this.SetContainerProperty(ref _dateTime, value); }
        //}
    }

    internal class TestContainerBag2 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime { get; set; }
        //{
        //    get { return _dateTime; }
        //    set { this.SetContainerProperty(ref _dateTime, value); }
        //}
    }

    internal class TestContainer1 : BaseContainer<TestContainerBag1>
    {
    }
    internal class TestContainer2 : BaseContainer<TestContainerBag2>
    {
    }

    [TestClass()]
    public class BaseContainerTests
    {
        [TestMethod()]
        public void GetCollectionListTest()
        {
            //var c1 = TestContainer1.GetCollectionList();
            //var c2 = TestContainer2.GetCollectionList();
            //Assert.IsNotNull(c1);
            //Assert.IsNotNull(c2);
            //var h1 = c1.GetHashCode();
            //var h2 = c2.GetHashCode();

            ////如果为本地缓存策略，通常是一致的，如果是分布式缓存策略，通常不一样
            //Assert.AreEqual(h2, h1);


            //Console.WriteLine("H1:{0}，H2{1}", h1, h2);
        }

        [TestMethod()]
        public void GetItemCacheKeyTest()
        {
            var shortKey = "123abc";
            var itemCacheKey = TestContainer1.GetBagCacheKey(shortKey);
            Assert.AreEqual("Container:Senparc.Weixin.Containers.Tests.TestContainerBag1:"+shortKey, itemCacheKey);
        }
    }
}