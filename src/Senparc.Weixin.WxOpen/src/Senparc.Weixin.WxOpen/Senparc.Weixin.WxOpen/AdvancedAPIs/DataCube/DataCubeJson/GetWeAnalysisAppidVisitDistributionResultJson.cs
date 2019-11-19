#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：GetWeAnalysisAppidVisitDistributionResultJson.cs
    文件功能描述：小程序“数据分析”接口 - 访问趋势：访问分布 返回结果
    
    
    创建标识：Senparc - 20180101
    
    添加访问分析接口的 access_source_visit_uv 属性（该场景 id 访问 uv）

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.DataCube
{
    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：访问分布 返回结果
    /// </summary>
    public class GetWeAnalysisAppidVisitDistributionResultJson : WxJsonResult
    {
        /// <summary>
        /// 时间： 如： "20170313"
        /// </summary>
        public string ref_date { get; set; }

        public List<GetWeAnalysisAppidVisitDistributionResultJson_list> list { get; set; }
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：访问分布 返回结果 - list
    /// </summary>
    public class GetWeAnalysisAppidVisitDistributionResultJson_list
    {
        /// <summary>
        /// 分布类型
        /// </summary>
        public GetWeAnalysisAppidVisitDistributionResultJson_list_index index { get; set; }

        /// <summary>
        /// 访问次数（自然周内汇总）
        /// </summary>
        public List<GetWeAnalysisAppidVisitDistributionResultJson_list_item_list> item_list { get; set; }

    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：访问分布 返回结果 - list - index 枚举
    /// </summary>
    public enum GetWeAnalysisAppidVisitDistributionResultJson_list_index
    {
        /// <summary>
        /// 访问来源分布
        /// </summary>
        access_source_session_cnt,
        /// <summary>
        /// 访问时长分布
        /// </summary>
        access_staytime_info,
        /// <summary>
        /// 访问深度的分布
        /// </summary>
        access_depth_info
    }

    /// <summary>
    /// 小程序“数据分析”接口 - 访问趋势：访问分布 返回结果 - list - item_list
    /// </summary>
    public class GetWeAnalysisAppidVisitDistributionResultJson_list_item_list
    {
        /// <summary>
        /// 场景 id
        /// </summary>
        public int key { get; set; }
        /// <summary>
        /// value 场景下的值（均为整数型）
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 该场景 id 访问 uv	
        /// </summary>
        public int access_source_visit_uv { get; set; }

        /*
         key对应关系如下：

        访问来源：(index="access_source_session_cnt")

        1：小程序历史列表

        2：搜索

        3：会话

        4：二维码

        5：公众号主页

        6：聊天顶部

        7：系统桌面

        8：小程序主页

        9：附近的小程序

        10：其他

        11：模板消息

        12：客服消息

        13: 公众号菜单

        14: APP分享

        15: 支付完成页

        16: 长按识别二维码

        17: 相册选取二维码

        18: 公众号文章

        19：钱包

        20：卡包

        21：小程序内卡券

        22：其他小程序

        23：其他小程序返回

        24：卡券适用门店列表

        25：搜索框快捷入口

        26：小程序客服消息

        27：公众号下发

        访问时长：(index="access_staytime_info")

        1: 0-2s

        2: 3-5s

        3: 6-10s

        4: 11-20s

        5: 20-30s

        6: 30-50s

        7: 50-100s

        8: > 100s

        平均访问深度：(index="access_depth_info")

        1: 1页

        2: 2页

        3: 3页

        4: 4页

        5: 5页

        6: 6-10页

        7: >10页
    */
    }

}
