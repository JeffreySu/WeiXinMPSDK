using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.MP.Test.vs2017.TenPayLibV3
{
    [TestClass]
    public class RequestHandlerTests
    {


        [TestMethod]
        public void CreateMd5SignTest()
        {
            var str = "appid=wxd930ea5d5a258f4f&body=test&device_info=1000&mch_id=10000100&nonce_str=ibuaiVcKdpRxkhJA";

        }
    }
}
