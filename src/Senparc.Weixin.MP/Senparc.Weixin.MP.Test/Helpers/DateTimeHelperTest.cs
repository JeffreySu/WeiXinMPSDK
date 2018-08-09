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
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Test
{
    [TestClass]
    public class DateTimeHelperTest
    {
        long timeStamp = 1358061152;
        DateTime expect = new DateTime(2013, 1, 13, 15, 12, 32);

        /// <summary>
        /// 测试Unix时间（基本方法）
        /// </summary>
        [TestMethod]
        public void UnixTimeTest()
        {
            var result = DateTimeHelper.BaseTime.AddTicks((timeStamp + 8 * 60 * 60) * 10000000);
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void GetDateTimeFromXmlTest()
        {
            var result = DateTimeHelper.GetDateTimeFromXml(timeStamp);
            Assert.AreEqual(expect, result);


            var result2 = DateTimeHelper.GetDateTimeFromXml(1432094942);
            Console.WriteLine(result2);
        }

         [TestMethod]
        public void GetWeixinDateTimeTest()
        {
            var result = DateTimeHelper.GetWeixinDateTime(expect);
            Assert.AreEqual(timeStamp, result);
        }
    }
}
