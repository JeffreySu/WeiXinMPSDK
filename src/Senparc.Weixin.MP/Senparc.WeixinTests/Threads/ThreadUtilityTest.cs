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
            //默认情况下ThreadUtility应该已经将SenparcMessageQueueThreadUtility加入队列
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

            ////读取队列
            //var mq = new SenparcMessageQueue();
            //var mqKey = SenparcMessageQueue.GenerateKey("A",typeof(TestContainerBag1), bag.Key,"B");
            //var mqItem = mq.GetItem(mqKey);
            //Assert.IsNotNull(mqItem);

        }
    }
}
