#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
  
    文件名：TenPayV3CloseOrderData.cs
    文件功能描述：微信支付查询订单请求参数
    
    创建标识：Senparc - 20161226

    修改标识：Senparc - 20161227
    修改描述：v14.3.100 修改类名为TenPayV3OrderQueryRequestData.cs

    修改标识：Senparc - 20170225
    修改描述：v14.3.129 设置TransactionId和OutTradeNo时判断是否为null，如果是则提供空字符串""

    修改标识：Senparc - 20170301
    修改描述：v14.3.130 修改TenPayV3OrderQueryRequestData()中的sign_type设置位置

    修改标识：Senparc - 20170322
    修改描述：v14.3.135 修改TenPayV3OrderQueryRequestData()增加服务商请求参数

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[查询订单]
    /// </summary>
    public class TenPayV3OrderQueryRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 子商户公众账号ID
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 微信的订单号，建议优先使用
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///商户系统内部的订单号，请确保在同一商户号下唯一
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;


        /// <summary>
        /// 查询订单 请求参数[境内服务商]
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="subAppId"></param>
        /// <param name="mchId"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="signType"></param>
        /// <param name="key"></param>
        /// <param name="subMchId"></param>
        /// <param name="transactionId"></param>
        /// <param name="nonceStr"></param>
        public TenPayV3OrderQueryRequestData(string appId, string subAppId, string mchId, string subMchId, string transactionId, string nonceStr,
            string outTradeNo, string key, string signType = null)
        {
            AppId = appId;
            SubAppId = subAppId;
            MchId = mchId;
            SubMchId = subMchId;
            NonceStr = nonceStr;
            TransactionId = transactionId;
            OutTradeNo = outTradeNo;
            SignType = signType;
            Key = key;

            #region 设置RequestHandler

            PackageRequestHandler = new RequestHandler(null);

            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId); //公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId); //子商户公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId); //子商户号
            PackageRequestHandler.SetParameter("transaction_id", this.TransactionId ?? ""); //微信的订单号
            PackageRequestHandler.SetParameter("out_trade_no", this.OutTradeNo ?? ""); //商户系统内部的订单号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameterWhenNotNull("sign_type", this.SignType); //签名类型
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名
            #endregion
        }

        /// <summary>
        /// 查询订单 请求参数[境内普通商户]
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="transactionId"></param>
        /// <param name="nonceStr"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="key"></param>
        /// <param name="signType"></param>
        public TenPayV3OrderQueryRequestData(string appId, string mchId, string transactionId, string nonceStr,
            string outTradeNo, string key, string signType = "MD5")
            : this(appId, null, mchId, null, transactionId, nonceStr, outTradeNo, key, signType)
        {

        }
    }
}