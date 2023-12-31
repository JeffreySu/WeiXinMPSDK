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
  
    文件名：QueryComplaintReturnJson.cs
    文件功能描述：查询投诉单详情返回Json类
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Complaint
{
    /// <summary>
    /// 查询投诉单详情返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_13.shtml </para>
    /// </summary>
    public class QueryComplaintReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="complaint_id">投诉单号 <para>投诉单对应的投诉单号</para><para>示例值：200201820200101080076610000</para></param>
        /// <param name="complaint_time">投诉时间 <para>投诉时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
        /// <param name="complaint_detail">投诉详情 <para>投诉的具体描述</para><para>示例值：反馈一个重复扣费的问题</para></param>
        /// <param name="complainted_mchid">被诉商户号 <para>投诉单对应的被诉商户号。</para><para>示例值：1900012181</para><para>可为null</para></param>
        /// <param name="complaint_state">投诉单状态 <para>标识当前投诉单所处的处理阶段，具体状态如下所示：PENDING：待处理PROCESSING：处理中PROCESSED：已处理完成</para><para>示例值：PENDING</para></param>
        /// <param name="payer_phone">投诉人联系方式 <para>投诉人联系方式。该字段已做加密处理，具体解密方法详见敏感信息加密说明。</para><para>示例值：sGdNeTHMQGlxCWiUyHu6XNO9GCYln2Luv4HhwJzZBfcL12sB</para><para>可为null</para></param>
        /// <param name="payer_openid">投诉人openid <para>投诉人在商户appid下的唯一标识</para><para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para></param>
        /// <param name="complaint_media_list">投诉资料列表 <para>用户上传的投诉相关资料，包括图片凭证等</para></param>
        /// <param name="complaint_order_info">投诉单关联订单信息 <para>投诉单关联订单信息</para><para>注：投诉单和订单目前是一对一关系，array是预留未来一对多的扩展</para></param>
        /// <param name="complaint_full_refunded">投诉单是否已全额退款 <para>投诉单下所有订单是否已全部全额退款</para><para>示例值：true</para></param>
        /// <param name="incoming_user_response">是否有待回复的用户留言 <para>投诉单是否有待回复的用户留言</para><para>示例值：true</para></param>
        /// <param name="problem_description">问题描述 <para>用户发起投诉前选择的faq标题（2021年7月15日之后的投诉单均包含此信息）</para><para>示例值：不满意商家服务</para></param>
        /// <param name="user_complaint_times">用户投诉次数 <para>用户投诉次数。用户首次发起投诉记为1次，用户每有一次继续投诉就加1</para><para>示例值：1</para></param>
        public QueryComplaintReturnJson(string complaint_id, string complaint_time, string complaint_detail, string complainted_mchid, string complaint_state, string payer_phone, string payer_openid, Complaint_Media_List[] complaint_media_list, Complaint_Order_Info[] complaint_order_info, bool complaint_full_refunded, bool incoming_user_response, string problem_description, int user_complaint_times)
        {
            this.complaint_id = complaint_id;
            this.complaint_time = complaint_time;
            this.complaint_detail = complaint_detail;
            this.complainted_mchid = complainted_mchid;
            this.complaint_state = complaint_state;
            this.payer_phone = payer_phone;
            this.payer_openid = payer_openid;
            this.complaint_media_list = complaint_media_list;
            this.complaint_order_info = complaint_order_info;
            this.complaint_full_refunded = complaint_full_refunded;
            this.incoming_user_response = incoming_user_response;
            this.problem_description = problem_description;
            this.user_complaint_times = user_complaint_times;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryComplaintReturnJson()
        {
        }

        /// <summary>
        /// 投诉单号
        /// <para>投诉单对应的投诉单号 </para>
        /// <para>示例值：200201820200101080076610000</para>
        /// </summary>
        public string complaint_id { get; set; }

        /// <summary>
        /// 投诉时间
        /// <para>投诉时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒 </para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public string complaint_time { get; set; }

        /// <summary>
        /// 投诉详情
        /// <para>投诉的具体描述 </para>
        /// <para>示例值：反馈一个重复扣费的问题</para>
        /// </summary>
        public string complaint_detail { get; set; }

        /// <summary>
        /// 被诉商户号
        /// <para>投诉单对应的被诉商户号。 </para>
        /// <para>示例值：1900012181</para>
        /// <para>可为null</para>
        /// </summary>
        public string complainted_mchid { get; set; }

        /// <summary>
        /// 投诉单状态
        /// <para>标识当前投诉单所处的处理阶段，具体状态如下所示： PENDING：待处理 PROCESSING：处理中 PROCESSED：已处理完成 </para>
        /// <para>示例值：PENDING</para>
        /// </summary>
        public string complaint_state { get; set; }

        /// <summary>
        /// 投诉人联系方式
        /// <para>投诉人联系方式。该字段已做加密处理，具体解密方法详见敏感信息加密说明。 </para>
        /// <para>示例值：sGdNeTHMQGlxCWiUyHu6XNO9GCYln2Luv4HhwJzZBfcL12sB</para>
        /// <para>可为null</para>
        /// </summary>
        public string payer_phone { get; set; }

        /// <summary>
        /// 投诉人openid
        /// <para>投诉人在商户appid下的唯一标识 </para>
        /// <para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para>
        /// </summary>
        public string payer_openid { get; set; }

        /// <summary>
        /// 投诉资料列表
        /// <para>用户上传的投诉相关资料，包括图片凭证等</para>
        /// </summary>
        public Complaint_Media_List[] complaint_media_list { get; set; }

        /// <summary>
        /// 投诉单关联订单信息
        /// <para>投诉单关联订单信息 </para>
        /// <para>注：投诉单和订单目前是一对一关系，array是预留未来一对多的扩展</para>
        /// </summary>
        public Complaint_Order_Info[] complaint_order_info { get; set; }

        /// <summary>
        /// 投诉单是否已全额退款
        /// <para>投诉单下所有订单是否已全部全额退款 </para>
        /// <para>示例值：true</para>
        /// </summary>
        public bool complaint_full_refunded { get; set; }

        /// <summary>
        /// 是否有待回复的用户留言
        /// <para>投诉单是否有待回复的用户留言 </para>
        /// <para>示例值：true</para>
        /// </summary>
        public bool incoming_user_response { get; set; }

        /// <summary>
        /// 问题描述
        /// <para>用户发起投诉前选择的faq标题（2021年7月15日之后的投诉单均包含此信息）</para>
        /// <para>示例值：不满意商家服务</para>
        /// </summary>
        public string problem_description { get; set; }

        /// <summary>
        /// 用户投诉次数
        /// <para>用户投诉次数。用户首次发起投诉记为1次，用户每有一次继续投诉就加1 </para>
        /// <para>示例值：1</para>
        /// </summary>
        public int user_complaint_times { get; set; }

        #region 子数据类型
        public class Complaint_Media_List
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="media_type">媒体文件业务类型 <para>媒体文件对应的业务类型USER_COMPLAINT_IMAGE：用户投诉图片，用户提交投诉时上传的图片凭证OPERATION_IMAGE：操作流水图片，用户、商户、微信支付客服在协商解决投诉时，上传的图片凭证</para><para>注：用户上传的图片凭证会以白名单的形式提供给商户，若希望查看用户图片，联系微信支付客服示例值：USER_COMPLAINT_IMAGE</para></param>
            /// <param name="media_url">媒体文件请求url <para>微信返回的媒体文件请求url</para><para>示例值：https://api.mch.weixin.qq.com/v3/merchant-service/images/xxxxx</para></param>
            public Complaint_Media_List(string media_type, string[] media_url)
            {
                this.media_type = media_type;
                this.media_url = media_url;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Complaint_Media_List()
            {
            }

            /// <summary>
            /// 媒体文件业务类型
            /// <para>媒体文件对应的业务类型 USER_COMPLAINT_IMAGE：用户投诉图片，用户提交投诉时上传的图片凭证  OPERATION_IMAGE：操作流水图片，用户、商户、微信支付客服在协商解决投诉时，上传的图片凭证</para>
            /// <para>注：用户上传的图片凭证会以白名单的形式提供给商户，若希望查看用户图片，联系微信支付客服        示例值：USER_COMPLAINT_IMAGE</para>
            /// </summary>
            public string media_type { get; set; }

            /// <summary>
            /// 媒体文件请求url
            /// <para>微信返回的媒体文件请求url</para>
            /// <para>示例值：https://api.mch.weixin.qq.com/v3/merchant-service/images/xxxxx</para>
            /// </summary>
            public string[] media_url { get; set; }

        }

        public class Complaint_Order_Info
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="transaction_id">微信订单号 <para>投诉单关联的微信订单号</para><para>示例值：4200000404201909069117582536</para></param>
            /// <param name="out_trade_no">商户订单号 <para>投诉单关联的商户订单号</para><para>示例值：20190906154617947762231</para></param>
            /// <param name="amount">订单金额 <para>订单金额，单位（分）</para><para>示例值：3</para></param>
            public Complaint_Order_Info(string transaction_id, string out_trade_no, int amount)
            {
                this.transaction_id = transaction_id;
                this.out_trade_no = out_trade_no;
                this.amount = amount;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Complaint_Order_Info()
            {
            }

            /// <summary>
            /// 微信订单号
            /// <para>投诉单关联的微信订单号 </para>
            /// <para>示例值：4200000404201909069117582536</para>
            /// </summary>
            public string transaction_id { get; set; }

            /// <summary>
            /// 商户订单号
            /// <para>投诉单关联的商户订单号 </para>
            /// <para>示例值：20190906154617947762231</para>
            /// </summary>
            public string out_trade_no { get; set; }

            /// <summary>
            /// 订单金额
            /// <para>订单金额，单位（分） </para>
            /// <para>示例值：3</para>
            /// </summary>
            public int amount { get; set; }

        }


        #endregion
    }
}


