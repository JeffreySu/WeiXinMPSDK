using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Containers.Tests;
using Senparc.Weixin.MessageQueue;

namespace Senparc.WeixinTests.Threads
{
    [TestClass]
    public class ThreadUtilityTest
    {
        [TestMethod]
        public void RunTest()
        {
            //默认情况下ThreadUtility应该已经将SenparcMessageQueueThreadUtility加入列队
            //下面对Container进行测试

            //var c1 = TestContainer1.GetCollectionList();
            //var key = DateTime.Now.Ticks.ToString();
            //var bag = new TestContainerBag1()
            //{
            //    Key = key,
            //    DateTime = DateTime.Now
            //};
            //TestContainer1.Update(key, bag);
            //bag.DateTime = DateTime.MinValue;//进行修改

            ////读取列队
            //var mq = new SenparcMessageQueue();
            //var mqKey = SenparcMessageQueue.GenerateKey("A",typeof(TestContainerBag1), bag.Key,"B");
            //var mqItem = mq.GetItem(mqKey);
            //Assert.IsNotNull(mqItem);

        }
    }
}
