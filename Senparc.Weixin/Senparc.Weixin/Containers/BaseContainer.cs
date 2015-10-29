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
    public interface IBaseContainer<T> where T : IBaseContainerBag, new()
    {
    }

    /// <summary>
    /// 微信容器接口（如Ticket、AccessToken）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseContainer<T> : IBaseContainer<T> where T : class, IBaseContainerBag, new()
    {

        private static string GetCacheKey<T>()
        {
            return string.Format("Container:{0}", typeof(T));
        }

        private static IBaseCacheStrategy<Dictionary<Type, Dictionary<string, T>>> Cache
        {
            get
            {


                //TODO:使用工厂模式或者配置进行动态加载
                return LocalContainerCacheStrategy<Dictionary<Type, Dictionary<string, T>>>.Instance;
            }
        }

        /// <summary>
        /// 所有数据集合的列表
        /// </summary>
        private static Dictionary<Type, Dictionary<string, T>> CollectionList
        {
            get
            {
                var cacheKey = GetCacheKey<T>();
                return Cache.Get(cacheKey);
            }
        }

        /// <summary>
        /// 获取当前容器的数据项集合
        /// </summary>
        /// <returns></returns>
        protected static Dictionary<string, T> ItemCollection
        {
            get
            {
                if (!CollectionList.ContainsKey(typeof(T)))
                {
                    CollectionList[typeof(T)] = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
                }
                return CollectionList[typeof(T)];
            }
        }

        /// <summary>
        /// 获取完整的数据集合的列表，包括所有的Container数据在内（建议不要进行任何修改操作）
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Type, Dictionary<string, T>> GetCollectionList()
        {
            return CollectionList;
        }

        /// <summary>
        /// 获取所有容器内已经注册的项目
        /// （此方法将会遍历Dictionary，当数据项很多的时候效率会明显降低）
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAllItems()
        {
            return ItemCollection.Select(z => z.Value).ToList();
        }

        /// <summary>
        /// 尝试获取某一项Bag
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T TryGetItem(string key)
        {
            if (ItemCollection.ContainsKey(key))
            {
                return ItemCollection[key];
            }

            return default(T);
        }

        /// <summary>
        /// 尝试获取某一项Bag中的具体某个属性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="property">具体某个属性</param>
        /// <returns></returns>
        public static K TryGetItem<K>(string key, Func<T, K> property)
        {
            if (ItemCollection.ContainsKey(key))
            {
                var item = ItemCollection[key];
                return property(item);
            }
            return default(K);
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">为null时删除该项</param>
        public static void Update(string key, T value)
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
        public static void Update(string key, Action<T> partialUpdate)
        {
            if (partialUpdate == null)
            {
                ItemCollection.Remove(key);//移除对象
            }
            else
            {
                if (!ItemCollection.ContainsKey(key))
                {
                    ItemCollection[key] = new T()
                    {
                        Key = key//确保这一项Key已经被记录
                    };
                }
                partialUpdate(ItemCollection[key]);//更新对象
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
