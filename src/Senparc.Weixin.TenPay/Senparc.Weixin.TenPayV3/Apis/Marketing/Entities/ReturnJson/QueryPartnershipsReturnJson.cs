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
  
    文件名：QueryPartnershipsReturnJson.cs
    文件功能描述：查询合作关系返回Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 查询合作关系返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_3.shtml </para>
    /// </summary>
    public class QueryPartnershipsReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="data">合作关系结果集 <para>合作关系查询结果集。如查询结果为0，将在控制流返回错误</para><para>可为null</para></param>
        /// <param name="limit">分页大小  <para>分页大小，最大50。</para><para>示例值：5</para></param>
        /// <param name="offset">分页页码  <para>分页页码，页码从0开始。</para><para>示例值：10</para></param>
        /// <param name="total_count">总数量  <para>总数量，当offset=0时返回。</para><para>示例值：1234</para><para>可为null</para></param>
        public QueryPartnershipsReturnJson(Data[] data, ulong limit, ulong offset, ulong total_count)
        {
            this.data = data;
            this.limit = limit;
            this.offset = offset;
            this.total_count = total_count;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryPartnershipsReturnJson()
        {
        }

        /// <summary>
        /// 合作关系结果集
        /// <para>合作关系查询结果集。如查询结果为0，将在控制流返回错误 </para>
        /// <para>可为null</para>
        /// </summary>
        public Data[] data { get; set; }

        /// <summary>
        /// 分页大小 
        /// <para>分页大小，最大50。 </para>
        /// <para>示例值：5 </para>
        /// </summary>
        public ulong limit { get; set; }

        /// <summary>
        /// 分页页码 
        /// <para>分页页码，页码从0开始。 </para>
        /// <para>示例值：10 </para>
        /// </summary>
        public ulong offset { get; set; }

        /// <summary>
        /// 总数量 
        /// <para>总数量，当offset=0时返回。 </para>
        /// <para>示例值：1234 </para>
        /// <para>可为null</para>
        /// </summary>
        public ulong total_count { get; set; }

        #region 子数据类型
        public class Data
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="partner">合作方信息 <para>合作方相关的信息。</para></param>
            /// <param name="authorized_data">被授权数据 <para>被授权的数据。</para></param>
            /// <param name="build_time">建立合作关系时间  <para>建立合作关系时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
            /// <param name="terminate_time">终止合作关系时间  <para>终止合作关系时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para><para>可为null</para></param>
            /// <param name="create_time">创建时间  <para>创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
            /// <param name="update_time">更新时间  <para>更新时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。</para><para>示例值：2015-05-20T13:29:35.120+08:00</para></param>
            public Data(Partner partner, Authorized_Data authorized_data, string build_time, string terminate_time, string create_time, string update_time)
            {
                this.partner = partner;
                this.authorized_data = authorized_data;
                this.build_time = build_time;
                this.terminate_time = terminate_time;
                this.create_time = create_time;
                this.update_time = update_time;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Data()
            {
            }

            /// <summary>
            /// 合作方信息
            /// <para> 合作方相关的信息。 </para>
            /// </summary>
            public Partner partner { get; set; }

            /// <summary>
            /// 被授权数据
            /// <para>被授权的数据。</para>
            /// </summary>
            public Authorized_Data authorized_data { get; set; }

            /// <summary>
            /// 建立合作关系时间 
            /// <para>建立合作关系时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00 </para>
            /// </summary>
            public string build_time { get; set; }

            /// <summary>
            /// 终止合作关系时间 
            /// <para>终止合作关系时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00 </para>
            /// <para>可为null</para>
            /// </summary>
            public string terminate_time { get; set; }

            /// <summary>
            /// 创建时间 
            /// <para>创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00 </para>
            /// </summary>
            public string create_time { get; set; }

            /// <summary>
            /// 更新时间 
            /// <para>更新时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss.sss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss.sss表示时分秒毫秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35.120+08:00表示，北京时间2015年5月20日 13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35.120+08:00 </para>
            /// </summary>
            public string update_time { get; set; }

            #region 子数据类型
            public class Partner
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="type">合作方类别  <para>合作方类别，枚举值：APPID：合作方为APPIDMERCHANT：合作方为商户</para><para>示例值：APPID</para></param>
                /// <param name="appid">合作方APPID  <para>合作方APPID，合作方类别为APPID时必填。</para><para>示例值：wx4e1916a585d1f4e9</para><para>可为null</para></param>
                /// <param name="merchant_id">合作方商户ID  <para>合作方商户ID，合作方类别为MERCHANT时必填。</para><para>特殊规则：最小字符长度为8</para><para>示例值：2480029552</para><para>可为null</para></param>
                public Partner(string type, string appid, string merchant_id)
                {
                    this.type = type;
                    this.appid = appid;
                    this.merchant_id = merchant_id;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Partner()
                {
                }

                /// <summary>
                /// 合作方类别 
                /// <para>合作方类别，枚举值： APPID：合作方为APPID MERCHANT：合作方为商户 </para>
                /// <para>示例值：APPID </para>
                /// </summary>
                public string type { get; set; }

                /// <summary>
                /// 合作方APPID 
                /// <para>合作方APPID，合作方类别为APPID时必填。 </para>
                /// <para>示例值：wx4e1916a585d1f4e9 </para>
                /// <para>可为null</para>
                /// </summary>
                public string appid { get; set; }

                /// <summary>
                /// 合作方商户ID 
                /// <para>合作方商户ID，合作方类别为MERCHANT时必填。 </para>
                /// <para>特殊规则：最小字符长度为8 </para>
                /// <para>示例值：2480029552 </para>
                /// <para>可为null</para>
                /// </summary>
                public string merchant_id { get; set; }

            }

            public class Authorized_Data
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="business_type">授权业务类别  <para>授权业务类别，枚举值：FAVOR_STOCK：代金券批次BUSIFAVOR_STOCK：商家券批次</para><para>示例值：FAVOR_STOCK</para></param>
                /// <param name="stock_id">授权批次ID  <para>授权批次ID，授权业务类别为商家券批次或代金券批次时，此参数必填。</para><para>示例值：2433405</para><para>可为null</para></param>
                public Authorized_Data(string business_type, string stock_id)
                {
                    this.business_type = business_type;
                    this.stock_id = stock_id;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Authorized_Data()
                {
                }

                /// <summary>
                /// 授权业务类别 
                /// <para>授权业务类别，枚举值： FAVOR_STOCK：代金券批次 BUSIFAVOR_STOCK：商家券批次 </para>
                /// <para>示例值：FAVOR_STOCK </para>
                /// </summary>
                public string business_type { get; set; }

                /// <summary>
                /// 授权批次ID 
                /// <para>授权批次ID，授权业务类别为商家券批次或代金券批次时，此参数必填。 </para>
                /// <para>示例值：2433405 </para>
                /// <para>可为null</para>
                /// </summary>
                public string stock_id { get; set; }

            }


            #endregion
        }


        #endregion
    }




}


