#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：QueryTransferReturnJson.cs
    文件功能描述：商家转账 - 查询转账单API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 查询转账单API 返回信息
    /// <para>适用于商户单号查询和微信单号查询接口</para>
    /// </summary>
    public class QueryTransferReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 微信转账单号
        /// <para>微信转账单号，微信商家转账系统返回的唯一标识</para>
        /// <para>示例值：1330000071100999991182020050700019480001</para>
        /// </summary>
        public string transfer_bill_no { get; set; }

        /// <summary>
        /// 商户单号
        /// <para>商户系统内部的商家单号，要求此参数只能由数字、大小写字母组成，在商户系统内部唯一</para>
        /// <para>示例值：plfk2020042013</para>
        /// </summary>
        public string out_bill_no { get; set; }

        /// <summary>
        /// 商户AppID
        /// <para>申请商户号的appid或商户号绑定的appid（企业号corpid即为此appid）</para>
        /// <para>示例值：wxf636efh567hg4356</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 转账场景ID
        /// <para>该笔转账使用的转账场景</para>
        /// <para>示例值：1000</para>
        /// </summary>
        public string transfer_scene_id { get; set; }

        /// <summary>
        /// 收款用户OpenID
        /// <para>用户在商户appid下的唯一标识</para>
        /// <para>示例值：o-MYE42l80oelYMDE34nYD456Xoy</para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 收款用户姓名
        /// <para>收款方真实姓名。采用标准RSA算法，公钥由微信侧提供</para>
        /// <para>示例值：757b340b45ebef5467rter35gf464344v3542sdf4t6re4tb4f54ty45t4yyry45</para>
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 转账金额
        /// <para>转账金额单位为"分"</para>
        /// <para>示例值：400000</para>
        /// </summary>
        public int transfer_amount { get; set; }

        /// <summary>
        /// 转账备注
        /// <para>转账备注，用户收款时可见该备注信息</para>
        /// <para>示例值：新会员开通有礼</para>
        /// </summary>
        public string transfer_remark { get; set; }

        /// <summary>
        /// 单据创建时间
        /// <para>单据受理成功时返回，按照使用rfc3339所定义的格式，格式为yyyy-MM-DDThh:mm:ss+TIMEZONE</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 单据状态
        /// <para>商家转账订单状态</para>
        /// <para>可选取值：</para>
        /// <para>ACCEPTED: 转账已受理</para>
        /// <para>PROCESSING: 转账锁定资金中</para>
        /// <para>WAIT_USER_CONFIRM: 待收款用户确认</para>
        /// <para>TRANSFERING: 转账中</para>
        /// <para>SUCCESS: 转账成功</para>
        /// <para>FAIL: 转账失败</para>
        /// <para>CANCELING: 商户撤销请求受理成功，该笔转账正在撤销中</para>
        /// <para>CANCELLED: 转账撤销完成</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 转账失败原因
        /// <para>转账失败时返回的失败原因</para>
        /// <para>示例值：账户余额不足</para>
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// 最后一次单据状态变更时间
        /// <para>按照使用rfc3339所定义的格式，格式为yyyy-MM-DDThh:mm:ss+TIMEZONE</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime update_time { get; set; }
    }
}
