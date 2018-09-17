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
 
    文件名：TenPayV3Info.cs
    文件功能描述：微信支付V3基础信息储存类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：添加支持 SenparcWeixinSetting 参数的构造函数

    修改标识：Senparc - 20180802
    修改描述：v15.2.0 SenparcWeixinSetting 添加 TenPayV3_WxOpenTenpayNotify 属性，用于设置小程序支付回调地址

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付基础信息储存类
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3Info
    {
        /// <summary>
        /// 第三方用户唯一凭证appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 第三方用户唯一凭证密钥，即appsecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 商户支付密钥Key。登录微信商户后台，进入栏目【账户设置】【密码安全】【API 安全】【API 密钥】
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面
        /// </summary>
        public string TenPayV3Notify { get; set; } // = "http://localhost/payNotifyUrl.aspx";
        /// <summary>
        /// 小程序支付完成后的会掉处理页面
        /// </summary>
        public string TenPayV3_WxOpenNotify { get; set; }

        /// <summary>
        /// 微信支付 V3 参数 构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="mchId"></param>
        /// <param name="key"></param>
        /// <param name="tenPayV3Notify"></param>
        /// <param name="tenPayV3WxOpenNotify"></param>
        public TenPayV3Info(string appId, string appSecret, string mchId, string key, string tenPayV3Notify,string tenPayV3WxOpenNotify)
        {
            AppId = appId;
            AppSecret = appSecret;
            MchId = mchId;
            Key = key;
            TenPayV3Notify = tenPayV3Notify;
            TenPayV3_WxOpenNotify = tenPayV3WxOpenNotify;
        }


        /// <summary>
        /// 微信支付 V3 参数 构造函数
        /// </summary>
        /// <param name="senparcWeixinSetting">已经填充过微信支付（旧版本）参数的 SenparcWeixinSetting 对象</param>
        public TenPayV3Info(ISenparcWeixinSettingForTenpayV3 senparcWeixinSetting)
            : this(senparcWeixinSetting.TenPayV3_AppId,
                  senparcWeixinSetting.TenPayV3_AppSecret,
                  senparcWeixinSetting.TenPayV3_MchId,
                  senparcWeixinSetting.TenPayV3_Key,
                  senparcWeixinSetting.TenPayV3_TenpayNotify,
                  senparcWeixinSetting.TenPayV3_WxOpenTenpayNotify
                  )
        {
        }

    }
}
