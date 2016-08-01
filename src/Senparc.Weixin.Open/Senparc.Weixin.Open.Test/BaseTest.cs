using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.ComponentAPIs;

namespace Senparc.Weixin.Open.Test
{
    public class BaseTest
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

        public BaseTest()
        {
            Func<string, string> getComponentVerifyTicketFunc = s =>
            {
                //do something
                return _ticket;
            };

            //ComponentContainer.Register(_appId, _appSecret, getComponentVerifyTicketFunc);
        }
    }
}
