using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// IContainerItemCollection，对某个Container下的缓存值ContainerBag进行封装
    /// </summary>
    public interface IContainerItemCollection : IDictionary<string, IBaseContainerBag>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 储存某个Container下所有ContainerBag的字典集合
    /// </summary>
    public class ContainerItemCollection : Dictionary<string, IBaseContainerBag>, IContainerItemCollection
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public ContainerItemCollection()
        {
            CreateTime = DateTime.Now;
        }
    }
}
