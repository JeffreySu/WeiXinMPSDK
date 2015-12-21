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

    public interface IContainerItemCollection : IDictionary<string, IBaseContainerBag>
    {
    }

    public class ContainerItemCollection : Dictionary<string, IBaseContainerBag>, IContainerItemCollection
    {
    }


    public interface IContainerCacheStragegy : /*IContainerCacheStragegy,*/ IBaseCacheStrategy<string, IContainerItemCollection>
    //where TContainerBag : class, IBaseContainerBag
    {
    }
}
