/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CustomOnlineJson.cs
    文件功能描述：在线客服列表返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
	public class CustomOnlineJson : WxJsonResult
	{
		/// <summary>
		/// 在线客服列表
		/// </summary>
		public List<CustomOnline_Json> kf_online_list { get; set; }
	}

	public class CustomOnline_Json
	{
        /// <summary>
        /// 完整客服帐号，格式为：帐号前缀@公众号微信号
        /// </summary>
        public string kf_account { get; set; }
        /// <summary>
        /// 客服在线状态 1：web 在线
        /// </summary>
        public int status { get; set; }

		/// <summary>
		/// 客服编号
		/// </summary>
		public string kf_id { get; set; }

		/// <summary>
		/// 客服当前正在接待的会话数
		/// </summary>
		public int accepted_case { get; set; }


        /// <summary>
        /// 客服在线状态: 1, Web 在线
        /// </summary>
	    public const int StatusWebOnline = 1;
	}
}