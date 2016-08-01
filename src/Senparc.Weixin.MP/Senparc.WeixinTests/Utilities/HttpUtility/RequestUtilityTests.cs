using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass()]
    public class RequestUtilityTests
    {
        [TestMethod()]
        public void SetHttpProxyTest()
        {
            //设置
            RequestUtility.SetHttpProxy("http://192.168.1.130", "8088", "username", "pwd");

            //清除
            RequestUtility.RemoveHttpProxy();
        }
    }
}