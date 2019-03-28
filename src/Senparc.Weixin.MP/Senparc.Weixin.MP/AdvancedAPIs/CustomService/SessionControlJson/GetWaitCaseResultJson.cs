/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetWaitCaseResultJson.cs
    文件功能描述：获取未接入会话列表返回结果
    
    
    创建标识：Senparc - 20150306
    
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 获取未接入会话列表返回结果
    /// </summary>
    public class GetWaitCaseResultJson : WxJsonResult
	{
		/// <summary>
        /// 未接入会话数量
		/// </summary>
        public int count { get; set; }

        /// <summary>
        /// 未接入会话列表，最多返回100条数据
        /// </summary>
        public List<SingleWaitCase> waitcaselist { get; set; }
	}

    public class SingleWaitCase
    {
        /// <summary>
        /// 客户openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 指定接待的客服，为空表示未指定客服
        /// </summary>
        public string kf_account { get; set; }

        /// <summary>
        /// 用户来访时间，UNIX时间戳
        /// </summary>
        public string createtime { get; set; }
    }
}