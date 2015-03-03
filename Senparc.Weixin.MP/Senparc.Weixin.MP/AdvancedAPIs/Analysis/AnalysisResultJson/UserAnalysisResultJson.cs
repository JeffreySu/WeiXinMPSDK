/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：UserAnalysisResultJson.cs
    文件功能描述：获取用户增减数据返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Analysis
{
    /// <summary>
    /// 获取用户增减数据返回结果
    /// </summary>
    public class UserSummaryResultJson : WxJsonResult
    {
        public List<UserSummary> list { get; set; }
    }

    public class UserSummary
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 用户的渠道，数值代表的含义如下：
        ///0代表其他 30代表扫二维码 17代表名片分享 35代表搜号码（即微信添加朋友页的搜索） 39代表查询微信公众帐号 43代表图文页右上角菜单
        /// </summary>
        public string user_source { get; set; }
        /// <summary>
        /// 新增的用户数量
        /// </summary>
        public string new_user { get; set; }
        /// <summary>
        /// 取消关注的用户数量，new_user减去cancel_user即为净增用户数量
        /// </summary>
        public string cancel_user { get; set; }
    }

    /// <summary>
    /// 获取累计用户数据返回结果
    /// </summary>
    public class UserCumulateResultJson : WxJsonResult
    {
        public List<UserCumulate> list { get; set; }
    }

    public class UserCumulate
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 总用户量
        /// </summary>
        public string cumulate_user { get; set; }
    }
}
