#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：TenPayInfoCollection.cs
    文件功能描述：微信支付信息集合，Key为商户号（PartnerId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：TenPayInfoCollection 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.TenPayLib
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（PartnerId）
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
    public class TenPayInfoCollection : Dictionary<string, TenPayInfo>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（PartnerId）
        /// </summary>
        public static TenPayInfoCollection Data = new TenPayInfoCollection();

        /// <summary>
        /// 注册WeixinPayInfo信息
        /// </summary>
        /// <param name="weixinPayInfo"></param>
        /// <param name="name">公众号唯一标识（或名称）</param>
        public static void Register(TenPayInfo weixinPayInfo, string name)
        {
            Data[weixinPayInfo.PartnerId] = weixinPayInfo;

            //添加到全局变量
            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinPay_PartnerId = weixinPayInfo.PartnerId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinPay_Key = weixinPayInfo.Key;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinPay_AppId = weixinPayInfo.AppId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinPay_AppKey = weixinPayInfo.AppKey;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinPay_TenpayNotify = weixinPayInfo.TenPayNotify;
            }
        }

        public new TenPayInfo this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("WeixinPayInfoCollection尚未注册Partner：{0}", key));
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                base[key] = value;
            }
        }

        public TenPayInfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
