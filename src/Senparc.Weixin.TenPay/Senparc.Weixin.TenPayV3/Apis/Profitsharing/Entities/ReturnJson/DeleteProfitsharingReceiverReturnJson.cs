#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：DeleteProfitsharingReceiverReturnJson.cs
    文件功能描述：删除分账接收方返回Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 删除分账接收方返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_9.shtml </para>
    /// </summary>
    public class DeleteProfitsharingReceiverReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DeleteProfitsharingReceiverReturnJson()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="type">分账接收方类型 <para>枚举值：MERCHANT_ID：商户号PERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：1900000109</para></param>
        public DeleteProfitsharingReceiverReturnJson(string type, string account)
        {
            this.type = type;
            this.account = account;
        }

        /// <summary>
        /// 含参构造函数（服务商模式）
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para><para>示例值：1900000109</para></param>
        /// <param name="type">分账接收方类型 <para>枚举值：MERCHANT_ID：商户号PERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：1900000109</para></param>
        public DeleteProfitsharingReceiverReturnJson(string sub_mchid, string type, string account)
        {
            this.sub_mchid = sub_mchid;
            this.type = type;
            this.account = account;
        }

        /// <summary>
        /// 含参构造函数（服务商模式-连锁品牌）
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="type">分账接收方类型 <para>枚举值：MERCHANT_ID：商户号PERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：1900000109</para></param>
        /// <param name="sub_mchid">子商户号 不需要</param>
        public DeleteProfitsharingReceiverReturnJson(string brand_mchid, string type, string account,string sub_mchid = null)
        {
            this.brand_mchid = brand_mchid;
            this.type = type;
            this.account = account;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式返回
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        #region 品牌连锁
        /// <summary>
        /// 品牌主商户号 
        /// 连锁平台需要
        /// <para>品牌主商户号，填写微信支付分配的商户号。</para>
        /// </summary>
        public string brand_mchid { get; set; }
        #endregion

        /// <summary>
        /// 分账接收方类型
        /// <para>枚举值： MERCHANT_ID：商户号 PERSONAL_OPENID：个人openid（由父商户APPID转换得到） </para>
        /// <para>示例值：MERCHANT_ID</para>
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 分账接收方账号
        /// <para>类型是MERCHANT_ID时，是商户号 类型是PERSONAL_OPENID时，是个人openid</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string account { get; set; }

    }
}
