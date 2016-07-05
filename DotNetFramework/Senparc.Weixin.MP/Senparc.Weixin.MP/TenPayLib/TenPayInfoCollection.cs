/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：TenPayInfoCollection.cs
    文件功能描述：微信支付信息集合，Key为商户号（PartnerId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.TenPayLib
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（PartnerId）
    /// </summary>
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
        public static void Register(TenPayInfo weixinPayInfo)
        {
            Data[weixinPayInfo.PartnerId] = weixinPayInfo;
        }

        new public TenPayInfo this[string key]
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
