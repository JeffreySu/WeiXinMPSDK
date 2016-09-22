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
            var result = JSSDKHelper.GetSignature("kgt8ON7yVITDhtdwci0qefK1QvDlwsAPwMnZOO_J0MxaUpuHtIU_IltC7zs3kfNOYTHEqeIEvEXZHbS3xXNx3g", "B7EE6F5F9AA5CD17CA1AEA43CE848496", "1474350784", "https://www.baidu.com");
            Assert.IsNotNull(result);
            Assert.AreEqual("3b1b4171bcfa0f0661be9c5474002d3eb25a3368", result);
            Console.WriteLine(result);
        }
    }
}