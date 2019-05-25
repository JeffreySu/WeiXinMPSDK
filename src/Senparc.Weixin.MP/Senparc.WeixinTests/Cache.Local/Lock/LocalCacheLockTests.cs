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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Cache;
using Senparc.Weixin.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache.Tests
{
    [TestClass()]
    public class LocalCacheLockTests
    {
        [TestMethod()]
        public void LocalCacheLockTest()
        {
            //实际上stragety在LocalCacheLock内部暂时没有用到，
            //这里给一个实例是因为还有一个基类，需要微程序提供良好的弹性
            var stragety = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance().BaseCacheStrategy();

            //强是指用本地缓存
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => LocalObjectCacheStrategy.Instance);

            using (stragety.BeginCacheLock("Test", "LocalCache"))  //1、等待并抢得锁
            {                                               //2、已获得锁，开始享受独占
                //操作公共资源                              //3、开始干活
            }                                               //4、打完收工，释放锁，下一个
        }


        //[TestMethod()]
        //public void LocalCacheLockTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void LockTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void LockTest1()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UnLockTest()
        //{
        //    Assert.Fail();
        //}
    }
}