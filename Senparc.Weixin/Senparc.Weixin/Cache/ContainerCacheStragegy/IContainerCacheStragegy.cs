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

    public interface IContainerCacheStragegy : /*IContainerCacheStragegy,*/ IBaseCacheStrategy<string, IDictionary<string, IBaseContainerBag>>
    //where TContainerBag : class, IBaseContainerBag
    {
    }
}
