using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
	public class CustomInfoJson : WxJsonResult
	{
		/// <summary>
		/// 客服列表
		/// </summary>
		public List<CustomInfo_Json> kf_list { get; set; }
	}

	public class CustomInfo_Json
	{
		/// <summary>
		/// 客服账号
		/// </summary>
		public string kf_account { get; set; }

		/// <summary>
		/// 客服昵称
		/// </summary>
		public string kf_nick { get; set; }

		/// <summary>
		/// 客服工号
		/// </summary>
		public int kf_id { get; set; }

		/// <summary>
		/// 客服头像
		/// </summary>
		public string kf_headimgurl { get; set; }
	}
}