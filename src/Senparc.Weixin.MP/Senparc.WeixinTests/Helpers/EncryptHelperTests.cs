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
    }
}