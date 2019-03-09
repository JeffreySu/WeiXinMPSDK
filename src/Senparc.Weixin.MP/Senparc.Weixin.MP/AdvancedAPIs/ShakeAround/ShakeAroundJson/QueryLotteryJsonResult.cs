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
    
    文件名：QueryLotteryJsonResult.cs
    文件功能描述：红包查询的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 红包查询的返回结果
    /// </summary>
   public class QueryLotteryJsonResult : WxJsonResult 
    {
       public QueryLottery_Result result { get; set; }
      

    }
   public class QueryLottery_Result
   {
       /// <summary>
       /// 红包抽奖id，来自addlotteryinfo返回的lottery_id
       /// </summary>
       public string lottery_id { get; set; }
       /// <summary>
       /// 抽奖活动名称（选择使用模板时，也作为摇一摇消息主标题），最长6个汉字，12个英文字母。
       /// </summary>
       public string title { get; set; }
       /// <summary>
       /// 抽奖活动描述（选择使用模板时，也作为摇一摇消息副标题），最长7个汉字，14个英文字母。
       /// </summary>
       public string desc { get; set; }
       /// <summary>
       /// 抽奖开关。0关闭，1开启，默认为1
       /// </summary>
       public int onoff { get; set; }
       /// <summary>
       /// 抽奖活动开始时间，unix时间戳，单位秒
       /// </summary>
       public long begin_time { get; set; }
       /// <summary>
       /// 抽奖活动结束时间，unix时间戳，单位秒，红包活动有效期最长为91天
       /// </summary>
       public long expire_time { get; set; }
       /// <summary>
       /// 红包提供商户公众号的appid
       /// </summary>
       public string sponsor_appid { get; set; }
       /// <summary>
       /// 创建活动的开发者appid
       /// </summary>
       public string appid { get; set; }
       /// <summary>
       /// 创建活动时预设的录入红包ticket数量上限
       /// </summary>
       public long prize_count_limit { get; set; }
       /// <summary>
       /// 已录入的红包总数
       /// </summary>
       public long prize_count { get; set; }
       /// <summary>
       /// 红包关注界面后可以跳转到第三方自定义的页面
       /// </summary>
       public string jump_url { get; set; }
       /// <summary>
       /// 过期红包ticket数量
       /// </summary>
       public long expired_prizes { get; set; }
       /// <summary>
       /// 已发放的红包ticket数量
       /// </summary>
       public long drawed_prizes { get; set; }
       /// <summary>
       /// 可用的红包ticket数量
       /// </summary>
       public long available_prizes { get; set; }
       /// <summary>
       /// 已过期的红包金额总和
       /// </summary>
       public long expired_value { get; set; }
       /// <summary>
       /// 已发放的红包金额总和
       /// </summary>
       public long drawed_value { get; set; }
       /// <summary>
       /// 可用的红包金额总和   
       /// </summary>
       public long available_value { get; set; }
   }
}
