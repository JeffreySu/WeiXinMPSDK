/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：FlushCache.cs
    文件功能描述：缓存立即生效方法


    创建标识：Senparc - 20160318

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MessageQueue;
using Senparc.Weixin.Threads;

namespace Senparc.Weixin.CacheUtility
{
    /// <summary>
    /// 缓存立即生效方法
    /// </summary>
    public class FlushCache : IDisposable
    {
        /// <summary>
        /// 是否立即个更新到缓存
        /// </summary>
        public bool DoFlush { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="doFlush">是否立即更新到缓存</param>
        public FlushCache(bool doFlush = true)
        {
            DoFlush = doFlush;
        }

        /// <summary>
        /// 释放，开始立即更新所有缓存
        /// </summary>
        public void Dispose()
        {
            if (DoFlush)
            {
                SenparcMessageQueue.OperateQueue();
            }
        }

        /// <summary>
        /// 创建一个FlushCache实例
        /// </summary>
        /// <param name="doFlush">是否立即更新到缓存</param>
        /// <returns></returns>
        public static FlushCache CreateInstance(bool doFlush = true)
        {
            return new FlushCache(doFlush);
        }
    }
}
