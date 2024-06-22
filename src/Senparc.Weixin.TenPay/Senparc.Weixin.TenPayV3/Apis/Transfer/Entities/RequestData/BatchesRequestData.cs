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
  
    文件名：BatchesRequestData.cs
    文件功能描述：发起商家转账API 请求数据
    
    
    创建标识：Senparc - 20220629

    修改标识：Guili95 - 20240623
    修改描述：v1.4.0 添加：微信支付-发起商家转账入参添加转账场景ID、通知地址；返回结果添加批次状态

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Transfer
{
    /// <summary>
    /// 发起商家转账API 请求数据
    /// </summary>
    public class BatchesRequestData
    {
        /// <summary>
        /// 直连商户的appid	
        /// <para>申请商户号的appid或商户号绑定的appid（企业号corpid即为此appid）。示例值：wxf636efh567hg4356</para>
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商家批次单号
        /// <para>商户系统内部的商家批次单号，要求此参数只能由数字、大小写字母组成，在商户系统内部唯一。示例值：plfk2020042013</para>
        /// </summary>
        public string out_batch_no { get; set; }
        /// <summary>
        /// 批次名称
        /// <para>商户系统内部的商家批次单号，要求此参数只能由数字、大小写字母组成，在商户系统内部唯一。示例值：plfk2020042013</para>
        /// </summary>
        public string batch_name { get; set; }
        /// <summary>
        /// 批次备注
        /// <para>转账说明，UTF8编码，最多允许32个字符。示例值：2019年1月深圳分部报销单</para>
        /// </summary>
        public string batch_remark { get; set; }
        /// <summary>
        /// 转账总金额
        /// <para>转账金额单位为“分”。转账总金额必须与批次内所有明细转账金额之和保持一致，否则无法发起转账操作。示例值：4000000</para>
        /// </summary>
        public int total_amount { get; set; }
        /// <summary>
        /// 转账总笔数
        /// <para>一个转账批次单最多发起三千笔转账。转账总笔数必须与批次内所有明细之和保持一致，否则无法发起转账操作。示例值：200</para>
        /// </summary>
        public int total_num { get; set; }
        /// <summary>
        /// 转账明细列表
        /// <para>发起批量转账的明细列表，最多三千笔</para>
        /// </summary>
        public Transfer_Detail_List[] transfer_detail_list { get; set; }
        /// <summary>
        /// 转账场景ID
        /// <para>该批次转账使用的转账场景，如不填写则使用商家的默认场景，如无默认场景可为空，可前往“商家转账到零钱-前往功能”中申请。示例值：1001-现金营销</para>
        /// </summary>
        public string transfer_scene_id { get; set; }
        /// <summary>
        /// 通知地址
        /// <para>异步接收微信支付结果通知的回调地址，通知url必须为公网可访问的url，必须为https，不能携带参数</para>
        /// </summary>
        public string notify_url { get; set; }
    }

    public class Transfer_Detail_List
    {
        /// <summary>
        /// 商家明细单号
        /// <para>商户系统内部区分转账批次单下不同转账明细单的唯一标识，要求此参数只能由数字、大小写字母组成。示例值：x23zy545Bd5436</para>
        /// </summary>
        public string out_detail_no { get; set; }
        /// <summary>
        /// 转账金额
        /// <para>转账金额单位为分。示例值：200000</para>
        /// </summary>
        public int transfer_amount { get; set; }
        /// <summary>
        /// 转账备注
        /// <para>单条转账备注（微信用户会收到该备注），UTF8编码，最多允许32个字符。示例值：2020年4月报销</para>
        /// </summary>
        public string transfer_remark { get; set; }
        /// <summary>
        /// 用户在直连商户应用下的用户标示	
        /// <para>openid是微信用户在公众号appid下的唯一用户标识（appid不同，则获取到的openid就不同），可用于永久标记一个用户。</para>
        /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/terms_definition/chapter1_1_3.shtml">获取openid</see></para>
        /// <para>示例值：o-MYE42l80oelYMDE34nYD456Xoy</para>
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 收款用户姓名
        /// <para>1、明细转账金额 >= 2,000，收款用户姓名必填；</para>
        /// <para>2、同一批次转账明细中，收款用户姓名字段需全部填写、或全部不填写；</para>
        /// <para>3、 若传入收款用户姓名，微信支付会校验用户openID与姓名是否一致，并提供电子回单；</para>
        /// <para>4、收款方姓名。采用标准RSA算法，公钥由微信侧提供</para>
        /// <para>5、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)</para>
        /// <para>6、商户需确保收集用户的姓名信息，以及向微信支付传输用户姓名和账号标识信息做一致性校验已合法征得用户授权</para>
        /// <para>示例值：757b340b45ebef5467rter35gf464344v3542sdf4t6re4tb4f54ty45t4yyry45</para>
        /// </summary>
        public string user_name { get; set; }
    }

}
