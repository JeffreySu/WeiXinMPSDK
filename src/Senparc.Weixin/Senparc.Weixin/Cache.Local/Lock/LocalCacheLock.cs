#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：LocalCacheLock.cs
    文件功能描述：本地锁


    创建标识：Senparc - 20160810

    修改标识：Senparc - 20170205
    修改描述：1、修改默认retryDelay时间为10毫秒，retryCount为99999，总时间为16.6分钟
              2、更新构造函数
              3、重构方法

----------------------------------------------------------------*/




using System;
using System.Collections.Generic;
using System.Threading;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 本地锁
    /// </summary>
    public class LocalCacheLock : BaseCacheLock
    {
        private IBaseCacheStrategy _localStrategy;
        public LocalCacheLock(IBaseCacheStrategy strategy, string resourceName, string key,
            int? retryCount = null, TimeSpan? retryDelay = null)
            : base(strategy, resourceName, key, retryCount ?? 0, retryDelay ?? TimeSpan.FromMilliseconds(10))
        {
            _localStrategy = strategy;
            LockNow();//立即等待并抢夺锁
        }

        /// <summary>
        /// 锁存放容器
        /// </summary>
        private static Dictionary<string, object> LockPool = new Dictionary<string, object>();
        /// <summary>
        /// 随机数
        /// </summary>
        private static Random _rnd = new Random();
        /// <summary>
        /// 读取LockPool时的锁
        /// </summary>
        private static object lookPoolLock = new object();

        public override bool Lock(string resourceName)
        {
            return Lock(resourceName, 99999 /*暂时不限制*/, new TimeSpan(0, 0, 0, 0, 10));
        }

        public override bool Lock(string resourceName, int retryCount, TimeSpan retryDelay)
        {

            int currentRetry = 0;
            int maxRetryDelay = (int)retryDelay.TotalMilliseconds;
            while (currentRetry++ < retryCount)
            {
                #region 尝试获得锁

                var getLock = false;
                try
                {
                    lock (lookPoolLock)
                    {
                        if (LockPool.ContainsKey(resourceName))
                        {
                            getLock = false;//已被别人锁住，没有取得锁
                        }
                        else
                        {
                            LockPool.Add(resourceName, new object());//创建锁
                            getLock = true;//取得锁
                        }
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.Log("本地同步锁发生异常：" + ex.Message);
                    getLock = false;
                }

                #endregion

                if (getLock)
                {
                    return true;//取得锁
                }
                Thread.Sleep(_rnd.Next(maxRetryDelay));
            }
            return false;
        }

        public override void UnLock(string resourceName)
        {
            lock (lookPoolLock)
            {
                LockPool.Remove(resourceName);
            }
        }
    }
}
