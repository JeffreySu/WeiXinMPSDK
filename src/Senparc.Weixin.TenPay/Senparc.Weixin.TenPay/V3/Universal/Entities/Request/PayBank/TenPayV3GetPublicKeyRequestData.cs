﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
  
    文件名：TenPayV3GetPublicKeyRequestData.cs
    文件功能描述：获取 RSA 加密公钥接口 请求参数
    
    创建标识：Senparc - 20180410

    修改标识：Senparc - 201804010
    修改描述：v14.11.0 添加“付款到银行卡”接口

    修改标识：Senparc - 20190521
    修改描述：v1.4.0 .NET Core 添加多证书注册功能；添加子商户号设置
----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 获取 RSA 加密公钥接口
    /// </summary>
    public class TenPayV3GetPublicKeyRequestData
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 子商户号，如果没有则不用设置
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

    
        /// <summary>
        /// 签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;

        /// <summary>
        /// 通过MD5签名算法计算得出的签名值，详见MD5签名生成算法
        /// </summary>
        public readonly string Sign;

        public TenPayV3GetPublicKeyRequestData(string mchId, string nonceStr, string key, string subMchId = null)
        {
            MchId = mchId;
            SubMchId = subMchId;
            NonceStr = nonceStr;
            Key = key;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();
            //设置package订单参数
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId); //子商户号
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion

        }
    }
}
