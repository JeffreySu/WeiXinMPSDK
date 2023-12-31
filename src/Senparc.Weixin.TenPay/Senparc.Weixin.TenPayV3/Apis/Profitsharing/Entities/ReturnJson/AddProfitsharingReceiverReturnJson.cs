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
  
    文件名：AddProfitsharingReceiverReturnJson.cs
    文件功能描述：添加分账接收方Json类
    
    
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
    /// 添加分账接收方Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_8.shtml </para>
    /// </summary>
    public class AddProfitsharingReceiverReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AddProfitsharingReceiverReturnJson()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="type">分账接收方类型 <para>枚举值：MERCHANT_ID：商户IDPERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：86693852</para></param>
        /// <param name="name">分账个人接收方姓名 <para>分账接收方类型是MERCHANT_ID时，是商户全称（必传），当商户是小微商户或个体户时，是开户人姓名分账接收方类型是PERSONAL_OPENID时，是个人姓名（选传，传则校验）1、此字段需要加密，加密方法详见：敏感信息加密说明2、使用微信支付平台证书中的公钥3、使用RSAES-OAEP算法进行加密4、将请求中HTTP头部的Wechatpay-Serial设置为证书序列号</para><para>示例值：hu89ohu89ohu89o</para><para>可为null</para></param>
        /// <param name="relation_type">与分账方的关系类型 <para>商户与接收方的关系。本字段值为枚举：STORE：门店STAFF：员工STORE_OWNER：店主PARTNER：合作伙伴HEADQUARTER：总部BRAND：品牌方DISTRIBUTOR：分销商USER：用户SUPPLIER：供应商CUSTOM：自定义</para><para>示例值：STORE</para></param>
        /// <param name="custom_relation">自定义的分账关系 <para>子商户与接收方具体的关系，本字段最多10个字。当字段relation_type的值为CUSTOM时，本字段必填;当字段relation_type的值不为CUSTOM时，本字段无需填写。</para><para>示例值：代理商</para><para>可为null</para></param>
        public AddProfitsharingReceiverReturnJson(string type, string account, string name, string relation_type, string custom_relation)
        {
            this.type = type;
            this.account = account;
            this.name = name;
            this.relation_type = relation_type;
            this.custom_relation = custom_relation;
        }

        /// <summary>
        /// 含参构造函数（服务商模式）
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para><para>示例值：1900000109</para></param>
        /// <param name="type">分账接收方类型 <para>枚举值：MERCHANT_ID：商户IDPERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：86693852</para></param>
        /// <param name="name">分账个人接收方姓名 <para>分账接收方类型是MERCHANT_ID时，是商户全称（必传），当商户是小微商户或个体户时，是开户人姓名分账接收方类型是PERSONAL_OPENID时，是个人姓名（选传，传则校验）1、此字段需要加密，加密方法详见：敏感信息加密说明2、使用微信支付平台证书中的公钥3、使用RSAES-OAEP算法进行加密4、将请求中HTTP头部的Wechatpay-Serial设置为证书序列号</para><para>示例值：hu89ohu89ohu89o</para><para>可为null</para></param>
        /// <param name="relation_type">与分账方的关系类型 <para>商户与接收方的关系。本字段值为枚举：STORE：门店STAFF：员工STORE_OWNER：店主PARTNER：合作伙伴HEADQUARTER：总部BRAND：品牌方DISTRIBUTOR：分销商USER：用户SUPPLIER：供应商CUSTOM：自定义</para><para>示例值：STORE</para></param>
        /// <param name="custom_relation">自定义的分账关系 <para>子商户与接收方具体的关系，本字段最多10个字。当字段relation_type的值为CUSTOM时，本字段必填;当字段relation_type的值不为CUSTOM时，本字段无需填写。</para><para>示例值：代理商</para><para>可为null</para></param>
        public AddProfitsharingReceiverReturnJson(string sub_mchid, string type, string account, string name, string relation_type, string custom_relation)
        {
            this.sub_mchid = sub_mchid;
            this.type = type;
            this.account = account;
            this.name = name;
            this.relation_type = relation_type;
            this.custom_relation = custom_relation;
        }

        /// <summary>
        /// 含参构造函数（服务商模式-连锁品牌）
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="type">分账接收方类型 <para>枚举值：MERCHANT_ID：商户IDPERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
        /// <param name="account">分账接收方账号 <para>类型是MERCHANT_ID时，是商户号类型是PERSONAL_OPENID时，是个人openid</para><para>示例值：86693852</para></param>
        /// <param name="name">分账个人接收方姓名 <para>分账接收方类型是MERCHANT_ID时，是商户全称（必传），当商户是小微商户或个体户时，是开户人姓名分账接收方类型是PERSONAL_OPENID时，是个人姓名（选传，传则校验）1、此字段需要加密，加密方法详见：敏感信息加密说明2、使用微信支付平台证书中的公钥3、使用RSAES-OAEP算法进行加密4、将请求中HTTP头部的Wechatpay-Serial设置为证书序列号</para><para>示例值：hu89ohu89ohu89o</para><para>可为null</para></param>
        /// <param name="relation_type">与分账方的关系类型 <para>商户与接收方的关系。本字段值为枚举：STORE：门店STAFF：员工STORE_OWNER：店主PARTNER：合作伙伴HEADQUARTER：总部BRAND：品牌方DISTRIBUTOR：分销商USER：用户SUPPLIER：供应商CUSTOM：自定义</para><para>示例值：STORE</para></param>
        /// <param name="custom_relation">自定义的分账关系 <para>子商户与接收方具体的关系，本字段最多10个字。当字段relation_type的值为CUSTOM时，本字段必填;当字段relation_type的值不为CUSTOM时，本字段无需填写。</para><para>示例值：代理商</para><para>可为null</para></param>
        /// <param name="sub_mchid">子商户号 不需要</param>
        public AddProfitsharingReceiverReturnJson(string brand_mchid, string type, string account, string name, string relation_type, string custom_relation, string sub_mchid = null)
        {
            this.brand_mchid = brand_mchid;
            this.type = type;
            this.account = account;
            this.name = name;
            this.relation_type = relation_type;
            this.custom_relation = custom_relation;
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
        /// <para>枚举值： MERCHANT_ID：商户ID PERSONAL_OPENID：个人openid（由父商户APPID转换得到） </para>
        /// <para>示例值：MERCHANT_ID</para>
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 分账接收方账号
        /// <para>类型是MERCHANT_ID时，是商户号 类型是PERSONAL_OPENID时，是个人openid</para>
        /// <para>示例值：86693852</para>
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 分账个人接收方姓名
        /// <para>分账接收方类型是MERCHANT_ID时，是商户全称（必传），当商户是小微商户或个体户时，是开户人姓名 分账接收方类型是PERSONAL_OPENID时，是个人姓名（选传，传则校验） 1、此字段需要加密，加密方法详见：敏感信息加密说明 2、使用微信支付平台证书中的公钥 3、使用RSAES-OAEP算法进行加密 4、将请求中HTTP头部的Wechatpay-Serial设置为证书序列号</para>
        /// <para>示例值：hu89ohu89ohu89o</para>
        /// <para>可为null</para>
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 与分账方的关系类型
        /// <para>商户与接收方的关系。本字段值为枚举： STORE：门店 STAFF：员工 STORE_OWNER：店主PARTNER：合作伙伴 HEADQUARTER：总部 BRAND：品牌方 DISTRIBUTOR：分销商USER：用户 SUPPLIER： 供应商CUSTOM：自定义</para>
        /// <para>示例值：STORE</para>
        /// </summary>
        public string relation_type { get; set; }

        /// <summary>
        /// 自定义的分账关系
        /// <para>子商户与接收方具体的关系，本字段最多10个字。 当字段relation_type的值为CUSTOM时，本字段必填; 当字段relation_type的值不为CUSTOM时，本字段无需填写。</para>
        /// <para>示例值：代理商</para>
        /// <para>可为null</para>
        /// </summary>
        public string custom_relation { get; set; }

    }


}
