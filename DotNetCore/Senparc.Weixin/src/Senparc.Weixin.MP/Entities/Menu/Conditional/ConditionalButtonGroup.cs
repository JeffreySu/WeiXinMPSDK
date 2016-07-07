namespace Senparc.Weixin.MP.Entities.Menu.AddConditional
{
	/// <summary>
	/// 个性化菜单按钮集合 
	/// </summary>
	public class ConditionalButtonGroup : ButtonGroupBase, IButtonGroupBase
	{
		public MenuMatchRule matchrule { get; set; }
		/// <summary>
		/// 菜单id，只在获取的时候自动填充，提交“菜单创建”请求时不需要设置 
		/// </summary>
		public long menuid { get; set; }
	}
}