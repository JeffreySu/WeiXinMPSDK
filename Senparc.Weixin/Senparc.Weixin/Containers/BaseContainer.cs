/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeixinContainer.cs
    文件功能描述：微信容器（如Ticket、AccessToken）
    
    
    创建标识：Senparc - 20151003
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.Cache;

namespace Senparc.Weixin.Containers
{
    public interface IBaseContainer
    {

    }

    public interface IBaseContainer<TBag> : IBaseContainer where TBag : IBaseContainerBag, new()
    {
    }

    /// <summary>
    /// 微信容器接口（如Ticket、AccessToken）
    /// </summary>
    /// <typeparam name="TBag"></typeparam>
    public abstract class BaseContainer<TBag> : IBaseContainer<TBag> where TBag : class, IBaseContainerBag, new()
    {
        private static string GetCacheKey<T>()
        {
            return string.Format("Container:{0}", typeof(T));
        }

        private static IContainerCacheStragegy /*IBaseCacheStrategy<string,Dictionary<string, TBag>>*/ Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                return CacheStrategyFactory.GetContainerCacheStragegyInstance();
            }
        }

        /// <summary>
        /// 所有数据集合的列表
        /// </summary>
        private static IDictionary<string, IContainerItemCollection> CollectionList
        {
            get
            {
                //var cacheKey = GetCacheKey<TBag>();
                var list = Cache.GetAll();
                return list as IDictionary<string, IContainerItemCollection>;
            }
        }

        /// <summary>
        /// 获取当前容器的数据项集合
        /// </summary>
        /// <returns></returns>
        protected static IContainerItemCollection ItemCollection
        {
            get
            {
                var cacheKey = GetCacheKey<TBag>();
                if (!CollectionList.ContainsKey(cacheKey))
                {
                    CollectionList[cacheKey] = new ContainerItemCollection();
                }
                return CollectionList[cacheKey];
            }
        }

        /// <summary>
        /// 获取完整的数据集合的列表，包括所有的Container数据在内（建议不要进行任何修改操作）
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, IContainerItemCollection> GetCollectionList()
        {
            return CollectionList;
        }

        /// <summary>
        /// 获取所有容器内已经注册的项目
        /// （此方法将会遍历Dictionary，当数据项很多的时候效率会明显降低）
        /// </summary>
        /// <returns></returns>
        public static List<TBag> GetAllItems()
        {
            return ItemCollection.Select(z => z.Value as TBag).ToList();
        }

        /// <summary>
        /// 尝试获取某一项Bag
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TBag TryGetItem(string key)
        {
            if (ItemCollection.ContainsKey(key))
            {
                return ItemCollection[key] as TBag;
            }

            return default(TBag);
        }

        /// <summary>
        /// 尝试获取某一项Bag中的具体某个属性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="property">具体某个属性</param>
        /// <returns></returns>
        public static K TryGetItem<K>(string key, Func<TBag, K> property)
        {
            if (ItemCollection.ContainsKey(key))
            {
                var item = ItemCollection[key] as TBag;
                return property(item);
            }
            return default(K);
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">为null时删除该项</param>
        public static void Update(string key, TBag value)
        {
            if (value == null)
            {
                ItemCollection.Remove(key);
            }
            else
            {
                value.Key = key;//确保Key有值
                ItemCollection[key] = value;
            }
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="partialUpdate">为null时删除该项</param>
        public static void Update(string key, Action<TBag> partialUpdate)
        {
            if (partialUpdate == null)
            {
                ItemCollection.Remove(key);//移除对象
            }
            else
            {
                if (!ItemCollection.ContainsKey(key))
                {
                    ItemCollection[key] = new TBag()
                    {
                        Key = key//确保这一项Key已经被记录
                    };
                }
                partialUpdate(ItemCollection[key] as TBag);//更新对象
            }
        }

        /// <summary>
        /// 检查Key是否已经注册
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string key)
        {
            return ItemCollection.ContainsKey(key);
        }
    }
}
