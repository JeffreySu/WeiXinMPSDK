/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BaseContainerBag.cs
    文件功能描述：微信容器接口中的封装Value（如Ticket、AccessToken等数据集合）
    
    
    创建标识：Senparc - 20151003
    
----------------------------------------------------------------*/

using System;
using System.Runtime.CompilerServices;
using Senparc.Weixin.Annotations;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MessageQueue;

namespace Senparc.Weixin.Containers
{
    /// <summary>
    /// IBaseContainerBag
    /// </summary>
    public interface IBaseContainerBag
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 当前对象被缓存的时间
        /// </summary>
        DateTime CacheTime { get; set; }
    }

    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
    [Serializable]
    public class BaseContainerBag : BindableBase, IBaseContainerBag
    {
        private string _key;

        /// <summary>
        /// 通常为AppId
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { this.SetContainerProperty(ref _key, value); }
        }

        /// <summary>
        /// 缓存时间，不使用属性变化监听
        /// </summary>
        public DateTime CacheTime { get; set; }


        private void BaseContainerBag_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var containerBag = (IBaseContainerBag)sender;
            var mqKey = SenparcMessageQueue.GenerateKey("ContainerBag", sender.GetType(), containerBag.Key, "UpdateContainerBag");

            //获取对应Container的缓存相关

            //加入消息列队，每过一段时间进行自动更新，防止属性连续被编辑，短时间内反复更新缓存。
            SenparcMessageQueue mq = new SenparcMessageQueue();
            mq.Add(mqKey, () =>
            {
                var containerCacheStragegy = CacheStrategyFactory.GetContainerCacheStragegyInstance();
                var cacheKey = ContainerHelper.GetCacheKey(this.GetType());
                containerBag.CacheTime = DateTime.Now;//记录缓存时间
                containerCacheStragegy.UpdateContainerBag(cacheKey, containerBag);
            });
        }


        /// <summary>
        /// 设置Container属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetContainerProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            var result = base.SetProperty(ref storage, value, propertyName);
            return result;
        }

        public BaseContainerBag()
        {
            base.PropertyChanged += BaseContainerBag_PropertyChanged;
        }
    }
}
