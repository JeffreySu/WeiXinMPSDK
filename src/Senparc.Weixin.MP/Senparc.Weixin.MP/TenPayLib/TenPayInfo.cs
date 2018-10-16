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
 
    文件名：TenPayInfo.cs
    文件功能描述：微信支付基础信息储存类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：添加支持 SenparcWeixinSetting 参数的构造函数

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.MP.TenPayLib
{
    /// <summary>
    /// 微信支付基础信息储存类
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
    public class TenPayInfo
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string PartnerId { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// paysignkey(非appkey) 
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面,*替换成notify_url.asp所在路径
        /// </summary>
        public string TenPayNotify { get; set; } // = "http://localhost/payNotifyUrl.aspx";

        /// <summary>
        /// 微信支付（旧版本）参数 构造函数
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="key">密钥</param>
        /// <param name="appId">appid</param>
        /// <param name="appKey">paysignkey(非appkey) </param>
        /// <param name="tenPayNotify">支付完成后的回调处理页面,*替换成notify_url.asp所在路径</param>
        public TenPayInfo(string partnerId, string key, string appId, string appKey, string tenPayNotify)
        {
            PartnerId = partnerId;
            Key = key;
            AppId = appId;
            AppKey = appKey;
            TenPayNotify = tenPayNotify;
        }

        /// <summary>
        /// 微信支付（旧版本）参数 构造函数
        /// </summary>
        /// <param name="senparcWeixinSetting">已经填充过微信支付（旧版本）参数的 SenparcWeixinSetting 对象</param>
        public TenPayInfo(ISenparcWeixinSettingForOldTenpay senparcWeixinSetting)
            : this(senparcWeixinSetting.WeixinPay_PartnerId,
                   senparcWeixinSetting.WeixinPay_Key,
                   senparcWeixinSetting.WeixinPay_AppId,
                   senparcWeixinSetting.WeixinPay_AppKey,
                   senparcWeixinSetting.WeixinPay_TenpayNotify)
        {

        }
    }
}
