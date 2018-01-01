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

    文件名：RedisCacheLock.cs
    文件功能描述：本地锁


    创建标识：Senparc - 20160810

    修改标识：Senparc - 20170205
    修改描述：v1.2.0 重构分布式锁

----------------------------------------------------------------*/

using System;
using Redlock.CSharp;

namespace Senparc.Weixin.Cache.Redis
{
    public class RedisCacheLock : BaseCacheLock
    {
        private Redlock.CSharp.Redlock _dlm;
        private Lock _lockObject;

        private RedisObjectCacheStrategy _redisStrategy;

        public RedisCacheLock(RedisObjectCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            : base(strategy, resourceName, key, retryCount, retryDelay)
        {
            _redisStrategy = strategy;
            LockNow();//立即等待并抢夺锁
        }

        public override bool Lock(string resourceName)
        {
            return Lock(resourceName, 0, new TimeSpan());
        }

        public override bool Lock(string resourceName, int retryCount, TimeSpan retryDelay)
        {
            if (retryCount != 0)
            {
                _dlm = new Redlock.CSharp.Redlock(retryCount, retryDelay, _redisStrategy._client);
            }
            else if (_dlm == null)
            {
                _dlm = new Redlock.CSharp.Redlock(_redisStrategy._client);
            }

            var ttl = (retryDelay.TotalMilliseconds > 0 ? retryDelay.TotalMilliseconds : 10)
                       *
                      (retryCount > 0 ? retryCount : 10);


            var successfull = _dlm.Lock(resourceName, TimeSpan.FromMilliseconds(ttl), out _lockObject);
            return successfull;
        }

        public override void UnLock(string resourceName)
        {
            if (_lockObject != null)
            {
                _dlm.Unlock(_lockObject);
            }
        }
    }
}
