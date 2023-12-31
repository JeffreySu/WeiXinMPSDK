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
  
    文件名：QueryPaygiftActivityMerchantsReturnJson.cs
    文件功能描述：查询活动发券商户号返回Json类
    
    
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
    /// 查询活动发券商户号返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_5.shtml </para>
    /// </summary>
    public class QueryPaygiftActivityMerchantsReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="data">结果集 <para>商户信息列表</para><para>可为null</para></param>
        /// <param name="total_count">总数  <para>商户数量</para><para>示例值：30</para></param>
        /// <param name="offset">分页页码  <para>分页页码</para><para>示例值：4</para></param>
        /// <param name="limit">分页大小  <para>限制分页最大数据条目。</para><para>示例值：20</para></param>
        /// <param name="activity_id">活动id  <para>活动id</para><para>示例值：126002309</para></param>
        public QueryPaygiftActivityMerchantsReturnJson(Data[] data, ulong total_count, ulong offset, ulong limit, string activity_id)
        {
            this.data = data;
            this.total_count = total_count;
            this.offset = offset;
            this.limit = limit;
            this.activity_id = activity_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryPaygiftActivityMerchantsReturnJson()
        {
        }

        /// <summary>
        /// 结果集
        /// <para>商户信息列表 </para>
        /// <para>可为null</para>
        /// </summary>
        public Data[] data { get; set; }

        /// <summary>
        /// 总数 
        /// <para>商户数量 </para>
        /// <para>示例值：30 </para>
        /// </summary>
        public ulong total_count { get; set; }

        /// <summary>
        /// 分页页码 
        /// <para>分页页码 </para>
        /// <para>示例值：4</para>
        /// </summary>
        public ulong offset { get; set; }

        /// <summary>
        /// 分页大小 
        /// <para>限制分页最大数据条目。 </para>
        /// <para>示例值：20 </para>
        /// </summary>
        public ulong limit { get; set; }

        /// <summary>
        /// 活动id 
        /// <para>活动id </para>
        /// <para>示例值：126002309 </para>
        /// </summary>
        public string activity_id { get; set; }

        #region 子数据类型
        public class Data
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="mchid">商户号  <para>商户号</para><para>示例值：6002309</para></param>
            /// <param name="merchant_name">商户名称  <para>商户名称</para><para>示例值：良品铺子</para><para>可为null</para></param>
            /// <param name="create_time">创建时间  <para>创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para><para>可为null</para></param>
            /// <param name="update_time">更新时间  <para>更新时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para><para>可为null</para></param>
            public Data(string mchid, string merchant_name, string create_time, string update_time)
            {
                this.mchid = mchid;
                this.merchant_name = merchant_name;
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
            /// 商户号 
            /// <para>商户号 </para>
            /// <para>示例值：6002309 </para>
            /// </summary>
            public string mchid { get; set; }

            /// <summary>
            /// 商户名称 
            /// <para>商户名称 </para>
            /// <para>示例值：良品铺子</para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_name { get; set; }

            /// <summary>
            /// 创建时间 
            /// <para>创建时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
            /// <para>可为null</para>
            /// </summary>
            public string create_time { get; set; }

            /// <summary>
            /// 更新时间 
            /// <para>更新时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 </para>
            /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
            /// <para>可为null</para>
            /// </summary>
            public string update_time { get; set; }

        }


        #endregion
    }


}


