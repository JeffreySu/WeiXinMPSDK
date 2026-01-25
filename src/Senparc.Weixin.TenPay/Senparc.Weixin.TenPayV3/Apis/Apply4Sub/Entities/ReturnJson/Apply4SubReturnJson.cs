#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc
  
    文件名：Apply4SubReturnJson.cs
    文件功能描述：服务商进件 - API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Sub
{
    /// <summary>
    /// 服务商进件 - 提交申请单API 返回信息
    /// </summary>
    public class Apply4SubApplymentReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 申请单号
        /// <para>微信支付分配的申请单号</para>
        /// <para>示例值：2000002124775691</para>
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }
    }

    /// <summary>
    /// 服务商进件 - 查询申请状态API 返回信息
    /// </summary>
    public class QueryApply4SubApplymentReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 申请单号
        /// <para>微信支付分配的申请单号</para>
        /// <para>示例值：2000002124775691</para>
        /// </summary>
        public string applyment_id { get; set; }

        /// <summary>
        /// 业务申请编号
        /// <para>服务商自定义的商户唯一编号</para>
        /// <para>示例值：APPLYMENT_00000000001</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 申请状态
        /// <para>CHECKING：资料校验中；ACCOUNT_NEED_VERIFY：待账户验证；AUDITING：审核中；REJECTED：已驳回；NEED_SIGN：待签约；FINISH：完成</para>
        /// <para>示例值：FINISH</para>
        /// </summary>
        public string applyment_state { get; set; }

        /// <summary>
        /// 申请状态描述
        /// <para>申请状态描述</para>
        /// <para>示例值：申请已通过</para>
        /// </summary>
        public string applyment_state_msg { get; set; }

        /// <summary>
        /// 签约链接
        /// <para>当申请状态为NEED_SIGN时，返回签约链接</para>
        /// <para>示例值：https://pay.weixin.qq.com/apply/applyment_detail/applyment_id</para>
        /// </summary>
        public string sign_url { get; set; }

        /// <summary>
        /// 汇款账户验证信息
        /// <para>当申请状态为ACCOUNT_NEED_VERIFY时，返回汇款验证信息</para>
        /// </summary>
        public Apply4SubAccountVerificationInfo account_validation { get; set; }

        /// <summary>
        /// 驳回原因详情
        /// <para>各项资料的审核情况</para>
        /// </summary>
        public Apply4SubAuditDetail[] audit_detail { get; set; }

        /// <summary>
        /// 法人验证链接
        /// <para>法人验证链接</para>
        /// <para>示例值：https://pay.weixin.qq.com/apply/applyment_detail/legal_validation_url</para>
        /// </summary>
        public string legal_validation_url { get; set; }

        /// <summary>
        /// 商户号
        /// <para>申请成功后分配的商户号</para>
        /// <para>示例值：1900013511</para>
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 账户验证信息
    /// </summary>
    public class Apply4SubAccountVerificationInfo
    {
        /// <summary>
        /// 汇款金额
        /// <para>汇款金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int account_validation_amount { get; set; }

        /// <summary>
        /// 汇款账户名称
        /// <para>汇款账户名称</para>
        /// <para>示例值：腾讯科技有限公司</para>
        /// </summary>
        public string account_name { get; set; }

        /// <summary>
        /// 汇款备注
        /// <para>汇款备注</para>
        /// <para>示例值：微信支付-商户号123456789</para>
        /// </summary>
        public string destination_account_remark { get; set; }

        /// <summary>
        /// 汇款截止时间
        /// <para>汇款截止时间</para>
        /// <para>示例值：2018-12-01T12:00:00+08:00</para>
        /// </summary>
        public DateTime destination_account_deadline { get; set; }
    }

    /// <summary>
    /// 审核详情
    /// </summary>
    public class Apply4SubAuditDetail
    {
        /// <summary>
        /// 字段名
        /// <para>提交申请单的资料项名称</para>
        /// <para>示例值：id_card_copy</para>
        /// </summary>
        public string field { get; set; }

        /// <summary>
        /// 字段名称
        /// <para>提交申请单的资料项中文名称</para>
        /// <para>示例值：身份证人像面照片</para>
        /// </summary>
        public string field_name { get; set; }

        /// <summary>
        /// 驳回原因
        /// <para>驳回原因</para>
        /// <para>示例值：证件照片不清晰</para>
        /// </summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 服务商进件 - 修改结算账号API 返回信息
    /// </summary>
    public class ModifyApply4SubSettlementReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户号
        /// <para>特约商户号</para>
        /// <para>示例值：1900013511</para>
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 服务商进件 - 查询结算账号API 返回信息
    /// </summary>
    public class QueryApply4SubSettlementReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 商户号
        /// <para>特约商户号</para>
        /// <para>示例值：1900013511</para>
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 账户信息
        /// <para>结算账户信息</para>
        /// </summary>
        public Apply4SubAccountInfo account_info { get; set; }
    }
}



