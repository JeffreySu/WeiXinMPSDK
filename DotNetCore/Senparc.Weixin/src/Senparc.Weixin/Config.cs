using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin
{
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
   /// <summary>
	/// 全局设置
	/// </summary>
	public static class Config
	{
		/// <summary>
		/// 请求超时设置（以毫秒为单位），默认为10秒。
		/// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
		/// </summary>
		public const int TIME_OUT = 10000;

		private static bool _isDebug = false;

		/// <summary>
		/// 指定是否是Debug状态，如果是，系统会自动输出日志
		/// </summary>
		public static bool IsDebug
		{
			get
			{
				return _isDebug;
			}
			set
			{
				_isDebug = value;

				//if (_isDebug)
				//{
				//    WeixinTrace.Open();
				//}
				//else
				//{
				//    WeixinTrace.Close();
				//}
			}
		}
		/// <summary>
		/// JavaScriptSerializer 类接受的 JSON 字符串的最大长度
		/// </summary>
		public static int MaxJsonLength = int.MaxValue;
	}
}
