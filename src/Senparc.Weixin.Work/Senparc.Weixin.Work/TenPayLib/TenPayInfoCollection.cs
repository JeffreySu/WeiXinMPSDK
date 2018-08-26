/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：TenPayInfoCollection.cs
    文件功能描述：微信支付信息集合，Key为商户号（MchId）


    创建标识：Senparc - 20150722
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.Work.TenPayLib
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（MchId）
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayInfoCollection : Dictionary<string, TenPayInfo>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（MchId）
        /// </summary>
        public static TenPayInfoCollection Data = new TenPayInfoCollection();

        /// <summary>
        /// 注册TenPayInfo信息
        /// </summary>
        /// <param name="tenPayInfo"></param>
        public static void Register(TenPayInfo tenPayInfo)
        {
            Data[tenPayInfo.MchId] = tenPayInfo;
        }

        public new TenPayInfo this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("TenPayInfoCollection尚未注册Mch：{0}", key));
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
