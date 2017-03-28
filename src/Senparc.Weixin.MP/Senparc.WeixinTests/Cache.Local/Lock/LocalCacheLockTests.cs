using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var stragety = CacheStrategyFactory.GetObjectCacheStrategyInstance();

            using (new LocalCacheLock(stragety, "Test", "LocalCache"))  //1、等待并抢得锁
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