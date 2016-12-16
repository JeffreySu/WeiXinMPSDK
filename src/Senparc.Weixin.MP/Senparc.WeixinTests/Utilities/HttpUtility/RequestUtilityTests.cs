using Microsoft.VisualStudio.TestTools.UnitTesting;

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