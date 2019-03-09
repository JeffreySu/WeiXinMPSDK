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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Context;

namespace Senparc.Weixin.MP.Test.Context
{
    [TestClass]
    public class MessageContainerTest
    {
        [TestMethod]
        public void AddTest()
        {
            var list = new MessageContainer<int>(-1);

            //测试不限量添加
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }
            Assert.AreEqual(1000, list.Count);

            //限量
            list.MaxRecordCount = 100;//限量100条
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }
            Assert.AreEqual(100, list.Count);
        }

        [TestMethod]
        public void AddRangeTest()
        {
            var list = new MessageContainer<int>(10);//限量10条

            for (int i = 0; i < 1000; i++)
            {
                list.AddRange(new[] { i, i + 1, i + 2 });
            }
            Assert.AreEqual(10, list.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            var list = new MessageContainer<int>(10);//限量10条

            for (int i = 0; i < 1000; i++)
            {
                list.Insert(0, i);
            }
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual(9, list[0]);
        }

        [TestMethod]
        public void InsertRangeTest()
        {
            var list = new MessageContainer<int>(10);//限量10条

            for (int i = 0; i < 1000; i++)
            {
                list.InsertRange(0, new[] { i, i + 1, i + 2 });

                //i=0:0,1,2
                //i=1:1,2,3,0,1,2
                //i=2:2,3,4,1,2,3,0,1,2
                //i=3:3,4,5,2,3,4,1,2,3,0,1,2 -> 5,2,3,4,1,2,3,0,1,2
            }
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
            Assert.AreEqual(4, list[3]);
        }
    }
}
