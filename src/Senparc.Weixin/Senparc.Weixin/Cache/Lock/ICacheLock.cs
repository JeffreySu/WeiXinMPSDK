using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// 立即开始锁定
        /// </summary>
        /// <returns></returns>
        ICacheLock LockNow();

        /// <summary>
        /// 开始锁
        /// </summary>
        /// <param name="resourceName"></param>
        bool Lock(string resourceName);

        /// <summary>
        /// 开始锁，并设置重试条件
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="retryCount"></param>
        /// <param name="retryDelay"></param>
        /// <returns></returns>
        bool Lock(string resourceName, int retryCount, TimeSpan retryDelay);

        //bool IsLocked(string resourceName);

        //释放锁
        void UnLock(string resourceName);
    }
}
