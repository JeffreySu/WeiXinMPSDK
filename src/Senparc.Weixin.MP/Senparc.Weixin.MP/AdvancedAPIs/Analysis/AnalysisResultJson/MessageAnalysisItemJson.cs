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
    
    文件名：MessageAnalysisItemJson.cs
    文件功能描述：获取消息发送概况数据返回结果 单条数据类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150310
    修改描述：修改类
----------------------------------------------------------------*/



namespace Senparc.Weixin.MP.AdvancedAPIs.Analysis
{
    public class BaseUpStreamMsgResult : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期，需在begin_date和end_date之间
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 消息类型，代表含义如下：1代表文字 2代表图片 3代表语音 4代表视频 6代表第三方应用消息（链接消息）        
        /// </summary>
        public int msg_type { get; set; }
        /// <summary>
        /// 上行发送了（向公众号发送了）消息的用户数
        /// </summary>
        public int msg_user { get; set; }
        /// <summary>
        /// 上行发送了消息的消息总数
        /// </summary>
        public int msg_count { get; set; }
    }

    /// <summary>
    /// 消息发送概况数据 单条数据
    /// </summary>
    public class UpStreamMsgItem : BaseUpStreamMsgResult
    {

    }

    /// <summary>
    /// 消息分送分时数据 单条数据
    /// </summary>
    public class UpStreamMsgHourItem : BaseUpStreamMsgResult
    {
        /// <summary>
        /// 数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>
        public int ref_hour { get; set; }
    }

    /// <summary>
    /// 消息发送周数据 单条数据
    /// </summary>
    public class UpStreamMsgWeekItem : BaseUpStreamMsgResult
    {

    }

    /// <summary>
    /// 消息发送月数据 单条数据
    /// </summary>
    public class UpStreamMsgMonthItem : BaseUpStreamMsgResult
    {

    }

    public class BaseUpStreamMsgDist : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期，需在begin_date和end_date之间
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 当日发送消息量分布的区间，0代表 “0”，1代表“1-5”，2代表“6-10”，3代表“10次以上”
        /// </summary>
        public int count_interval { get; set; }
        /// <summary>
        /// 上行发送了（向公众号发送了）消息的用户数
        /// </summary>
        public int msg_user { get; set; }
    }

    /// <summary>
    /// 消息发送分布数据 单条数据
    /// </summary>
    public class UpStreamMsgDistItem : BaseUpStreamMsgDist
    {

    }

    /// <summary>
    /// 消息发送分布周数据 单条数据
    /// </summary>
    public class UpStreamMsgDistWeekItem : BaseUpStreamMsgDist
    {

    }

    /// <summary>
    /// 消息发送分布月数据 单条数据
    /// </summary>
    public class UpStreamMsgDistMonthItem : BaseUpStreamMsgDist
    {

    }
}
