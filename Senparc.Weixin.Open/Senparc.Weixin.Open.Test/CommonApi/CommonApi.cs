using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Senparc.Weixin.Open.Test.CommonApi
{
    [TestClass]
    public class CommonApi
    {
        private dynamic _appConfig;
        protected dynamic AppConfig
        {
            get
            {
                if (_appConfig == null)
                {
                    if (File.Exists("../../test.config"))
                    {
                        var doc = XDocument.Load("../../test.config");
                        _appConfig = new
                        {
                            AppId = doc.Root.Element("AppId").Value,
                            Secret = doc.Root.Element("Secret").Value,
                            Ticket = doc.Root.Element("Ticket").Value
                        };
                    }
                    else
                    {
                        _appConfig = new
                        {
                            AppId = "YourAppId", //换成你的信息
                            Secret = "YourSecret",//换成你的信息
                            Ticket = "YourTicket"//换成你的信息
                        };
                    }
                }
                return _appConfig;
            }
        }

        protected string _appId
        {
            get { return AppConfig.AppId; }
        }

        protected string _appSecret
        {
            get { return AppConfig.Secret; }
        }

        protected string _ticket
        {
            get { return AppConfig.Ticket; }
        }

        [TestMethod]
        public void GetComponentAccessTokenTest()
        {
            var component_access_tokenResult = Open.ComponentAPIs.ComponentApi.GetComponentAccessToken(_appId, _appSecret, _ticket);
            //运行此测试务必将本地IP加入到白名单，否则可能发生错误：错误代码：61004，说明：access clientip is not registered tips: requestIP: 49.73.28.245
            Assert.IsNotNull(component_access_tokenResult);
        }
    }
}
