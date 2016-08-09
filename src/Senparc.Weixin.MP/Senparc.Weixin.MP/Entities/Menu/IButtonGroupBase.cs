using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Entities.Menu
{
    public interface IButtonGroupBase
    {
        List<BaseButton> button { get; set; }
    }
}
