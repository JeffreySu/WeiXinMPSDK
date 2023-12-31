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
  
    文件名：DeleteProfitsharingReceiverRequestData.cs
    文件功能描述：删除分账接收方请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 删除分账接收方请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_9.shtml </para>
    /// </summary>
    public class DeleteProfitsharingReceiverRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DeleteProfitsharingReceiverRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID <para>body微信分配的公众账号ID</para><para>示例值：wx8888888888888888</para></param>
        /// <param name="type">分账接收方类型 <para>body枚举值：MERCHANT_ID：商户IDPERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>body类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：86693852</para></param>
        public DeleteProfitsharingReceiverRequestData( string appid, string type, string account)
        {
            this.appid = appid;
            this.type = type;
            this.account = account;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="sub_appid">子商户应用ID <para>微信分配的子商户公众账号ID，分账接收方类型包含PERSONAL_SUB_OPENID时必填。</para>示例值：wx8888888888888889<para></para></param>
        /// <param name="appid">应用ID <para>body微信分配的公众账号ID</para><para>示例值：wx8888888888888888</para></param>
        /// <param name="type">分账接收方类型 <para>body枚举值：MERCHANT_ID：商户IDPERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>body类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：86693852</para></param>
        public DeleteProfitsharingReceiverRequestData(string sub_mchid, string sub_appid, string appid, string type, string account)
        {
            this.sub_mchid = sub_mchid;
            this.sub_appid = sub_appid;
            this.appid = appid;
            this.type = type;
            this.account = account;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="sub_appid">子商户应用ID <para>微信分配的子商户公众账号ID，分账接收方类型包含PERSONAL_SUB_OPENID时必填。</para>示例值：wx8888888888888889<para></para></param>
        /// <param name="appid">应用ID <para>body微信分配的公众账号ID</para><para>示例值：wx8888888888888888</para></param>
        /// <param name="type">分账接收方类型 <para>body枚举值：MERCHANT_ID：商户IDPERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>body类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：86693852</para></param>
        public DeleteProfitsharingReceiverRequestData(string brand_mchid, string sub_mchid, string sub_appid, string appid, string type, string account)
        {
            this.brand_mchid = brand_mchid;
            this.sub_mchid = sub_mchid;
            this.sub_appid = sub_appid;
            this.appid = appid;
            this.type = type;
            this.account = account;
        }

        #region 品牌连锁
        /// <summary>
        /// 品牌主商户号 
        /// 连锁平台需要
        /// <para>品牌主商户号，填写微信支付分配的商户号。</para>
        /// </summary>
        public string brand_mchid { get; set; }
        #endregion

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 子商户应用ID	 
        /// 微信分配的子商户公众账号ID，分账接收方类型包含PERSONAL_SUB_OPENID时必填。
        /// 服务商模式需要
        /// </summary>
        public string sub_appid { get; set; }
        #endregion

        /// <summary>
        /// 应用ID
        /// <para>body微信分配的公众账号ID</para>
        /// <para>示例值：wx8888888888888888</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 分账接收方类型
        /// <para>body枚举值： MERCHANT_ID：商户ID  PERSONAL_OPENID：个人openid（由父商户APPID转换得到） </para>
        /// <para>示例值：MERCHANT_ID</para>
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 分账接收方账号
        /// <para>body类型是MERCHANT_ID时，是商户号 类型是PERSONAL_OPENID时，是个人openid </para>
        /// <para>示例值：86693852</para>
        /// </summary>
        public string account { get; set; }

    }
}
