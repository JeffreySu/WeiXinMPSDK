using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.net6.HttpHandlers
{
    [TestClass]
    public class TenPayApiRequestTests
    {
        [TestMethod]
        public void SetHeaderTest()
        {
            var request = new TenPayApiRequest();
            HttpClient client = new HttpClient();
            request.SetHeader(client);
            Console.WriteLine(client.DefaultRequestHeaders.Accept.ToString());
            Console.WriteLine(client.DefaultRequestHeaders.UserAgent.ToString());
        }
    }
}
