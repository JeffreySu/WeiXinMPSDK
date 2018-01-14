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


#region Apache License Version 2.0

#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 缓存锁接口
    /// </summary>
    public interface ICacheLock : IDisposable
    {
        /// <summary>
        /// 是否成功获得锁
        /// </summary>
        bool LockSuccessful { get; set; }

        ///// <summary>
        ///// 立即开始锁定
        ///// </summary>
        ///// <returns></returns>
        //ICacheLock LockNow();

        /// <summary>
        /// 开始锁
        /// </summary>
        /// <param name="resourceName">资源名称，即锁的标识，实际值为IBaseCacheStrategy接口中的 BeginCacheLock() 方法中的 [resourceName.key]</param>
        bool Lock(string resourceName);

        /// <summary>
        /// 开始锁，并设置重试条件
        /// </summary>
        /// <param name="resourceName">资源名称，即锁的标识，实际值为IBaseCacheStrategy接口中的 BeginCacheLock() 方法中的 [resourceName.key]</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryDelay">每次重试延时</param>
        /// <returns></returns>
        bool Lock(string resourceName, int retryCount, TimeSpan retryDelay);

        //bool IsLocked(string resourceName);

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="resourceName">需要释放锁的 Key，即锁的标识，实际值为IBaseCacheStrategy接口中的 BeginCacheLock() 方法中的 [resourceName.key]资源名称，和 Lock() 方法中的 resourceName 对应</param>
        void UnLock(string resourceName);
    }
}
