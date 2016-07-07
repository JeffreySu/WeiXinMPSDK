using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Entities.JsonResult.Menu
{
	/// <summary>
	/// CreateMenuAddConditional返回的Json结果 
	/// </summary>
	public class CreateMenuConditionalResult : WxJsonResult
	{
		/// <summary>
		/// menuid
		/// </summary>
		public long menuid { get; set; }
	}
}
