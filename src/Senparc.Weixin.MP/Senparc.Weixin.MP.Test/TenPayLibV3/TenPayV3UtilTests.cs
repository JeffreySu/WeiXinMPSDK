using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.TenPayLibV3.Tests
{
    [TestClass()]
    public class TenPayV3UtilTests
    {
        [TestMethod()]
        public void BuildRandomStrTest()
        {
            var result = TenPayV3Util.BuildRandomStr(100);
            Assert.AreEqual(100,result.Length);
            Console.WriteLine(result);
        }
    }
}