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
  
    文件名：CreateProfitsharingRequestData.cs
    文件功能描述：请求分账接口请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Attributes;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 请求分账接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_1.shtml </para>
    /// </summary>
    public class CreateProfitsharingRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateProfitsharingRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID <para>body微信分配的商户appid</para><para>示例值：wx8888888888888888</para></param>
        /// <param name="transaction_id">微信订单号 <para>body微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="receivers">分账接收方列表 <para>body分账接收方列表，可以设置出资商户作为分账接受方，最多可有50个分账接收方</para></param>
        /// <param name="unfreeze_unsplit">是否解冻剩余未分资金 <para>body1、如果为true，该笔订单剩余未分账的金额会解冻回分账方商户；2、如果为false，该笔订单剩余未分账的金额不会解冻回分账方商户，可以对该笔订单再次进行分账。</para><para>示例值：true</para></param>
        public CreateProfitsharingRequestData(string appid, string transaction_id, string out_order_no, Receiver[] receivers, bool unfreeze_unsplit)
        {
            this.appid = appid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.receivers = receivers;
            this.unfreeze_unsplit = unfreeze_unsplit;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="sub_appid">子商户应用ID <para>微信分配的子商户公众账号ID，分账接收方类型包含PERSONAL_SUB_OPENID时必填。</para>示例值：wx8888888888888889<para></para></param>
        /// <param name="appid">应用ID <para>body微信分配的商户appid</para><para>示例值：wx8888888888888888</para></param>
        /// <param name="transaction_id">微信订单号 <para>body微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="receivers">分账接收方列表 <para>body分账接收方列表，可以设置出资商户作为分账接受方，最多可有50个分账接收方</para></param>
        /// <param name="unfreeze_unsplit">是否解冻剩余未分资金 <para>body1、如果为true，该笔订单剩余未分账的金额会解冻回分账方商户；2、如果为false，该笔订单剩余未分账的金额不会解冻回分账方商户，可以对该笔订单再次进行分账。</para><para>示例值：true</para></param>
        public CreateProfitsharingRequestData(string sub_mchid, string sub_appid, string appid, string transaction_id, string out_order_no, Receiver[] receivers, bool unfreeze_unsplit)
        {
            this.sub_mchid = sub_mchid;
            this.sub_appid = sub_appid;
            this.appid = appid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.receivers = receivers;
            this.unfreeze_unsplit = unfreeze_unsplit;
        }

        /// <summary>
        /// 含参构造函数(服务商模式-连锁品牌)
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="sub_appid">子商户应用ID <para>微信分配的子商户公众账号ID，分账接收方类型包含PERSONAL_SUB_OPENID时必填。</para>示例值：wx8888888888888889<para></para></param>
        /// <param name="appid">应用ID <para>body微信分配的商户appid</para><para>示例值：wx8888888888888888</para></param>
        /// <param name="transaction_id">微信订单号 <para>body微信支付订单号</para><para>示例值：4208450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@</para><para>示例值：P20150806125346</para></param>
        /// <param name="receivers">分账接收方列表 <para>body分账接收方列表，可以设置出资商户作为分账接受方，最多可有50个分账接收方</para></param>
        /// <param name="unfreeze_unsplit">是否解冻剩余未分资金 <para>body1、如果为true，该笔订单剩余未分账的金额会解冻回分账方商户；2、如果为false，该笔订单剩余未分账的金额不会解冻回分账方商户，可以对该笔订单再次进行分账。</para><para>示例值：true</para></param>
        public CreateProfitsharingRequestData(string brand_mchid, string sub_mchid, string sub_appid, string appid, string transaction_id, string out_order_no, Receiver[] receivers, bool unfreeze_unsplit)
        {
            this.brand_mchid = brand_mchid;
            this.sub_mchid = sub_mchid;
            this.sub_appid = sub_appid;
            this.appid = appid;
            this.transaction_id = transaction_id;
            this.out_order_no = out_order_no;
            this.receivers = receivers;
            this.unfreeze_unsplit = unfreeze_unsplit;
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
        /// <para>body微信分配的商户appid</para>
        /// <para>示例值：wx8888888888888888</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信订单号
        /// <para>body微信支付订单号</para>
        /// <para>示例值：4208450740201411110007820472</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@ </para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 分账接收方列表
        /// <para>body分账接收方列表，可以设置出资商户作为分账接受方，最多可有50个分账接收方</para>
        /// </summary>
        public Receiver[] receivers { get; set; }

        /// <summary>
        /// 是否解冻剩余未分资金
        /// <para>body1、如果为true，该笔订单剩余未分账的金额会解冻回分账方商户； 2、如果为false，该笔订单剩余未分账的金额不会解冻回分账方商户，可以对该笔订单再次进行分账。</para>
        /// <para>示例值：true</para>
        /// </summary>
        public bool unfreeze_unsplit { get; set; }

        #region 子数据类型
        public class Receiver
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="type">分账接收方类型 <para>1、MERCHANT_ID：商户号2、PERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para><para>示例值：MERCHANT_ID</para></param>
            /// <param name="account">分账接收方账号 <para>1、分账接收方类型为MERCHANT_ID时，分账接收方账号为商户号2、分账接收方类型为PERSONAL_OPENID时，分账接收方账号为个人openid</para><para>示例值：86693852</para></param>
            /// <param name="name">分账个人接收方姓名 <para>可选项，在接收方类型为个人的时可选填，若有值，会检查与name是否实名匹配，不匹配会拒绝分账请求1、分账接收方类型是PERSONAL_OPENID，是个人姓名的密文（选传，传则校验）此字段的加密方法详见：敏感信息加密说明2、使用微信支付平台证书中的公钥3、使用RSAES-OAEP算法进行加密4、将请求中HTTP头部的Wechatpay-Serial设置为证书序列号</para><para>示例值：hu89ohu89ohu89o</para><para>可为null</para></param>
            /// <param name="amount">分账金额 <para>分账金额，单位为分，只能为整数，不能超过原订单支付金额及最大分账比例金额</para><para>示例值：888</para></param>
            /// <param name="description">分账描述 <para>分账的原因描述，分账账单中需要体现</para><para>示例值：分给商户A</para></param>
            public Receiver(string type, string account, string name, int amount, string description)
            {
                this.type = type;
                this.account = account;
                this.name = name;
                this.amount = amount;
                this.description = description;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Receiver()
            {
            }

            /// <summary>
            /// 分账接收方类型
            /// <para>1、MERCHANT_ID：商户号 2、PERSONAL_OPENID：个人openid（由父商户APPID转换得到）</para>
            /// <para>示例值：MERCHANT_ID</para>
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 分账接收方账号
            /// <para>1、分账接收方类型为MERCHANT_ID时，分账接收方账号为商户号2、分账接收方类型为PERSONAL_OPENID时，分账接收方账号为个人openid</para>
            /// <para>示例值：86693852</para>
            /// </summary>
            public string account { get; set; }

            /// <summary>
            /// 分账个人接收方姓名
            /// <para>可选项，在接收方类型为个人的时可选填，若有值，会检查与 name 是否实名匹配，不匹配会拒绝分账请求 1、分账接收方类型是PERSONAL_OPENID，是个人姓名的密文（选传，传则校验） 此字段的加密方法详见：敏感信息加密说明 2、使用微信支付平台证书中的公钥 3、使用RSAES-OAEP算法进行加密 4、将请求中HTTP头部的Wechatpay-Serial设置为证书序列号</para>
            /// <para>示例值：hu89ohu89ohu89o</para>
            /// <para>可为null  方法内部已自动加密，只需要赋值明文即可</para>
            /// </summary>
            [FieldEncrypt]
            public string name { get; set; }

            /// <summary>
            /// 分账金额
            /// <para>分账金额，单位为分，只能为整数，不能超过原订单支付金额及最大分账比例金额</para>
            /// <para>示例值：888</para>
            /// </summary>
            public int amount { get; set; }

            /// <summary>
            /// 分账描述
            /// <para>分账的原因描述，分账账单中需要体现</para>
            /// <para>示例值：分给商户A</para>
            /// </summary>
            public string description { get; set; }

        }


        #endregion
    }


}
