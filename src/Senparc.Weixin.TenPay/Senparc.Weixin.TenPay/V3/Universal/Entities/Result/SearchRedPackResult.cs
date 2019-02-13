#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
  
    文件名：SendRedPackResult.cs
    文件功能描述：获取查询红包接口的结果，既可以查询普通红包，也可以查询裂变红包
    
    
    创建标识：Yu XiaoChou - 20160107
    
    修改标识：Senparc - 20161112
    修改描述：RedPackHBInfo去除status属性
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 获取查询红包接口的结果，既可以查询普通红包，也可以查询裂变红包
    /// </summary>
    public class SearchRedPackResult
    {
        /// <summary>
        /// 返回状态码,SUCCESS/FAIL,此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断 
        /// </summary>
        public bool return_code { get; set; }

        /// <summary>
        /// 返回信息，如非空，为错误原因,签名失败,参数格式校验错误 
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 业务结果,SUCCESS/FAIL
        /// </summary>
        public bool result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 商户订单号（每个订单号必须唯一） 组成：mch_id+yyyymmdd+10位一天内不能重复的数字
        /// </summary>
        public string mch_billno { get; set; }

        /// <summary>
        /// 商户号，微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 红包单号
        /// </summary>
        public string detail_id { get; set; }

        /// <summary>
        /// 红包状态，SENDING:发放中，SENT:已发放待领取，FAILED：发放失败，RECEIVED:已领取，REFUND:已退款 
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 发放类型,API:通过API接口发放,UPLOAD:通过上传文件方式发放,ACTIVITY:通过活动方式发放 
        /// </summary>
        public string send_type { get; set; }
        /// <summary>
        /// 红包类型,GROUP:裂变红包,NORMAL:普通红包 
        /// </summary>
        public string hb_type { get; set; }

        /// <summary>
        /// 红包个数
        /// </summary>
        public string total_num { get; set; }
        /// <summary>
        /// 红包总金额（单位分） 
        /// </summary>
        public string total_amount { get; set; }

        /// <summary>
        /// 红包发送时间
        /// </summary>
        public string send_time { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        public string wishing { get; set; }

        /// <summary>
        /// 活动描述，低版本微信可见
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 活动名称（请注意活动名称长度，官方文档提示为32个字符，实际限制不足32个字符）
        /// </summary>
        public string act_name { get; set; }

        /// <summary>
        /// 发送失败原因
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 红包的退款时间（如果其未领取的退款）  
        /// </summary>
        public string refund_time { get; set; }

        /// <summary>
        /// 红包退款金额
        /// </summary>
        public string refund_amount { get; set; }

        /// <summary>
        /// 红包领取列表,普通红包只有一项，列表红包可以有很多项
        /// </summary>
        public ICollection<RedPackHBInfo> hblist { get; set; }

    }

    /// <summary>
    /// 单个OpenID红包领取信息
    /// </summary>
    public class RedPackHBInfo
    {
        /// <summary>
        /// 领取红包的Openid
        /// </summary>
        public string openid { get; set; }

        ///// <summary>
        ///// 红包状态，SENDING:发放中，SENT:已发放待领取，FAILED：发放失败，RECEIVED:已领取，REFUND:已退款 
        ///// </summary>
        //public string status { get; set; }

        /// <summary>
        /// 领取金额
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// 领取时间
        /// </summary>
        public string rcv_time { get; set; }

    }
}
