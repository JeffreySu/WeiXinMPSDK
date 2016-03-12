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

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value, "DateTime"); }
        }
    }

    internal class TestContainerBag2 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value, "DateTime"); }

        }
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
    }
}