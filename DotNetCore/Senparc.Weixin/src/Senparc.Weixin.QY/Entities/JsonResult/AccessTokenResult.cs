using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.Entities
{
	/// <summary>
	/// GetToken请求后的JSON返回格式
	/// </summary>
	public class AccessTokenResult : QyJsonResult
	{
		/// <summary>
		/// 获取到的凭证。长度为64至512个字节
		/// </summary>
		public string access_token { get; set; }
		/// <summary>
		/// 凭证的有效时间（秒）
		/// </summary>
		public int expires_in { get; set; }
	}
}
