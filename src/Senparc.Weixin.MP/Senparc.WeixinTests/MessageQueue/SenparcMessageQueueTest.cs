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
