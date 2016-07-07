using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.Entities.Menu.AddConditional;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
	/// <summary>
	/// GetMenu返回的Json结果
	/// </summary>
	public class GetMenuResult : WxJsonResult
	{
		//TODO：这里如果有更加复杂的情况，可以换成ButtonGroupBase类型，并提供泛型
		public ButtonGroupBase menu { get; set; }
		/// <summary>
		/// 有个性化菜单时显示
		/// </summary>
		public List<ConditionalButtonGroup> conditionalmenu { get; set; }
		public GetMenuResult(ButtonGroupBase buttonGroupBase)
		{
			menu = buttonGroupBase;
		}
	}
}
