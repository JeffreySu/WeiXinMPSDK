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
  
    文件名：TenPayV3MicroPayRequestData.cs
    文件功能描述：提交刷卡支付请求参数
    
    创建标识：Senparc - 20161227
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 【境内服务商】微信支付提交的XML Data数据[提交刷卡支付]
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3MicroPayRequestData_ServiceProvider : TenPayV3MicroPayRequestData
    {
        /// <summary>
        /// String(32)	wx8888888888888888	微信分配的子商户公众账号ID
        /// </summary>
        public string SubAppId { get; set; }
        /// <summary>
        /// String(32)	1900000109	微信支付分配的子商户号，开发者模式下必填
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 提交刷卡支付 请求参数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="deviceInfo"></param>
        /// <param name="body"></param>
        /// <param name="detail"></param>
        /// <param name="attach"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="totalFee"></param>
        /// <param name="feeType"></param>
        /// <param name="spbillCreateIp"></param>
        /// <param name="goodsTag"></param>
        /// <param name="authCode"></param>
        /// <param name="signType"></param>
        /// <param name="sub_appid"></param>
        /// <param name="sub_mch_id"></param>
        public TenPayV3MicroPayRequestData_ServiceProvider(string appId, string mchId,
            string sub_appid,
            string sub_mch_id,
            string key, string nonceStr, string deviceInfo,
            string body, string detail, string attach, string outTradeNo, string totalFee, string feeType,
            string spbillCreateIp, string goodsTag, string authCode, string signType = "MD5")
            : base(appId, mchId,
             key, nonceStr, deviceInfo,
             body, detail, attach, outTradeNo, totalFee, feeType,
             spbillCreateIp, goodsTag, authCode, signType)
        {
            SubAppId = sub_appid;
            SubMchId = sub_mch_id;


            #region 设置RequestHandler

            PackageRequestHandler.SetParameter("sub_appid", this.SubAppId); //微信分配的子商户公众账号ID

            PackageRequestHandler.SetParameter("sub_mch_id", this.SubMchId); //微信支付分配的子商户号，开发者模式下必填

            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }

    }
}