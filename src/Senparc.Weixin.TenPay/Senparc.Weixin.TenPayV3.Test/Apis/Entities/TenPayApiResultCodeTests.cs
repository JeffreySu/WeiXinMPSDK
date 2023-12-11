using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Entities
{
    [TestClass]
    public class TenPayApiResultCodeTests
    {
        [TestMethod]
        public void TryGetCodeTest()
        {
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.BadRequest, "{\"code\":\"RESOURCE_ALREADY_EXISTS\",\"message\":\"创建请求重入，但本次请求与上次请求信息不一致\"}");
            Assert.IsNotNull(result);
            Assert.AreEqual("400", result.StateCode);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("RESOURCE_ALREADY_EXISTS", result.ErrorCode);
            Assert.AreEqual("创建请求重入，但本次请求与上次请求信息不一致", result.Solution);
        }

        [TestMethod]
        public void TryGetCode_204Test()
        {
            var result = TenPayApiResultCode.TryGetCode(System.Net.HttpStatusCode.NoContent, null);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.AreEqual("204", result.StateCode);
            Assert.IsTrue(result.Success);
        }
    }
}
