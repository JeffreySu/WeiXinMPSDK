using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.User
{
	/// <summary>
	/// 高级接口获取的用户信息
	/// </summary>
	public class UserInfoJson : WxJsonResult
	{
		/// <summary>
		/// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
		/// </summary>
		public int subscribe { get; set; }
		public string openid { get; set; }
		public string nickname { get; set; }
		public int sex { get; set; }
		public string language { get; set; }
		public string city { get; set; }
		public string province { get; set; }
		public string country { get; set; }
		public string headimgurl { get; set; }
		public long subscribe_time { get; set; }
		public string unionid { get; set; }
		public string remark { get; set; }
		public int groupid { get; set; }
	}

	/// <summary>
	/// 批量获取用户基本信息返回结果
	/// </summary>
	public class BatchGetUserInfoJsonResult : WxJsonResult
	{
		public List<UserInfoJson> user_info_list { get; set; }
	}
}
