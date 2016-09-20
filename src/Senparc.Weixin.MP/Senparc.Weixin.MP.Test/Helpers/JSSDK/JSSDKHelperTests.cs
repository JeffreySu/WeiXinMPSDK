using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Helpers.Tests
{
    [TestClass()]
    public class JSSDKHelperTests
    {
        [TestMethod()]
        public void GetSignatureTest()
        {
            var result = JSSDKHelper.GetSignature("ticket", "noncestr", "timestamp", "url");
            Assert.IsNotNull(result);
            Assert.AreEqual("59aeda0e6c88f28a661c4cacc5250fc494757c07",result);
            Console.WriteLine(result);
        }
    }
}