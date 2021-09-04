using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 为 JsApi 在 UI 输出准备的信息包
    /// </summary>
    public class JsApiUiPackage
    {
        /// <summary>
        /// 微信AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机码
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// PrepayId的打包信息
        /// </summary>
        public string PrepayIdPackage { get; set; }

        public JsApiUiPackage(string appId, string timestamp, string nonceStr,string prepayIdPackage, string signature)
        {
            AppId = appId;
            Timestamp = timestamp;
            NonceStr = nonceStr;
            PrepayIdPackage = prepayIdPackage;
            Signature = signature;
        }
    }
}
