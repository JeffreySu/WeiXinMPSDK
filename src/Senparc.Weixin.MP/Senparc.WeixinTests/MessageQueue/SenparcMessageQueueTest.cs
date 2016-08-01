using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.MessageQueue;

namespace Senparc.WeixinTests.MessageQueue
{
    [TestClass]
    public class SenparcMessageQueueTest
    {
        [TestMethod]
        public void TestAll()
        {
            var mq = new SenparcMessageQueue();
            var count = mq.GetCount();
            var key = DateTime.Now.Ticks.ToString();

            //Test Add()
            var item = mq.Add(key, () => WeixinTrace.Log("测试SenparcMessageQueue写入Key=A"));
            Assert.AreEqual(count+1,mq.GetCount());
            //var hashCode = item.GetHashCode();

            //Test GetCurrentKey()
            var currentKey = mq.GetCurrentKey();
            Assert.AreEqual(key,currentKey);

            //Test GetItem
            var currentItem = mq.GetItem(currentKey);
            Assert.AreEqual(currentItem.Key,item.Key);
            Assert.AreEqual(currentItem.AddTime,item.AddTime);

            //Test Remove
            mq.Remove(key);
            Assert.AreEqual(count, mq.GetCount());
        }
    }
}
