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
    public interface ICacheLock
    {
        /// <summary>
        /// 开始锁
        /// </summary>
        /// <param name="resourceName"></param>
        bool Lock(string resourceName);
        //释放锁
        void UnLock(string resourceName);
    }
}
