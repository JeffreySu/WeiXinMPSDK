using Senparc.Weixin.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache.ContainerCacheStragegy
{
	/// <summary>
	/// IContainerItemCollection，对某个Container下的缓存值ContainerBag进行封装 
	/// </summary>
	public interface IContainerItemCollection : IBaseCacheStrategy<string, IBaseContainerBag>
	{
		/// <summary>
		/// 创建时间
		/// </summary>
		DateTime CreateTime { get; set; }
		IBaseContainerBag this[string key] { get; set; }
	}

	/// <summary>
	/// 储存某个Container下所有ContainerBag的字典集合
	/// </summary>
	public class ContainerItemCollection : IContainerItemCollection
	{
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		private Dictionary<string, IBaseContainerBag> _cache = new Dictionary<string, IBaseContainerBag>();

		public ContainerItemCollection()
		{
			CreateTime = DateTime.Now;
		}
		public string CacheSetKey { get; set; }

		public IBaseContainerBag this[string key]
		{
			get
			{
				return this.Get(key);
			}

			set
			{
				this.Update(key, value);
			}
		}

		public void InsertToCache(string key, IBaseContainerBag value)
		{
			_cache[key] = value;
		}

		public void RemoveFromCache(string key)
		{
			_cache.Remove(key);
		}

		public IBaseContainerBag Get(string key)
		{
			if (this.CheckExisted(key))
			{
				return _cache[key];
			}
			return null;
		}

		public IDictionary<string, IBaseContainerBag> GetAll()
		{
			return _cache;
		}

		public bool CheckExisted(string key)
		{
			return _cache.ContainsKey(key);
		}

		public long GetCount()
		{
			return _cache.Count;
		}

		public void Update(string key, IBaseContainerBag value)
		{
			_cache[key] = value;
		}
	}
}
