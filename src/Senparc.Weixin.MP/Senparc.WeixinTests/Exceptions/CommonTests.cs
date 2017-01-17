using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.WeixinTests.Exceptions
{
    [TestClass()]
    public class CommonTests
    {

        [TestMethod]
        public void ExceptionThrowTest()
        {
            try
            {
                try
                {
                    throw new ErrorJsonResultException("测试", null, new WxJsonResult());
                }
                catch (WeixinMenuException)
                {
                    Assert.Fail();
                }
                catch (ErrorJsonResultException ex)
                {
                    ex.AccessTokenOrAppId = "APPID-ErrorJsonResultException";
                    Console.WriteLine("ErrorJsonResultException:" + ex.Message);
                    throw;
                }
                catch (WeixinException ex)
                {
                    ex.AccessTokenOrAppId = "APPID-WeixinException";
                    Console.WriteLine("WeixinException:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.Message);
                }
                finally
                {
                    Console.WriteLine("这应该是第2行");
                }
            }
            catch (WeixinException ex)
            {
                Console.WriteLine("AppID:" + ex.AccessTokenOrAppId);
            }
        }
    }
}
