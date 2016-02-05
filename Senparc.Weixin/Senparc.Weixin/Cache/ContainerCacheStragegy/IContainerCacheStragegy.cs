using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    //public interface IContainerCacheStragegy : IBaseCacheStrategy
    //{
    //}

    /// <summary>
    /// IContainerItemCollection
    /// </summary>
    public interface IContainerItemCollection : IDictionary<string, IBaseContainerBag>
    {
    }

    /// <summary>
    /// 储存某个Container下所有ContainerBag的字典集合
    /// </summary>
    public class ContainerItemCollection : Dictionary<string, IBaseContainerBag>, IContainerItemCollection
    {
    }

    public interface IContainerCacheStragegy : /*IContainerCacheStragegy,*/ IBaseCacheStrategy<string, IContainerItemCollection>
    //where TContainerBag : class, IBaseContainerBag
    {
        void UpdateContainerBag(string key, IBaseContainerBag containerBag);
    }
}
