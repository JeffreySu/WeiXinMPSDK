using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
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
		/// 客服账号
		/// </summary>
		public string kf_account { get; set; }
		/// <summary>
		/// 客服在线状态 1：pc在线，2：手机在线 若pc和手机同时在线则为 1+2=3
		/// </summary>
		public int status { get; set; }

		/// <summary>
		/// 客服工号
		/// </summary>
		public int kf_id { get; set; }

		/// <summary>
		/// 客服设置的最大自动接入数
		/// </summary>
		public int auto_accept { get; set; }

		/// <summary>
		/// 客服当前正在接待的会话数
		/// </summary>
		public int accepted_case { get; set; }
	}
}