using System;

namespace Senparc.Weixin.Entities
{
	public interface IJsonResult
	{
		string errmsg { get; set; }
		object P2PData { get; set; }
	}

	public interface IWxJsonResult : IJsonResult
	{
		ReturnCode errcode { get; set; }
	}

	/// <summary>
	/// 公众号JSON返回结果（用于菜单接口等）
	/// </summary>
#if NET451
	[Serializable]
#endif 
	public class WxJsonResult : IWxJsonResult
	{
		public ReturnCode errcode { get; set; }
		public string errmsg { get; set; }
		/// <summary>
		/// 为P2P返回结果做准备
		/// </summary>
		public virtual object P2PData { get; set; }
		//public ReturnCode ReturnCode
		//{
		//    get
		//    {
		//        try
		//        {
		//            return (ReturnCode) errorcode;
		//        }
		//        catch
		//        {
		//            return ReturnCode.系统繁忙;//如果有“其他错误”的话可以指向其他错误
		//        }
		//    }
		//}
	}
}
