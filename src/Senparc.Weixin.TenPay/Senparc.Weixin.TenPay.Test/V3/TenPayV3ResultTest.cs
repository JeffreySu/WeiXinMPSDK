using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPay.V3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPay.Test.vs2017.V3
{
    [TestClass]
    public class TenPayV3ResultTest
    {
        [TestMethod]
        public void GetXmlValuesTest()
        {
            var xml = @"<xml><a1>1</a1><a2>2</a2>
<b1>Senparc</b1><b2>Good</b2>
<return_code>SUCCESS</return_code><return_msg>SUCCESS</return_msg></xml>";
            var tenPayV3Result = new TenPayV3Result(xml);
            var result = tenPayV3Result.GetXmlValues<int>("a");

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            var resultB = tenPayV3Result.GetXmlValues<string>("b");
            foreach (var item in resultB)
            {
                Console.WriteLine(item);
            }
        }
    }
}
