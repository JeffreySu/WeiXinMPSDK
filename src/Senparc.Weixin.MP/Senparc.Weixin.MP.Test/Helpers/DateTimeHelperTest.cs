using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
