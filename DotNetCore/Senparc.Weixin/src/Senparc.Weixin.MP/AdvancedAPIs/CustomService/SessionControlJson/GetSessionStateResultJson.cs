using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
	/// <summary>
	/// 获取客户的会话状态返回结果
	/// </summary>
	public class GetSessionStateResultJson : WxJsonResult
	{
		/// <summary>
		/// 正在接待的客服，为空表示没有人在接待
		/// </summary>
		public string kf_account { get; set; }

		/// <summary>
		/// 会话接入的时间
		/// </summary>
		public string createtime { get; set; }
	}
}