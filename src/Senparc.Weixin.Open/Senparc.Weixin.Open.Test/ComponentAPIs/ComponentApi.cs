using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Senparc.Weixin.Open.Test.CommonApi
{
    [TestClass]
    public class ComponentApi : CommonApi
    {
        [TestMethod]
        public void GetAuthorizerInfoTest()
        {
            var component_access_tokenResult = Open.ComponentAPIs.ComponentApi.GetComponentAccessToken(_appId, _appSecret, _ticket);

            var result = Open.ComponentAPIs.ComponentApi.GetAuthorizerInfo(component_access_tokenResult.component_access_token,
                _appId, "wx7cfd56c9f047bf51");
        }
    }
}
