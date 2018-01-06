using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Helpers.Tests
{
    [TestClass()]
    public class EncryptHelperTests
    {
        string encypStr = "Senparc";
        string exceptMD5Result = "8C715F421744218AB5B9C8E8D7E64AC6";

        [TestMethod()]
        public void GetMD5Test()
        {
            //常规方法
            var result = EncryptHelper.GetMD5(encypStr, Encoding.UTF8);
            Assert.AreEqual(exceptMD5Result, result);

            //重写方法
            result = EncryptHelper.GetMD5(encypStr);
            Assert.AreEqual(exceptMD5Result, result);

            //小写
            result = EncryptHelper.GetLowerMD5(encypStr, Encoding.UTF8);
            Assert.AreEqual(exceptMD5Result.ToLower(), result);
        }

        [TestMethod]
        public void GetHmacSha256Test()
        {
            var msg = "{\"foo\":\"bar\"}";
            var sessionKey = "o0q0otL8aEzpcZL/FT9WsQ==";
            var result = EncryptHelper.GetHmacSha256(msg, sessionKey);
            Assert.AreEqual("654571f79995b2ce1e149e53c0a33dc39c0a74090db514261454e8dbe432aa0b",result);
        }
    }
}