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
  
    文件名：TenPayV3DownloadBillRequestData.cs
    文件功能描述：微信支付下载对账单请求参数 
    
    创建标识：Senparc - 20170215
    
    修改标识：Senparc - 20170716
    修改描述：修改TenPayV3DownloadBillRequestData

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[下载对账单]
    /// </summary>
    public class TenPayV3DownloadBillRequestData
    {
        /// <summary>
        /// 公众账号ID [appid]
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号 [mch_id]
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户号 [device_info]
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        ///对账单日期	[bill_date]
        /// </summary>
        public string BillDate { get; set; }

        /// <summary>
        /// 账单类型	[bill_type]
        /// </summary>
        public string BillType { get; set; }

        /// <summary>
        /// 压缩账单	[tar_type]
        /// </summary>
        public string TarType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 下载对账单
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="nonceStr"></param>
        /// <param name="deviceInfo"></param>
        /// <param name="billDate"></param>
        /// <param name="billType"></param>
        /// <param name="key"></param>
        /// <param name="tarType"></param>
        /// <param name="signType"></param>
        public TenPayV3DownloadBillRequestData(string appId, string mchId, string nonceStr, string deviceInfo,
            string billDate, string billType, string key, string tarType = "GZIP", string signType = "MD5")
        {
            PackageRequestHandler = new RequestHandler();

            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            DeviceInfo = deviceInfo;
            BillDate = billDate;
            BillType = billType;
            SignType = signType;
            TarType = tarType;
            Key = key;

            #region 设置RequestHandler

            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId); //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameterWhenNotNull("device_info", this.DeviceInfo); //设备号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameterWhenNotNull("sign_type", this.SignType); //签名类型
            PackageRequestHandler.SetParameter("bill_date", this.BillDate); //对账单日期
            PackageRequestHandler.SetParameter("bill_type", this.BillType); //账单类型
            PackageRequestHandler.SetParameterWhenNotNull("tar_type", this.TarType); //压缩账单

            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}