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
  
    文件名：TenPayV3ReverseRequestData.cs
    文件功能描述：微信支付撤销订单请求参数
    
    创建标识：Senparc - 20161227


    修改标识：Senparc - 20190521
    修改描述：v1.4.0 .NET Core 添加多证书注册功能；添加子商户号设置

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[撤销订单]
    /// </summary>
    public class TenPayV3ReverseRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 子商户公众账号ID，如果没有则不用设置
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 商户号[mch_id]
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 子商户号，如果没有则不用设置
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 微信的订单号，建议优先使用
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }

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
        /// 撤销订单 请求参数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="signType"></param>
        /// <param name="subAppId"></param>
        /// <param name="subMchId"></param>
        /// <param name="key"></param>
        /// <param name="transactionId"></param>
        /// <param name="nonceStr"></param>
        public TenPayV3ReverseRequestData(string appId, string mchId, string transactionId, string nonceStr,
            string outTradeNo, string key, string signType = "MD5", string subAppId = null, string subMchId = null)
        {
            AppId = appId;
            SubAppId = subAppId;
            MchId = mchId;
            SubMchId = subMchId;
            NonceStr = nonceStr;
            TransactionId = transactionId;
            SignType = signType;
            OutTradeNo = outTradeNo;
            Key = key;

            #region 设置RequestHandler
            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();
            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId); //公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId); //子商户公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId); //子商户号
            PackageRequestHandler.SetParameter("transaction_id", this.TransactionId); //微信的订单号
            PackageRequestHandler.SetParameter("out_trade_no", this.OutTradeNo); //商户系统内部的订单号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType); //签名类型
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}
