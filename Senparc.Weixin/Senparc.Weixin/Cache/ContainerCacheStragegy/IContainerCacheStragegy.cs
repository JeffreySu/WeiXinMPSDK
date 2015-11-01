using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    public interface IContainerCacheStragegy
    {
    }

    public interface IContainerCacheStragegy<TContainerBag> : IContainerCacheStragegy, IBaseCacheStrategy<string, Dictionary<string, TContainerBag>>
     where TContainerBag : class, IBaseContainerBag
    {
    }
}
