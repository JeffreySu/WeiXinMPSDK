/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：BaseContainerBag.cs
    文件功能描述：微信容器接口中的封装Value（如Ticket、AccessToken等数据集合）
    
    
    创建标识：Senparc - 20151003
    
----------------------------------------------------------------*/

using System;
using System.Runtime.CompilerServices;
using Senparc.Weixin.Annotations;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Containers
{
    /// <summary>
    /// IBaseContainerBag
    /// </summary>
    public interface IBaseContainerBag
    {
        string Key { get; set; }
    }

    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
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

        public BaseContainerBag()
        {
            base.PropertyChanged += BaseContainerBag_PropertyChanged;
        }

        private void BaseContainerBag_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var containerBag = (BaseContainerBag) sender;
            var key = containerBag.Key;

            //获取对应Container的缓存相关

            //TODO:做消息列队，每过一段时间进行自动更新，防止属性连续被编辑，短时间内反复更新缓存。


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
    }

}
