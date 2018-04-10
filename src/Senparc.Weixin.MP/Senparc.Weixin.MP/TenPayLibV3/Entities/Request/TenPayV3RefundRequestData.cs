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
  
    文件名：TenPayV3RefundRequestData.cs
    文件功能描述：微信支付统一请求参数
    
    创建标识：Senparc - 20170502
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[申请退款]
    /// </summary>
    public class TenPayV3RefundRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }


        /// <summary>
        /// 商户自定义的终端设备号，如门店编号、设备的ID
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 微信订单号（和OutTradeNo二选一）
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统内部的订单号（和TransactionId二选一）
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户侧传给微信的退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 订单金额。订单总金额，单位为分，只能为整数，详见支付金额
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 退款金额。退款总金额，订单总金额，单位为分，只能为整数，详见支付金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型
        /// </summary>
        public string RefundFeeType { get; set; }

        /// <summary>
        /// 操作员，操作员帐号, 默认为商户号
        /// </summary>
        public string OpUserId { get; set; }

        /// <summary>
        /// 退款资金来源。仅针对老资金流商户使用
        /// REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款（默认使用未结算资金退款）
        /// REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款(限非当日交易订单的退款）
        /// </summary>
        public string RefundAccount { get; set; }

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
        /// 申请退款 请求参数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="deviceInfo"></param>
        /// <param name="transactionId"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="outRefundNo"></param>
        /// <param name="refundFeeType"></param>
        /// <param name="signType"></param>
        /// <param name="totalFee"></param>
        /// <param name="refundFee"></param>
        /// <param name="opUserId"></param>
        /// <param name="refundAccount"></param>
        public TenPayV3RefundRequestData(string appId, string mchId, string key, string deviceInfo, string nonceStr,
            string transactionId, string outTradeNo, string outRefundNo, int totalFee, int refundFee,
            string opUserId, string refundAccount,
            string refundFeeType = "CNY", string signType = "MD5")
        {
            AppId = appId;
            MchId = mchId;
            Key = key;
            NonceStr = nonceStr;
            DeviceInfo = deviceInfo;
            TransactionId = transactionId;
            OutTradeNo = outTradeNo;
            OutRefundNo = outRefundNo;
            TotalFee = totalFee;
            RefundFee = refundFee;
            OpUserId = opUserId;
            RefundAccount = refundAccount;
            RefundFeeType = refundFeeType;

            SignType = signType;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();
            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId); //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameterWhenNotNull("device_info", this.DeviceInfo);
            PackageRequestHandler.SetParameter("sign_type", this.SignType);
            PackageRequestHandler.SetParameterWhenNotNull("transaction_id", this.TransactionId);
            PackageRequestHandler.SetParameterWhenNotNull("out_trade_no", this.OutTradeNo);
            PackageRequestHandler.SetParameter("out_refund_no", this.OutRefundNo);
            PackageRequestHandler.SetParameter("total_fee", this.TotalFee.ToString());
            PackageRequestHandler.SetParameter("refund_fee", this.RefundFee.ToString());
            PackageRequestHandler.SetParameter("op_user_id", this.OpUserId);
            PackageRequestHandler.SetParameterWhenNotNull("refund_account", this.RefundAccount);
            PackageRequestHandler.SetParameterWhenNotNull("refund_fee_type", this.RefundFeeType);
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}