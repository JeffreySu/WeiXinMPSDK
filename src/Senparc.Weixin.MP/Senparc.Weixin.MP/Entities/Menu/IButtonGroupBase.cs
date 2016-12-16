using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities.Menu
{
    public interface IButtonGroupBase
    {
        List<BaseButton> button { get; set; }
    }
}