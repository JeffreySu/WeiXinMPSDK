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
  
    文件名：QueryNegotiationHistorysReturnJson.cs
    文件功能描述：查询投诉协商历史返回Json类
    
    
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
    /// 查询投诉协商历史返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_12.shtml </para>
    /// </summary>
    public class QueryNegotiationHistorysReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="data">投诉协商历史 <para>投诉协商历史</para><para>可为null</para></param>
        /// <param name="limit">分页大小 <para>设置该次请求返回的最大协商历史条数，范围[1,300]。</para></param>
        /// <param name="offset">分页开始位置 <para>设置该次请求返回的最大协商历史条数，范围[1,300]</para><para>示例值：50</para></param>
        /// <param name="total_count">投诉协商历史总条数 <para>投诉协商历史总条数，当offset=0时返回</para><para>示例值：1000</para><para>可为null</para></param>
        public QueryNegotiationHistorysReturnJson(Data[] data, int limit, int offset, int total_count)
        {
            this.data = data;
            this.limit = limit;
            this.offset = offset;
            this.total_count = total_count;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryNegotiationHistorysReturnJson()
        {
        }

        /// <summary>
        /// 投诉协商历史
        /// <para>投诉协商历史 </para>
        /// <para>可为null</para>
        /// </summary>
        public Data[] data { get; set; }

        /// <summary>
        /// 分页大小
        /// <para>设置该次请求返回的最大协商历史条数，范围[1,300]。</para>
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 分页开始位置
        /// <para>设置该次请求返回的最大协商历史条数，范围[1,300] </para>
        /// <para>示例值：50</para>
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 投诉协商历史总条数
        /// <para>投诉协商历史总条数，当offset=0时返回 </para>
        /// <para>示例值：1000</para>
        /// <para>可为null</para>
        /// </summary>
        public int total_count { get; set; }

        #region 子数据类型
        public class Data
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="complaint_media_list">投诉资料列表 <para>用户、商户、微信支付客服对投诉单执行操作时，上传的投诉相关资料，包括图片凭证等</para><para>可为null</para></param>
            /// <param name="log_id">操作流水号 <para>操作流水号</para><para>示例值：300285320210322170000071077</para></param>
            /// <param name="operator">操作人 <para>当前投诉协商记录的操作人</para><para>示例值：投诉人</para></param>
            /// <param name="operate_time">操作时间 <para>当前投诉协商记录的操作时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
            /// <param name="operate_type">操作类型 <para>当前投诉协商记录的操作类型，对应枚举：USER_CREATE_COMPLAINT：用户提交投诉USER_CONTINUE_COMPLAINT：用户继续投诉USER_RESPONSE：用户留言PLATFORM_RESPONSE：平台留言MERCHANT_RESPONSE：商户留言MERCHANT_CONFIRM_COMPLETE：商户申请结单COMPLAINT_FULL_REFUNDED：投诉单全额退款USER_CREATE_COMPLAINT_SYSTEM_MESSAGE：用户提交投诉系统通知COMPLAINT_FULL_REFUNDED_SYSTEM_MESSAGE：投诉单全额退款系统通知USER_CONTINUE_COMPLAINT_SYSTEM_MESSAGE：用户继续投诉系统通知MERCHANT_CONFIRM_COMPLETE_SYSTEM_MESSAGE：商户申请结单系统通知USER_REVOKE_COMPLAINT：用户主动撤诉（只存在于历史投诉单的协商历史中）</para><para>示例值：USER_CREATE_COMPLAINT</para></param>
            /// <param name="operate_details">操作内容 <para>当前投诉协商记录的具体内容</para><para>示例值：已与用户电话沟通解决</para><para>可为null</para></param>
            /// <param name="image_list">图片凭证 <para>当前投诉协商记录提交的图片凭证（url格式），最多返回4张图片，url有效时间为1小时。如未查询到协商历史图片凭证，则返回空数组。</para><para>注：本字段包含商户、微信支付客服在协商解决投诉时上传的图片凭证，若希望查看用户图片，请使用complaint_media_list字段并联系微信支付客服</para><para>示例值：https://qpic.cn/xxx</para><para>可为null</para></param>
            public Data(Complaint_Media_List complaint_media_list, string log_id, string @operator, string operate_time, string operate_type, string operate_details, string[] image_list)
            {
                this.complaint_media_list = complaint_media_list;
                this.log_id = log_id;
                this.@operator = @operator;
                this.operate_time = operate_time;
                this.operate_type = operate_type;
                this.operate_details = operate_details;
                this.image_list = image_list;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Data()
            {
            }

            /// <summary>
            /// 投诉资料列表
            /// <para>用户、商户、微信支付客服对投诉单执行操作时，上传的投诉相关资料，包括图片凭证等</para>
            /// <para>可为null</para>
            /// </summary>
            public Complaint_Media_List complaint_media_list { get; set; }

            /// <summary>
            /// 操作流水号
            /// <para>操作流水号 </para>
            /// <para>示例值：300285320210322170000071077</para>
            /// </summary>
            public string log_id { get; set; }

            /// <summary>
            /// 操作人
            /// <para>当前投诉协商记录的操作人 </para>
            /// <para>示例值：投诉人</para>
            /// </summary>
            public string @operator { get; set; }

            /// <summary>
            /// 操作时间
            /// <para>当前投诉协商记录的操作时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示北京时间2015年05月20日13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
            /// </summary>
            public string operate_time { get; set; }

            /// <summary>
            /// 操作类型
            /// <para>当前投诉协商记录的操作类型，对应枚举： USER_CREATE_COMPLAINT：用户提交投诉USER_CONTINUE_COMPLAINT：用户继续投诉USER_RESPONSE：用户留言PLATFORM_RESPONSE：平台留言MERCHANT_RESPONSE：商户留言MERCHANT_CONFIRM_COMPLETE：商户申请结单COMPLAINT_FULL_REFUNDED：投诉单全额退款USER_CREATE_COMPLAINT_SYSTEM_MESSAGE：用户提交投诉系统通知COMPLAINT_FULL_REFUNDED_SYSTEM_MESSAGE：投诉单全额退款系统通知USER_CONTINUE_COMPLAINT_SYSTEM_MESSAGE：用户继续投诉系统通知MERCHANT_CONFIRM_COMPLETE_SYSTEM_MESSAGE：商户申请结单系统通知USER_REVOKE_COMPLAINT：用户主动撤诉（只存在于历史投诉单的协商历史中）</para>
            /// <para>示例值：USER_CREATE_COMPLAINT</para>
            /// </summary>
            public string operate_type { get; set; }

            /// <summary>
            /// 操作内容
            /// <para>当前投诉协商记录的具体内容 </para>
            /// <para>示例值：已与用户电话沟通解决</para>
            /// <para>可为null</para>
            /// </summary>
            public string operate_details { get; set; }

            /// <summary>
            /// 图片凭证
            /// <para>当前投诉协商记录提交的图片凭证（url格式），最多返回4张图片，url有效时间为1小时。如未查询到协商历史图片凭证，则返回空数组。 </para>
            /// <para>注：本字段包含商户、微信支付客服在协商解决投诉时上传的图片凭证，若希望查看用户图片，请使用complaint_media_list字段并联系微信支付客服</para>
            /// <para>示例值：https://qpic.cn/xxx</para>
            /// <para>可为null</para>
            /// </summary>
            public string[] image_list { get; set; }

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


            #endregion
        }


        #endregion
    }
}

