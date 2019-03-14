#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

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
