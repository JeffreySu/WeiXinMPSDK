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
    /// 微信支付提交的XML Data数据[提交刷卡支付]
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3MicroPayRequestData
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
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 终端设备号(商户自定义，如门店编号)
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 商品简单描述，该字段须严格按照规范传递
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 商品详细列表
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 商户系统内部的订单号,32个字符内、可包含字母
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public string TotalFee { get; set; }


        /// <summary>
        /// 符合ISO4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string FeeType { get; set; }

        /// <summary>
        /// 调用微信支付API的机器IP
        /// </summary>
        public string SpbillCreateIp { get; set; }

        /// <summary>
        /// 商品标记
        /// </summary>
        public string GoodsTag { get; set; }

        /// <summary>
        /// 扫码支付授权码
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public string Sign;

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
        public TenPayV3MicroPayRequestData(string appId, string mchId, string key, string nonceStr, string deviceInfo,
            string body, string detail, string attach, string outTradeNo, string totalFee, string feeType,
            string spbillCreateIp, string goodsTag, string authCode, string signType = "MD5")
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            DeviceInfo = deviceInfo;
            Body = body;
            Detail = detail;
            Attach = attach;
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            FeeType = feeType;
            SpbillCreateIp = spbillCreateIp;
            GoodsTag = goodsTag;
            AuthCode = authCode;
            SignType = signType;
            Key = key;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();
            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId); //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameter("device_info", this.DeviceInfo); //终端设备号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType); //签名类型
            PackageRequestHandler.SetParameter("body", this.Body); //商品简单描述
            PackageRequestHandler.SetParameter("detail", this.Detail); //商品详细列表
            PackageRequestHandler.SetParameter("attach", this.Attach); //附加数据
            PackageRequestHandler.SetParameter("out_trade_no", this.OutTradeNo); //商户系统内部的订单号
            PackageRequestHandler.SetParameter("total_fee", this.TotalFee); //订单总金额
            PackageRequestHandler.SetParameter("fee_type", this.FeeType); //货币类型
            PackageRequestHandler.SetParameter("spbill_create_ip", this.SpbillCreateIp); //终端IP
            PackageRequestHandler.SetParameter("goods_tag", this.GoodsTag); //商品标记
            PackageRequestHandler.SetParameter("auth_code", this.AuthCode); //授权码

            // TODO:★554393109 修改
            //***************************************************************************************
            Sign = "MD5".Equals(signType, System.StringComparison.OrdinalIgnoreCase) 
                ? PackageRequestHandler.CreateMd5Sign("key", this.Key) 
                : PackageRequestHandler.CreateSha256Sign("key", this.Key);
            //***************************************************************************************

            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}