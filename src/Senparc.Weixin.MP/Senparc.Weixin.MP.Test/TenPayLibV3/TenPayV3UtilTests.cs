﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Senparc.Weixin.MP.TenPayLibV3.Tests
{
    [TestClass()]
    public class TenPayV3UtilTests
    {
        [TestMethod()]
        public void BuildRandomStrTest()
        {
            var result = TenPayV3Util.BuildRandomStr(100);
            Assert.AreEqual(100, result.Length);
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void BuildDailyRandomStrTest()
        {
            var result = TenPayV3Util.BuildDailyRandomStr(10);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
    }
}