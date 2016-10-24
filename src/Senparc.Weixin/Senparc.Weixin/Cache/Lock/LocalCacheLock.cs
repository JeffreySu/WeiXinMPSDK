using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache
{
    public class LocalCacheLock : BaseCacheLock
    {
        private IBaseCacheStrategy _localStrategy;
        public LocalCacheLock(IBaseCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            :base(strategy,resourceName,key,retryCount,retryDelay)
        {
            _localStrategy = strategy;
        }

        private static Dictionary<string, object> LockPool = new Dictionary<string, object>();
        private static Random _rnd = new Random();

        private bool RetryLock(string resourceName, int retryCount, TimeSpan retryDelay, Func<bool> action)
        {
            int currentRetry = 0;
            int maxRetryDelay = (int)retryDelay.TotalMilliseconds;
            while (currentRetry++ < retryCount)
            {
                if (action())
                {
                    return true;//取得锁
                }
                Thread.Sleep(_rnd.Next(maxRetryDelay));
            }
            return false;
        }

        public override bool Lock(string resourceName)
        {
            return Lock(resourceName, 9999 /*暂时不限制*/, new TimeSpan(0, 0, 0, 0, 20));
        }

        public override bool Lock(string resourceName, int retryCount, TimeSpan retryDelay)
        {
            var successful = RetryLock(resourceName, retryCount, retryDelay, () =>
            {
                try
                {
                    if (LockPool.ContainsKey(resourceName))
                    {
                        return false;//已被别人锁住，没有取得锁
                    }
                    else
                    {
                        LockPool.Add(resourceName, new object());//创建锁
                        return true;//取得锁
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.Log("本地同步锁发生异常：" + ex.Message);
                    return false;
                }
            }
                           );
            return successful;
        }

        public override void UnLock(string resourceName)
        {
            LockPool.Remove(resourceName);
        }
    }
}
