using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Containers.Tests;

namespace Senparc.WeixinTests.Utilities.CachedUtility
{
    [Serializable]
    internal class CacheTestContainerBag : BaseContainerBag
    {
        private DateTime _dateTime;
        private string _appSecret;
        private string _appId;

        public string AppId
        {
            get { return _appId; }
            set { base.SetContainerProperty(ref _appId, value, "AppId"); }
        }

        public string AppSecret
        {
            get { return _appSecret; }
            set { base.SetContainerProperty(ref _appSecret, value, "AppSecret"); }
        }


        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value, "DateTime"); }
        }

    }

    internal class CacheTestContainer : BaseContainer<CacheTestContainerBag>
    {
        public static void Register(string appId, string appSecret)
        {
            using (FlushCache.CreateInstance())//注释掉这一行，FlushCacheTest()将失败
            {
                Update(appId, new CacheTestContainerBag()
                {
                    AppId = appId,
                    AppSecret = appSecret
                });
            }
        }

    }

    [TestClass]
    public class FlushCacheTests
    {
        [TestMethod]
        public void CreateInstanceTest()
        {
            var instance = FlushCache.CreateInstance();
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void FlushCacheTest()
        {
            //开启Redis缓存
            //CacheStrategyFactory.RegisterContainerCacheStrategy(() => RedisContainerCacheStrategy.Instance);//Redis

            var key = DateTime.Now.Ticks.ToString();
            var value = Guid.NewGuid().ToString();
            CacheTestContainer.Register(key, value);

            Assert.IsTrue(CacheTestContainer.CheckRegistered(key));
            var bag = CacheTestContainer.TryGetItem(key);
            Assert.IsNotNull(bag);
            Assert.IsInstanceOfType(bag, typeof(CacheTestContainerBag));
            Assert.AreEqual(key, bag.Key);
            Assert.AreEqual(value, bag.AppSecret);
        }
    }
}
