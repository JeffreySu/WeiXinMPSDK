/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SenparcWeixinSettingItemCollection.cs
    文件功能描述：SenparcWeixinSettingItem 集合
    
    
    创建标识：Senparc - 20180707

    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// SenparcWeixinSettingItem 集合
    /// </summary>
    public class SenparcWeixinSettingItemCollection : Dictionary<string, SenparcWeixinSettingItem>
    {
        public SenparcWeixinSettingItemCollection() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 设置或获取 SenparcWeixinSettingItem，key 不存在时会自动创建对象，因此不需要判断 key 是否存在
        /// </summary>
        /// <param name="key">SenparcWeixinSettingItem 标识</param>
        /// <returns></returns>
        new public SenparcWeixinSettingItem this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    base[key] = new SenparcWeixinSettingItem();
                }
                return base[key];
            }
            set
            {
                base[key] = value;
                base[key].ItemKey = key;//设置标识
            }
        }
    }
}
