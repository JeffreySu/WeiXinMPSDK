/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeixinContainer.cs
    文件功能描述：微信容器接口（如Ticket、AccessToken）
    
    
    创建标识：Senparc - 20151003
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Containers
{
    public interface IWeixinContainerBag
    {
        string Key { get; set; }
    }

    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
    public class BaseContainerBag : IWeixinContainerBag
    {
        /// <summary>
        /// 通常为AppId
        /// </summary>
        public string Key { get; set; }
    }

    /// <summary>
    /// 微信容器接口（如Ticket、AccessToken）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseContainer<T> where T : IWeixinContainerBag, new()
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        static Dictionary<string, T> _itemCollection = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);



        /// <summary>
        /// 获取所有容器内已经注册的项目
        /// （此方法将会遍历Dictionary，当数据项很多的时候效率会明显降低）
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAllItems()
        {
            return _itemCollection.Select(z => z.Value).ToList();
        }

        /// <summary>
        /// 尝试获取某一项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T TryGetItem(string key)
        {
            if (_itemCollection.ContainsKey(key))
            {
                return _itemCollection[key];
            }

            return default(T);
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
                _itemCollection.Remove(key);
            }
            else
            {
                _itemCollection[key] = value;
            }
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="partialUpdate">为null时删除该项</param>
        public static void Update(string key, Func<T, object> partialUpdate)
        {
            if (partialUpdate == null)
            {
                _itemCollection.Remove(key);
            }
            else
            {
                partialUpdate(_itemCollection[key]);
            }
        }
    }
}
