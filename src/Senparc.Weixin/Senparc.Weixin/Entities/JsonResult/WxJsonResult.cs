/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：WxJsonResult.cs
    文件功能描述：JSON返回结果基类（用于菜单接口等）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150303
    修改描述：添加QyJsonResult（企业号JSON返回结果）

    修改标识：Senparc - 20150706
    修改描述：调整位置，去除MP下的WxJsonResult
    
    修改标识：Senparc - 20161108
    修改描述：重写ToString()方法，快捷输出结果

----------------------------------------------------------------*/

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
	public class WxJsonResult : IWxJsonResult
	{
		public WxJsonResult BaseResult
		{
			get { return this; }
		}

		public ReturnCode errcode { get; set; }
		public string errmsg { get; set; }
		/// <summary>
		/// 为P2P返回结果做准备
		/// </summary>
		public virtual object P2PData { get; set; }

		public override string ToString()
		{
			return string.Format("WxJsonResult：{{errcode:'{0}',errcode_name:'{1}',errmsg:'{2}'}}",
				(int)errcode, errcode.ToString(), errmsg);
		}

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