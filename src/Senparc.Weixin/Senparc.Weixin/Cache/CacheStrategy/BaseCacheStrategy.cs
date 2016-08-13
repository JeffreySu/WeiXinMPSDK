/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：BaseCacheStrategy.cs
    文件功能描述：泛型缓存策略基类。


    创建标识：Senparc - 20160813 v4.7.7 

 ----------------------------------------------------------------*/


using System;
using System.Collections.Generic;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 泛型缓存策略基类
    /// </summary>
    public abstract class BaseCacheStrategy : IBaseCacheStrategy
    {
        public string GetFinalKey(string key, bool isFullKey = false)
        {
            return isFullKey ? key : String.Format("SenparcWeixin:{0}:{1}", Config.DefaultCacheNamespace, key);
        }

        public abstract ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan());
    }
}
