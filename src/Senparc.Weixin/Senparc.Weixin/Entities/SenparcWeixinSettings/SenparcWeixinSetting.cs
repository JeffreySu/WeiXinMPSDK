/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SenparcWeixinSetting.cs
    文件功能描述：Senparc.Weixin JSON 配置
    
    
    创建标识：Senparc - 20170302

    修改标识：Senparc - 20180622
    修改描述：v5.0.3.1 SenparcWeixinSetting 添加 Cache_Memcached_Configuration 属性
    
    修改标识：Senparc - 20180622
    修改描述：v5.0.6.2 WeixinRegister.UseSenparcWeixin() 方法去除 isDebug 参数，提供扩展缓存自动扫描添加功能

    修改标识：Senparc - 20180622
    修改描述：v5.0.8 SenparcWeixinSetting 构造函数提供 isDebug 参数

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// Senparc.Weixin JSON 配置
    /// </summary>
    public class SenparcWeixinSetting : SenparcWeixinSettingItem//继承 SenparcWeixinSettingItem 是为了可以得到一组默认的参数，方便访问
    {
        #region 微信全局

        /// <summary>
        /// 是否处于 Debug 状态（仅限微信范围）
        /// </summary>
        public bool IsDebug { get; set; }

        #endregion

        /// <summary>
        /// 系统中所有微信设置的参数，默认已经添加一个 Key 为“Default”的对象
        /// </summary>
        public SenparcWeixinSettingItemCollection Items { get; set; }

        /// <summary>
        /// SenparcWeixinSetting 构造函数
        /// </summary>
        public SenparcWeixinSetting(): this(false)
        { }

        /// <summary>
        /// SenparcWeixinSetting 构造函数
        /// </summary>
        /// <param name="isDebug">是否开启 Debug 模式</param>
        public SenparcWeixinSetting(bool isDebug)
        {
            IsDebug = isDebug;

            Items = new SenparcWeixinSettingItemCollection();
            Items["Default"] = this;//储存第一个默认参数
        }
    }
}
