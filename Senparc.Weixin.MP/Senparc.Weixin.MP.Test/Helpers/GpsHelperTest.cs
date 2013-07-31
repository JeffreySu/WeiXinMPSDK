using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Test.Helpers
{
    [TestClass]
    public class GpsHelperTest
    {
        [TestMethod]
        public void DistanceTest()
        {
            var result = GpsHelper.Distance(31.3131, 120.5815, 31.2751, 120.6497);
            Assert.IsTrue(result>0);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetLatitudeDifferenceTest()
        {
            var result = GpsHelper.GetLatitudeDifference(10);
            Assert.IsTrue(result>0);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetLongitudeDifferenceTest()
        {
            var result = GpsHelper.GetLongitudeDifference(10);
            Assert.IsTrue(result > 0);
            Console.WriteLine(result);
        }
    }
}
