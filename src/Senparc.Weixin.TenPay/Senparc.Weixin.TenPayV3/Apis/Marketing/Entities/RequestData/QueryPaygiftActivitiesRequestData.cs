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
  
    文件名：QueryPaygiftActivitiesRequestData.cs
    文件功能描述：获取支付有礼活动列表接口请求数据
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 获取支付有礼活动列表接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_9.shtml </para>
    /// </summary>
    public class QueryPaygiftActivitiesRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="offset">分页页码  <para>query分页页码，页面从0开始</para><para>示例值：1</para></param>
        /// <param name="limit">分页大小  <para>query分页大小</para><para>特殊规则：最大取值为100，最小为1</para><para>示例值：20</para></param>
        /// <param name="activity_name">活动名称  <para>query活动名称，支持模糊搜索</para><para>示例值：良品铺子回馈活动</para><para>可为null</para></param>
        /// <param name="activity_status">活动状态  <para>query活动状态，枚举值：ACT_STATUS_UNKNOWN：状态未知CREATE_ACT_STATUS：已创建ONGOING_ACT_STATUS：运行中TERMINATE_ACT_STATUS：已终止STOP_ACT_STATUS：已暂停OVER_TIME_ACT_STATUS：已过期CREATE_ACT_FAILED：创建活动失败</para><para>示例值：CREATE_ACT_STATUS</para><para>可为null</para></param>
        /// <param name="award_type">奖品类型  <para>query奖品类型，暂时只支持商家券BUSIFAVOR：商家券</para><para>示例值：BUSIFAVOR</para><para>可为null</para></param>
        public QueryPaygiftActivitiesRequestData(int offset, int limit, string activity_name, string activity_status, string award_type)
        {
            this.offset = offset;
            this.limit = limit;
            this.activity_name = activity_name;
            this.activity_status = activity_status;
            this.award_type = award_type;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryPaygiftActivitiesRequestData()
        {
        }

        /// <summary>
        /// 分页页码 
        /// <para>query 分页页码，页面从0开始 </para>
        /// <para>示例值：1</para>
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 分页大小 
        /// <para>query 分页大小 </para>
        /// <para>特殊规则：最大取值为100，最小为1 </para>
        /// <para>示例值：20</para>
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 活动名称 
        /// <para>query 活动名称，支持模糊搜索 </para>
        /// <para>示例值：良品铺子回馈活动</para>
        /// <para>可为null</para>
        /// </summary>
        public string activity_name { get; set; }

        /// <summary>
        /// 活动状态 
        /// <para>query 活动状态，枚举值： ACT_STATUS_UNKNOWN：状态未知 CREATE_ACT_STATUS：已创建 ONGOING_ACT_STATUS：运行中 TERMINATE_ACT_STATUS：已终止 STOP_ACT_STATUS：已暂停 OVER_TIME_ACT_STATUS：已过期 CREATE_ACT_FAILED：创建活动失败</para>
        /// <para>示例值：CREATE_ACT_STATUS</para>
        /// <para>可为null</para>
        /// </summary>
        public string activity_status { get; set; }

        /// <summary>
        /// 奖品类型 
        /// <para>query 奖品类型，暂时只支持商家券 BUSIFAVOR：商家券</para>
        /// <para>示例值：BUSIFAVOR</para>
        /// <para>可为null</para>
        /// </summary>
        public string award_type { get; set; }

    }
}
