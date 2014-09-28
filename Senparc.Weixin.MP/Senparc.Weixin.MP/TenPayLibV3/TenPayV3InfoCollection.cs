using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（PartnerId）
    /// </summary>
    public class TenPayV3InfoCollection : Dictionary<string, TenPayV3Info>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（PartnerId）
        /// </summary>
        public static TenPayV3InfoCollection Data = new TenPayV3InfoCollection();

        /// <summary>
        /// 注册WeixinPayInfo信息
        /// </summary>
        /// <param name="weixinPayInfo"></param>
        public static void Register(TenPayV3Info weixinPayInfo)
        {
            Data[weixinPayInfo.AppId] = weixinPayInfo;
        }

        new public TenPayV3Info this[string key]
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

        public TenPayV3InfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
