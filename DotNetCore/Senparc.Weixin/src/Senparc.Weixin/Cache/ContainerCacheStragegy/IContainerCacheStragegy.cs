using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 容器缓存策略接口
    /// </summary>
    public interface IContainerCacheStragegy : IBaseCacheStrategy<string, IContainerItemCollection>
    {
        /// <summary>
        /// 更新ContainerBag
        /// </summary>
        /// <param name="key"></param>
        /// <param name="containerBag"></param>
        void UpdateContainerBag(string key, IBaseContainerBag containerBag);
    }
}
