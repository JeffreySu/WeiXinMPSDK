using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Helpers.Tests
{
    [TestClass()]
    public class TenPayDateTimeHelperTests
    {
        [TestMethod()]
        public void ToTenPayDateTimeTest()
        {
            var dateTime = new DateTime(2021, 1, 24, 2, 16, 54, 666);
            var dtString = dateTime.ToTenPayDateTime();
            Assert.AreEqual("2021-01-24T02:16:54.666+08:00", dtString);

            dtString = dateTime.ToTenPayDateTime(false);
            Assert.AreEqual("2021-01-24T02:16:54+08:00", dtString);
        }
    }
}