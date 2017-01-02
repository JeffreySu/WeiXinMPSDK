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
                throw new ErrorJsonResultException("测试", null, new WxJsonResult());
            }
            catch (ErrorJsonResultException ex)
            {
                Console.WriteLine("ErrorJsonResultException:" + ex.Message);
                throw;
            }
            catch (WeixinMenuException)
            {
                Assert.Fail();
            }
            catch (WeixinException ex)
            {
                Console.WriteLine("WeixinException:" + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("这应该是第3行");
            }
        }
    }
}
