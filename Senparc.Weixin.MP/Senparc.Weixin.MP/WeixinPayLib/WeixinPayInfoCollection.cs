using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.WeixinPayLib
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（PartnerId）
    /// </summary>
    public class WeixinPayInfoCollection : Dictionary<string, WeixinPayInfo>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（PartnerId）
        /// </summary>
        public static WeixinPayInfoCollection Data = new WeixinPayInfoCollection();

        /// <summary>
        /// 注册WeixinPayInfo信息
        /// </summary>
        /// <param name="weixinPayInfo"></param>
        public static void Register(WeixinPayInfo weixinPayInfo)
        {
            Data[weixinPayInfo.PartnerId] = weixinPayInfo;
        }

        new public WeixinPayInfo this[string key]
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

        public WeixinPayInfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
