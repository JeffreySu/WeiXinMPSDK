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

    文件名：TenPayV3InfoCollection.cs
    文件功能描述：微信支付V3信息集合，Key为商户号（MchId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：TenPayV3InfoCollection 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20180802
    修改描述：v15.2.0 SenparcWeixinSetting 添加 TenPayV3_WxOpenTenpayNotify 属性，用于设置小程序支付回调地址


    TODO：升级为Container
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（MchId）
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3InfoCollection : Dictionary<string, TenPayV3Info>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（MchId）
        /// </summary>
        public static TenPayV3InfoCollection Data = new TenPayV3InfoCollection();

        /// <summary>
        /// 注册TenPayV3Info信息
        /// </summary>
        /// <param name="tenPayV3Info"></param>
        /// <param name="name">公众号唯一标识（或名称）</param>
        public static void Register(TenPayV3Info tenPayV3Info,string name)
        {
            Data[tenPayV3Info.MchId] = tenPayV3Info;

            //添加到全局变量
            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_AppId = tenPayV3Info.AppId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_AppSecret = tenPayV3Info.AppSecret;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_MchId = tenPayV3Info.MchId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_Key = tenPayV3Info.Key;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_TenpayNotify = tenPayV3Info.TenPayV3Notify;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_WxOpenTenpayNotify = tenPayV3Info.TenPayV3_WxOpenNotify;
            }
        }

        /// <summary>
        /// 索引 TenPayV3Info
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new TenPayV3Info this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("TenPayV3InfoCollection尚未注册Mch：{0}", key));
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

        /// <summary>
        /// TenPayV3InfoCollection 构造函数
        /// </summary>
        public TenPayV3InfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
