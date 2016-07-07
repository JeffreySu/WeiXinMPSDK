using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.QY.AdvancedAPIs.KF
{
	/// <summary>
	/// 客服返回结果
	/// </summary>
	public class GetKFListResult : QyJsonResult
	{
		public KF_Item @internal { get; set; }
		public KF_Item external { get; set; }
	}

	public class KF_Item
	{
		public string[] user { get; set; }
		public int[] party { get; set; }
		public int[] tag { get; set; }
	}
}
